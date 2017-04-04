using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Menu
{
    /// <summary>
    /// MenuStripItem that is only visible when a specific type of menu is displayed
    /// May optionally chose to be disabled instead of made invisible (single document program)
    /// </summary>
    public class DocumentMenuStripItem<T> : ToolStripMenuItem
    {
        bool disableMode_;

        public DocumentMenuStripItem(string title, bool disable = false) : base(title)
        {
            Name = title;
            DocumentManager.GetInst().DocumentChanged += DocumentMenuStripItem_DocumentChanged;
            disableMode_ = disable;
        }

        void DocumentMenuStripItem_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            if (args.New == null || args.New.GetType() != typeof(T))
            {
                if (disableMode_)
                    Enabled = false;
                else
                    Visible = false;
            }
            else
            {
                if (disableMode_)
                    Enabled = true;
                else
                    Visible = true;
            }
        }
    }
}
