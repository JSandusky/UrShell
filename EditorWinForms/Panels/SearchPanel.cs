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
using System.Threading;
using Sce.Atf.Controls;
using Sce.Atf.Applications;
using EditorCore;
using EditorCore.Panels;
using Sce.Atf;

namespace EditorWinForms.Panels
{
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide, true)]
    public partial class SearchPanel : DockContent
    {
        TreeListControl list;
        TreeView view;
        
        public SearchPanel()
        {
            InitializeComponent();
            txtSearch.KeyUp += txtSearch_KeyUp;
            Text = "Search".Localize();

            list = new Sce.Atf.Controls.TreeListControl();
            list.ShowRoot = false;
            view = new TreeView();
            list.Dock = DockStyle.Fill;
            list.ItemRenderer = new TreeListItemRenderer(view);
            list.Columns.Add(new TreeListView.Column("Detail", 80));
            splitter.Panel2.Controls.Add(list);
        }

        void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                DoSearch(txtSearch.Text);
            }
        }

        void DoSearch(string searchText)
        {
            view.results.Clear();
            string[] terms = searchText.Split(' ');
            Thread thread = new Thread(delegate()
            {
                List<EditorCore.Interfaces.ISearchResult> results = new List<EditorCore.Interfaces.ISearchResult>();
                //foreach (EditorCore.Interfaces.ISearchable searchable in UIRegistry.GetInst().GetList<EditorCore.Interfaces.ISearchable>())
                //{
                //    searchable.Search(terms, results);
                //    if (results.Count > 0)
                //    {
                //        Invoke((MethodInvoker)delegate
                //        {
                //            AddResults(results);
                //        });
                //    }
                //    results.Clear();
                //}
            });
            thread.Start();
        }

        void AddResults(List<EditorCore.Interfaces.ISearchResult> results)
        {
            GroupNode gp = new GroupNode { Name = results[0].SourceName };
            foreach (EditorCore.Interfaces.ISearchResult result in results)
                gp.Results.Add(result);
        }

        class TreeView : ITreeView, IItemView
        {
            public List<GroupNode> results = new List<GroupNode>();

            #region ITreeView
            public IEnumerable<object> GetChildren(object parent)
            {
                if (parent is TreeView)
                    return results;
                if (parent is GroupNode)
                    return ((GroupNode)parent).Results;
                return null;
            }

            public object Root
            {
                get { return this; }
            }
            #endregion

            #region IItemView
            public void GetInfo(object item, ItemInfo info)
            {
                if (item is TreeView)
                {
                    info.IsLeaf = false;
                }
                else if (item is GroupNode)
                {
                    info.Label = ((GroupNode)item).Name;
                    info.IsLeaf = false;
                }
                else if (item is EditorCore.Interfaces.ISearchResult)
                {
                    info.IsLeaf = true;
                    EditorCore.Interfaces.ISearchResult result = item as EditorCore.Interfaces.ISearchResult;
                    info.Label = result.Detail;
                    info.Properties = new object[] { result.Location };
                }
            }
            #endregion
        }

        class GroupNode
        {
            public string Name;
            public List<EditorCore.Interfaces.ISearchResult> Results = new List<EditorCore.Interfaces.ISearchResult>();
        }
    }
}
