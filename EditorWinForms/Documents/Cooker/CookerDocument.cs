using EditorCore;
using EditorCore.Documents;
using Sce.Atf;
using Sce.Atf.Applications;
using Sce.Atf.Controls.PropertyEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace EditorWinForms.Documents.Cooker
{
    [DocumentFilter("Cooker Project (*.cook)|*.cook", "Cooker Project")]
    public class CookerDocument : DocumentBase, ICommandClient
    {
        CookerView view;

        [NewDocumentFactory("Cooker Project", "Cooker Projects (*.cook)|*.cook", new string[] { ".cook" })]
        public static CookerDocument NewDocumentFactory()
        {
            return new CookerDocument(null);
        }

        [OpenDocumentFactory("Cooker Project", new string[] { ".cook" })]
        public static CookerDocument OpenDocumentFactory(string path)
        {
            return new CookerDocument(path);
        }

        private CookerDocument(string path)
        {
            if (path != null)
                DoLoad(new Uri(path));
            view = new CookerView(this);
            view.Show(MainWindow.inst().DockingPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        List<ICookBase> cookingItems_ = new List<ICookBase>();
        public List<ICookBase> CookingItems { get { return cookingItems_; } }

        public void Cook()
        {
            foreach (CookTask item in CookingItems)
                item.Cook();
        }

        #region IDocument
        public override string Type { get { return "Cooker".Localize(); } }
        #endregion

        public bool CanDoCommand(object commandTag)
        {
            if (commandTag.ToString().Equals("SaveCmd"))
                return true;
            else if (commandTag.ToString().Equals("SaveAsCmd"))
                return true;
            return false;
        }

        public void DoCommand(object commandTag)
        {
            if (commandTag.ToString().Equals("SaveCmd"))
                DoSave(Uri == null);
            else if (commandTag.ToString().Equals("SaveAsCmd"))
                DoSave(true);
        }

        public void UpdateCommand(object commandTag, CommandState commandState)
        {
        }

        void DoSave(bool prompt)
        {
            if (prompt)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Cooker (*.cook)|*.cook";
                dlg.Title = "Save Cooker".Localize();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Uri = new Uri(dlg.FileName);
                }
            }
            if (Uri != null)
            {
                System.Xml.Serialization.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(typeof(List<ICookBase>));
                using (MemoryStream stream = new MemoryStream())
                {
                    serial.Serialize(stream, CookingItems);
                    stream.Position = 0;
                    XmlDocument doc = new XmlDocument();
                    doc.Load(stream);
                    doc.Save(Uri.LocalPath);
                    stream.Close();
                }
            }
        }

        void DoLoad(Uri file)
        {
            Uri = file;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(file.LocalPath);
            string xmlString = xmlDocument.OuterXml;
            using (StringReader read = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ICookBase>));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    cookingItems_ = (List<ICookBase>)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
            }

        }
    }
}
