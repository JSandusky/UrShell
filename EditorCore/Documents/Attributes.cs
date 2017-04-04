using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    // Mark method as factory method
    // Method must use the signature METHOD(string path)
    [AttributeUsage(AttributeTargets.Method)]
    public class OpenDocumentFactory : Attribute
    {
        public OpenDocumentFactory(string name, string[] ext) { Extensions = ext; Name = name; }

        public string Name { get; set; }
        public string[] Extensions { get; set; }
    }

    /// <summary>
    /// Marks a method as factory method to be used creating an original document
    /// Method must use the signature METHOD()
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NewDocumentFactory : Attribute
    {
        public NewDocumentFactory(string name, string filter, string[] ext) { Filter = filter; Extensions = ext; Name = name; }

        public string Name { get; set; }
        public string Filter { get; set; }
        public string[] Extensions { get; set; }
    }

    /// <summary>
    /// Marks a method as to be invoked for inserting "New"/"Open" menu commands
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DocumentMenuInserter : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DocumentFilter : Attribute
    {
        public DocumentFilter(string filter, string name) { Filter = filter; Name = name; }
        public string Filter { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Marks a method as a command method to be used for persisting a document
    /// Method must use the signature METHOD(IDocument, Uri), type may be subistituted for IDocument
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DocumentSaver : Attribute
    {

    }
}
