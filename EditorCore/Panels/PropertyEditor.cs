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
using System.Reflection;
using Sce.Atf.Applications;
using EditorCore;
using Sce.Atf;

namespace EditorCore.Panels
{
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockRight, true)]
    public partial class PropertyEditor : Controls.PanelDockContent
    {
        Sce.Atf.Controls.PropertyEditing.PropertyGrid sonygrid;
        
        public PropertyEditor()
        {
            InitializeComponent();
            Text = "Properties".Localize();
            DoubleBuffered = true;
            CloseButton = CloseButtonVisible = false;

            sonygrid = new Sce.Atf.Controls.PropertyEditing.PropertyGrid();
            sonygrid.PropertySorting = Sce.Atf.Controls.PropertyEditing.PropertySorting.None;
            sonygrid.Dock = DockStyle.Fill;
            Controls.Add(sonygrid);

            DocumentManager.GetInst().DocumentChanged += PropertyEditor_DocumentChanged;
        }

        void PropertyEditor_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            ISelectionContext docBase = args.New as ISelectionContext;
            if (docBase != null)
            {
                docBase.SelectionChanged += docBase_SelectionChanged;
                if (docBase.SelectionCount != 0)
                    docBase_SelectionChanged(docBase, null); // EventArgs is unused
                return;
            }

            Documents.IPropertyDocument pdoc = args.New as Documents.IPropertyDocument;
            if (pdoc != null)
            {
                pdoc.PropertyBoundChanged += pdoc_PropertyBoundChanged;
                sonygrid.Visible = true;
                sonygrid.Bind(pdoc.PropertyBound);
                return;
            }

            sonygrid.Bind(null);
            sonygrid.Visible = false;
        }

        void pdoc_PropertyBoundChanged(object sender, EventArgs e)
        {
            sonygrid.Bind(((Documents.IPropertyDocument)sender).PropertyBound);
        }

        void docBase_SelectionChanged(object sender, EventArgs e)
        {
            ISelectionContext src = sender as ISelectionContext;
            if (src != null)
            {
                if (src.SelectionCount == 1)
                {
                    object sel = src.Selection.First();
                    sonygrid.Visible = true;
                    sonygrid.Bind(sel);
                }
                else
                {
                    sonygrid.Visible = false; // Hiding the grid prevents visual anomalies
                    sonygrid.Bind(null);
                }
            }
        }
    }
}
