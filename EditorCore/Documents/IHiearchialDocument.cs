using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    public interface IHierarchialDocument
    {
        event EventHandler TreeRootChanged;
        object TreeRoot { get; }
    }
}
