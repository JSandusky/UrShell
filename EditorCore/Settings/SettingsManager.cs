using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorCore.Settings
{
    [EditorCore.Interfaces.IProgramInitializer("Settings Management")]
    [EditorCore.Interfaces.IProgramTerminated]
    public class SettingsManager
    {
        static SettingsManager inst_;
        List<SettingsObject> settings_ = new List<SettingsObject>();

        public static SettingsManager GetInst() { return inst_; }

        public T GetSettingsObject<T>() where T : SettingsObject
        {
            foreach (SettingsObject obj in settings_)
            {
                if (obj.GetType() == typeof(T))
                    return (T)obj;
            }
            return default(T);
        }

        public SettingsObject GetSettingsObject(Type t)
        {
            foreach (SettingsObject obj in settings_)
            {
                if (obj.GetType() == t)
                    return obj;
            }
            return null;
        }

        public List<SettingsObject> GetSettings() { return settings_; }

        public XmlDocument GetXML()
        {
            XmlDocument newDocument = new XmlDocument();
            XmlElement rootElem = newDocument.CreateElement("settings");
            newDocument.AppendChild(rootElem);

            foreach (SettingsObject obj in inst_.settings_)
            {
                XmlElement settingElem = newDocument.CreateElement(obj.GetType().Name);
                rootElem.AppendChild(settingElem);
                obj.StoreSettings(newDocument, settingElem);
            }

            return newDocument;
        }

        public void SetXML(XmlDocument document)
        {
            foreach (SettingsObject obj in inst_.settings_)
            {
                // There should only be one
                foreach (XmlElement elem in document.GetElementsByTagName(obj.GetType().Name))
                    obj.RestoreSettings(elem);
            }
        }

        static void ProgramInitialized()
        {
            inst_ = new SettingsManager();

            string exeDir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string pathDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pathDir = System.IO.Path.Combine(pathDir, Assembly.GetEntryAssembly().GetName().Name);
            pathDir = System.IO.Path.Combine(pathDir, "settings.xml");

            if (!System.IO.File.Exists(pathDir))
                pathDir = System.IO.Path.Combine(exeDir, "settings.xml");

            // Instantiate settings objects
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type t in asm.GetTypes())
                    {
                        if (typeof(SettingsObject).IsAssignableFrom(t) && !t.IsAbstract)
                        {
                            inst_.settings_.Add(Activator.CreateInstance(t) as SettingsObject);
                        }
                    }
                }
                catch (Exception) { }
            }

            if (System.IO.File.Exists(pathDir))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathDir);
                inst_.SetXML(doc);
            }
        }

        static void Terminate()
        {
            XmlDocument newDocument = inst_.GetXML();

            string pathDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); ;
            pathDir = System.IO.Path.Combine(pathDir, Assembly.GetEntryAssembly().GetName().Name);
            pathDir = System.IO.Path.Combine(pathDir, "settings.xml");
            newDocument.Save(pathDir);
        }
    }
}
