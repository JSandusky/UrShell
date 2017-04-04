using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Automation
{
    /// <summary>
    /// Documents that may be constructed for commandline automation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Automatable : Attribute
    {
        public Automatable(string kind) { AutomationType = kind; }
        public string AutomationType { get; private set; }
    }
}
