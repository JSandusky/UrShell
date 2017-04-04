using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    public class DocumentMeta
    {
        public Type DocumentType { get; private set; }
        public DocumentFilter Filter { get; private set; }
        public NewDocumentFactory NewFactoryData { get; private set; }
        public MethodInfo NewFactory { get; private set; }
        public OpenDocumentFactory OpenFactoryData { get; private set; }
        public MethodInfo OpenFactory { get; private set; }

        public DocumentMeta(Type documentType)
        {
            DocumentType = documentType;
            Filter = documentType.GetCustomAttribute<DocumentFilter>();
            foreach (MethodInfo mi in documentType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                NewDocumentFactory newFactory = mi.GetCustomAttribute<NewDocumentFactory>();
                if (newFactory != null)
                {
                    NewFactory = mi;
                    NewFactoryData = newFactory;
                }

                OpenDocumentFactory openFactory = mi.GetCustomAttribute<OpenDocumentFactory>();
                if (openFactory != null)
                {
                    OpenFactory = mi;
                    OpenFactoryData = openFactory;
                }
            }
        }
    }

    public class DocumentMetaCollection : List<DocumentMeta>
    {
        public DocumentMetaCollection(params Type[] args)
        {
            foreach (Type t in args)
                Add(new DocumentMeta(t));
        }

        public DocumentMeta Get<T>()
        {
            foreach (DocumentMeta m in this)
            {
                if (m.DocumentType == typeof(T))
                    return m;
            }
            return null;
        }

        public DocumentMeta Get(Type type)
        {
            foreach (DocumentMeta m in this)
            {
                if (m.DocumentType == type)
                    return m;
            }
            return null;
        }
    }
}
