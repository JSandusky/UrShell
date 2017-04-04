using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Typing
{
    // Interface for TypeDescriptors/PropertyDescriptors to implement to use the External typing
    public interface ISourcedDescriptor
    {
        object Source { get; }
    }
}
