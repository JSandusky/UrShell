using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore
{
    public abstract class ProgramBuilder
    {
        List<Type> documentTypes = new List<Type>();
        List<Type> panelTypes = new List<Type>();

        public void AddDocuments(params Type[] args)
        {
            documentTypes.AddRange(args);
        }

        public void AddPanels(params Type[] args)
        {
            panelTypes.AddRange(args);
        }
    }
}
