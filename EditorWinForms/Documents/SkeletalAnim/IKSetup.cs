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

namespace EditorWinForms.Documents.SkeletalAnim
{
    public partial class IKSetup : UserControl
    {
        TreeControl chainList; // List of IK chains
        TreeControl chainHiarchy; // Tree of bones in the selected IK chain
        TreeControl skeletonHierarchy; // Bones in the model
        ToolStrip toolStrip;

        public IKSetup()
        {
            InitializeComponent();

            toolStrip = new ToolStrip();
            ToolStripButton newChainButton = new ToolStripButton
            {
                Text = "",
                Name = ""
            };
            toolStrip.Items.Add(newChainButton);
            newChainButton.Click += (sender, args) =>
            {

            };

            ToolStripButton deleteButton = new ToolStripButton
            {
                Text = "",
                Name = ""
            };
            toolStrip.Items.Add(deleteButton);
            deleteButton.Click += (sender, args) =>
            {

            };
        }
    }
}
