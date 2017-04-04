using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Sce.Atf.Applications;
using EditorCore;
using Sce.Atf;

namespace EditorCore.Panels
{
    /// <summary>
    /// Display a Markdown based Help view
    /// </summary>
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockRight, true)]
    public partial class HelpPanel : Controls.PanelDockContent
    {
        EditorCore.Controls.MarkDownViewer markdown;

        public HelpPanel()
        {
            InitializeComponent();

            btnBack.Image = Media.ImageUtil.FromResourceFolder("Undo32.png");
            btnForward.Image = Media.ImageUtil.FromResourceFolder("Redo32.png");

            Text = "Help".Localize();
            markdown = new EditorCore.Controls.MarkDownViewer();
            markdown.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Add(markdown);

            HandleCreated += HelpPanel_HandleCreated;
            markdown.Navigating += markdown_Navigating;
        }

        void markdown_Navigating(object sender, EventArgs e)
        {
            string to = sender.ToString();
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path = System.IO.Path.Combine(path, "Resources/help");
            path = System.IO.Path.Combine(path, to);
            if (System.IO.File.Exists(path))
                markdown.Markdown = System.IO.File.ReadAllText(path);
        }

        void HelpPanel_HandleCreated(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            path = System.IO.Path.Combine(path, "Resources/help");
            path = System.IO.Path.Combine(path, "index.md");

            markdown.Markdown = System.IO.File.ReadAllText(path);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            markdown.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            markdown.GoForward();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void btnScript_Click(object sender, EventArgs e)
        {

        }

        private void btnAttributes_Click(object sender, EventArgs e)
        {

        }
    }
}
