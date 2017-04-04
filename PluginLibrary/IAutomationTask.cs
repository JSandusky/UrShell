using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    /// <summary>
    /// Interface for externally extending the automation commands
    /// </summary>
    public interface IAutomationTask
    {
        void Run(string command);
    }
}
