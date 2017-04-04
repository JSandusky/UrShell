using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using EditorCore;
using Sce.Atf;

namespace EditorCore.Dialogs
{
    /// <summary>
    /// Manages the setup of paths/source-tree/etc
    /// </summary>
    public partial class SettingsDlg : Form
    {
        Sce.Atf.Controls.TreeControl tree;
        Sce.Atf.Controls.PropertyEditing.PropertyGrid propertyGrid;
        //PropertyGrid propertyGrid;

        new public static void ShowDialog()
        {
            List<Settings.SettingsObject> settings = Settings.SettingsManager.GetInst().GetSettings();
            
            SettingsDlg dlg = new SettingsDlg(settings);
            dlg.ShowDialog(null);
        }

        SettingsDlg(List<Settings.SettingsObject> settings)
        {
            InitializeComponent();

            // Create the tree view
            tree = new Sce.Atf.Controls.TreeControl();
            tree.Dock = DockStyle.Fill;
            tree.SelectionChanged += tree_SelectionChanged;
            topSplitter.Panel1.Controls.Add(tree);
            tree.ShowRoot = false;

            foreach (Settings.SettingsObject obj in settings)
            {
                Sce.Atf.Controls.TreeControl.Node node = tree.Root.Add(obj);
                node.Label = obj.GetType().Name.Replace("Settings", "").Localize();
                node.IsLeaf = true;
            }

            // Create the property sheet
            propertyGrid = new Sce.Atf.Controls.PropertyEditing.PropertyGrid();
            propertyGrid.Dock = DockStyle.Fill;
            propertiesSplitter.Panel1.Controls.Add(propertyGrid);

            Sce.Atf.Applications.SkinService.ApplyActiveSkin(this);
        }

        void tree_SelectionChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNodes.Count() == 1)
            {
                propertyGrid.Visible = true;
                propertyGrid.Bind(tree.SelectedNodes.First().Tag);
            }
            else
            {
                propertyGrid.Visible = false;
                propertyGrid.Bind(null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.None;
            Close();
        }
    }
}
