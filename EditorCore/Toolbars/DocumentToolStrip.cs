using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Toolbars
{
    /// <summary>
    /// A toolstrip whose visibile (or enabled status) is linked to a document type
    /// </summary>
    public class DocumentToolStrip : ToolStrip
    {
        public Type DocumentType { get; set; }
        public bool DisableMode { get; set; }

        public DocumentToolStrip(string name, Type docType, bool disableMode = false)
        {
            Name = name;
            DisableMode = disableMode;
            DocumentType = docType;
            DocumentManager.GetInst().DocumentChanged += DocumentToolStrip_DocumentChanged;
        }

        public DocumentToolStrip(string name, Type docType, List<CommandInfo> commands, bool disableMode = false)
        {
            Name = name;
            DisableMode = disableMode;
            DocumentType = docType;
            CommandToolStrip.Fill(this, commands);
        }

        void DocumentToolStrip_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            if (DisableMode)
            {
                if (args.New == null)
                    Enabled = false;
                else
                {
                    Enabled = args.New.GetType() == DocumentType;
                    Visible = true;
                }
            }
            else
            {
                if (args.New == null)
                    Visible = false;
                else
                    Visible = args.New.GetType() == DocumentType;
            }
        }
    }
}
