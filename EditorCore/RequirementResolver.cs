using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf;
using EditorCore.Settings;

namespace EditorCore
{
    /// <summary>
    /// Required is a soft binding, sort of like a Lazy<> that asks for something from the major places
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Required<T> where T : new()
    {
        T cached_;
        bool failed_ = false;
        bool firstAttempt_ = false;
        public T Value
        {
            get
            {
                if (cached_ != null && !failed_)
                    return cached_;
                cached_ = (T)Resolve(typeof(T));
                return cached_;
            }
        }

        object Resolve(Type type)
        {
            try
            {
                // Check in settings
                if (typeof(SettingsObject).IsAssignableFrom(type))
                    return SettingsManager.GetInst().GetSettingsObject(type);

                // Check in the UIRegistry
                //if (typeof(System.Windows.Forms.Control).IsAssignableFrom(type))
                //{
                //    object o = UIRegistry.GetInst().GetSingle(type);
                //    if (o != null)
                //        return o;
                //}

                // Check in the DocumentManager?
                if (typeof(IDocument).IsAssignableFrom(type))
                {
                    if (firstAttempt_)
                        DocumentManager.GetInst().DocumentChanged += Required_DocumentChanged;
                    if (DocumentManager.GetInst().ActiveDocument != null && type.IsAssignableFrom(DocumentManager.GetInst().ActiveDocument.GetType()))
                        return DocumentManager.GetInst().ActiveDocument;
                }
                failed_ = true;
                return null;
            }
            finally
            {
                firstAttempt_ = false;
            }
        }

        void Required_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            failed_ = false; // Reset failed flag
            cached_ = (T)Resolve(typeof(T));
        }
    }
}
