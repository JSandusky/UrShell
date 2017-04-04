using EditorCore;
using EditorCore.Documents;
using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EditorWinForms.Documents
{
    [DocumentFilter("UI Layouts (*.xml)|*.xml","User Interface")]
    public class UIDocument : IDocument
    {
        [OpenDocumentFactory("UI Layout", new string[] { ".xml" })]
        public static UIDocument FactoryMethod(string path)
        {
            // Verify it's a valid UI layout
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                if (!doc.DocumentElement.Name.Equals("element"))
                {
                    return null;
                }
            }
            catch (Exception ex) {
                ErrorHandler.GetInst().Error(ex);
            }
            return null;
        }

        [NewDocumentFactory("UI Layout", "UI Layouts (*.xml)|*.xml", new string[] { ".xml" })]
        public static UIDocument NewMethod(string path)
        {
            return null;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {

        }

        public void SaveAs()
        {

        }

        public bool Dirty
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler DirtyChanged;

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public string Type
        {
            get { throw new NotImplementedException(); }
        }

        public Uri Uri
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler<UriChangedEventArgs> UriChanged;
    }
}
