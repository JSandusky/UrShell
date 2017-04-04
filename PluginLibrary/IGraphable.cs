using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    /// <summary>
    /// Interface that must be implemented to create a graphable type
    /// </summary>
    public interface IGraphable
    {
        string Name { get; }
        IList<IGraphSocket> IncomingSockets { get; }
        IList<IGraphSocket> OutgoingSockets { get; }
    }

    public interface IGraphSocket
    {
        string RestrictionName { get; }
        int ConnectionLimit { get; }
        IList<IGraphable> Connections { get; }
    }
}
