using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    /// <summary>
    /// Interface for a document that can have notes.
    /// </summary>
    public interface INotableDocument
    {
        ICollection<string> Notes { get; }

        void NoteUpdated(int index, string text);
    }
}
