using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    /// <summary>
    /// Implementing types will be documents that need properties to be bound to the property grids
    /// The Editor will show the active document
    /// The "Sheet" will show all open documents of the same kind as the active document
    /// </summary>
    public interface IPropertyDocument
    {
        event EventHandler PropertyBoundChanged;
        object PropertyBound { get; }
    }
}
