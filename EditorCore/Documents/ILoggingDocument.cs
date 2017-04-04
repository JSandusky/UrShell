using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Documents
{
    public delegate void LogMessageAdded(object msg, EventArgs args);

    public interface ILoggingDocument
    {
        event LogMessageAdded LogAppended;

        void AddLogMessage(string msg);
        List<string> Log { get; }
    }
}
