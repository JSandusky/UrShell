using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sce.Atf.Controls;
using Sce.Atf;

namespace EditorWinForms.Documents.Cooker
{
    public partial class CookerView : EditorCore.Controls.DocumentDockContent
    {
        CookerTree contents;
        CookerPalette palette;

        public CookerView(CookerDocument doc) : base(doc)
        {
            InitializeComponent();

            palette = new CookerPalette();
            palette.Dock = DockStyle.Fill;

            contents = new CookerTree(doc);
            contents.Dock = DockStyle.Fill;

            splitter.Panel1.Controls.Add(palette);
            splitter.Panel2.Controls.Add(contents);

        // Toolstrip commands for the cooker
            ToolStrip st = new ToolStrip();
            st.Dock = DockStyle.Top;
            Controls.Add(st);

            // Cook
            ToolStripButton cookCmd = new ToolStripButton("Cook");
            cookCmd.Click += (src, args) =>
            {
                doc.Cook();
            };
            st.Items.Add(cookCmd);

            // Delete
            ToolStripButton deleteCmd = new ToolStripButton("");
            deleteCmd.Image = EditorCore.Media.ImageUtil.FromResourceFolder("delete.png");
            deleteCmd.Click += (src, args) =>
            {
                contents.DeleteSelected();
            };
            st.Items.Add(deleteCmd);
        }
    }
}
