using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinLayer
{
    /// <summary>
    /// Interface for containing an event to trigger when an object has changed "substantially" enough
    /// that reevaulation is required
    /// 
    /// ie. ScriptInstance scriptfile/class changed
    /// </summary>
    public interface INotifyObjectChanged
    {
        event EventHandler ObjectChanged;
    }
}
