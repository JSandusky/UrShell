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
using Sce.Atf.Controls;
using EditorWinForms;
using EditorCore;
using Sce.Atf;

namespace EditorCore.Panels
{
    /// <summary>
    /// Full filesystem tree
    /// </summary>
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide, true)]
    public partial class FileSystemPanel : Controls.PanelDockContent
    {
        ImageList imageList;
        TreeControl tree;
        ListView viewList;

        public FileSystemPanel()
        {
            InitializeComponent();
            Text = "File Browser".Localize();

            imageList = new ImageList();
            //imageList.Images.Add(Resources.Asset32);

            tree = new TreeControl();
            tree.ImageList = imageList;
            tree.Dock = DockStyle.Fill;
            tree.NodeExpandedChanged += tree_NodeExpandedChanged;
            tree.SelectionChanged += tree_SelectionChanged;
            splitter.Panel1.Controls.Add(tree);

            //Fill(rootPath, tree.Root);
            tree.ShowRoot = false;

            viewList = new ListView();
            viewList.Dock = DockStyle.Fill;
            viewList.Columns.Add(new ColumnHeader { Text = "Filename", Name = "Name" });
            viewList.Columns.Add(new ColumnHeader { Text = "Last Modified", Name = "LastModified" });
            viewList.Columns.Add(new ColumnHeader { Text = "Size", Name = "Size" });
            viewList.TileSize = new System.Drawing.Size(256, 128);
            viewList.View = View.Details;
            splitter.Panel2.Controls.Add(viewList);
        }

        void tree_SelectionChanged(object sender, EventArgs e)
        {
            if (tree.SelectedNodes.Count() == 1)
            {
                TreeControl.Node nd = tree.SelectedNodes.First();
                string str = nd.Tag as string;
                viewList.Clear();

                foreach (string file in System.IO.Directory.EnumerateFiles(str))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);
                    
                    ListViewItem item = new ListViewItem(new string[] {
                        System.IO.Path.GetFileName(file),
                        fi.LastAccessTime.ToString(),
                        StringExtensions.ToFileSize((int)fi.Length)
                    });
                    viewList.Items.Add(item);
                }
            }
        }

        void tree_NodeExpandedChanged(object sender, TreeControl.NodeEventArgs e)
        {
            if (e.Node.Expanded)
            {
                Fill(e.Node.Tag as string, e.Node);
                Invalidate();
            }
            else
            {
                e.Node.Clear();
            }
        }

        void Fill(string path, TreeControl.Node current)
        {
            try
            {
                foreach (string dir in System.IO.Directory.EnumerateDirectories(path))
                {
                    TreeControl.Node newNode = current.Add(dir);
                    newNode.Label = System.IO.Path.GetFileName(dir);
                    newNode.IsLeaf = System.IO.Directory.EnumerateDirectories(dir).Count() == 0;
                    newNode.ImageIndex = 0;
                }
            } catch (Exception)
            {

            }
        }
    }
}
