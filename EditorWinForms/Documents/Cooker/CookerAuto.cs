using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorWinForms.Documents.Cooker
{
    /// <summary>
    /// Used to run commandline commands that involve using the cooker project
    /// ie. EditorSuite.exe cooker "C:\myCookerProject.cook"
    /// </summary>
    [EditorCore.Automation.Automatable("cooker")]
    public sealed class CookerAuto
    {
        public static void Run(IDocument document)
        {
            CookerDocument doc = document as CookerDocument;
            if (doc != null)
            {
                Console.WriteLine(String.Format("Cooking project: {0}", doc.Uri.ToString()));
                doc.Cook();
            }
            else
            {
                Console.WriteLine("ERROR: Document is not a valid CookerDocument");
                Console.WriteLine(String.Format("    Found: {0}", document.GetType().FullName));
            }
        }
    }
}
