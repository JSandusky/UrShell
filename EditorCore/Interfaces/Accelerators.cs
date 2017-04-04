using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorCore.Interfaces
{
    public class Accelerator
    {
        public string CmdName { get; set; }

        public string Text
        {
            get
            {
                string ret = "";
                foreach (Keys k in modifiers)
                {
                    if (ret.Length > 0)
                        ret += " + ";
                    ret += k.ToString();
                }
                if (ret.Length > 0)
                    ret += key.ToString();
                return ret;
            }
        }

        public Keys Modifiers
        {
            get
            {
                Keys ret = (Keys)0;
                foreach (Keys k in modifiers)
                    ret |= k;
                return ret;
            }
        }

        public Keys Concatenated {
            get {
                return Modifiers & key;
            }
        }

        public bool Passes(Keys inputKeys)
        {
            return (inputKeys & Concatenated) != 0;
        }

        public List<Keys> modifiers = new List<Keys>();
        public Keys key;
    }

    public class AcceleratorContext
    {
        public List<Accelerator> Accelerators { get; private set; }
        public Type ContextType { get; private set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
        public AcceleratorContext(Type type)
        {
            ContextType = type;
            Accelerators = new List<Accelerator>();
        }
    }

    [IProgramInitializer("Accelerators")]
    public class Accelerators
    {
        static Accelerators inst_;
        public static Accelerators GetInst() { return inst_; }

        static readonly string ACCELFILE = "accelerators.xml";

        List<AcceleratorContext> accelerators_ = new List<AcceleratorContext>();

        private Accelerators()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string accelPath = System.IO.Path.Combine(appDataDir, Assembly.GetEntryAssembly().GetName().Name);
            if (!System.IO.Directory.Exists(accelPath))
                System.IO.Directory.CreateDirectory(accelPath);
            accelPath = System.IO.Path.Combine(accelPath, ACCELFILE);

            // If a file does not exist in app data
            if (!System.IO.File.Exists(accelPath))
                accelPath = ACCELFILE;
            
            XmlDocument doc = new XmlDocument();
            doc.Load(accelPath);
            foreach (XmlElement contextElem in doc.GetElementsByTagName("context"))
            {
                Type contextType = null;
                try {
                    contextType = Type.GetType(contextElem.GetAttribute("type"));
                } 
                catch (Exception)
                {
                    continue;
                }
                if (contextType == null)
                    continue;
                AcceleratorContext context = new AcceleratorContext(contextType);
                context.DisplayName = contextElem.GetAttribute("name");
                if (contextElem.HasAttribute("category"))
                    context.Category = contextElem.GetAttribute("category");

                foreach (XmlElement elem in contextElem.GetElementsByTagName("accel"))
                {
                    string[] accelTerms = elem.InnerText.Split(',');
                    Accelerator accel = new Accelerator();
                    foreach (string keyStr in accelTerms)
                    {
                        Keys key = (Keys)Enum.Parse(typeof(Keys), keyStr.Trim());
                        if (IsModifierKey(key))
                            accel.modifiers.Add(key);
                        else
                            accel.key = key;
                    }
                    accel.CmdName = elem.GetAttribute("cmd");
                    context.Accelerators.Add(accel);
                }
                accelerators_.Add(context);
            }
        }

        public bool IsAccelerator(object context, Keys key, out string cmdName)
        {
            cmdName = null;
            if (context == null)
                return false;
            foreach (AcceleratorContext accContext in accelerators_)
            {
                if (accContext.ContextType.IsAssignableFrom(context.GetType()))
                {
                    foreach (Accelerator acc in accContext.Accelerators)
                    {
                        if (acc.Passes(key))
                        {
                            cmdName = acc.CmdName;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsModifierKey(Keys key)
        {
            if ((key & Keys.Control) != 0)
                return true;
            else if ((key & Keys.Shift) != 0)
                return true;
            else if ((key & Keys.Alt) != 0)
                return true;
            return false;
        }

        static void ProgramInitialized()
        {
            inst_ = new Accelerators();
        }
    }
}
