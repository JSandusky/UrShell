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
using EditorCore;
using Sce.Atf;
using Sce.Atf.Applications;

namespace EditorCore.Panels
{
    /// <summary>
    /// Spreadsheet style Property editing control that allows editing multiple objects at once and comparing their values more easily
    /// </summary>
    [PanelHint(WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide, true)]
    public partial class PropertyTable : Controls.PanelDockContent
    {
        Sce.Atf.Controls.PropertyEditing.GridControl propertyGrid;

        public PropertyTable()
        {
            InitializeComponent();
            Text = "Property Sheet".Localize();

            DocumentManager.GetInst().DocumentChanged += PropertyTable_DocumentChanged;

            propertyGrid = new Sce.Atf.Controls.PropertyEditing.GridControl();
            propertyGrid.Dock = DockStyle.Fill;
            Controls.Add(propertyGrid);
        }

        void PropertyTable_DocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            ISelectionContext docBase = args.New as ISelectionContext;
            if (docBase != null)
                docBase.SelectionChanged += docBase_SelectionChanged;
            else
            {
                // If the active document is an IPropertyDocument then we'll bind all open documents "PropertyBound" attribute to the sheet
                if (args.New is Documents.IPropertyDocument)
                {
                    List<Documents.IPropertyDocument> openDocs = DocumentManager.GetInst().GetList<Documents.IPropertyDocument>(args.New.GetType());
                    if (openDocs.Count > 0)
                    {
                        List<object> things = new List<object>();
                        foreach (Documents.IPropertyDocument doc in openDocs)
                            things.Add(doc.PropertyBound);
                        SetObject(things);
                        return;
                    }
                }
            }
            SetObject(null);
        }

        void docBase_SelectionChanged(object sender, EventArgs e)
        {
            ISelectionContext src = sender as ISelectionContext;
            if (src != null)
            {
                if (src.SelectionCount > 0)
                {
                    SetObject(src.Selection);
                    propertyGrid.Refresh();
                    return;
                }
            }
            SetObject(null);
            propertyGrid.Refresh();
        }

        public void SetObject(object obj)
        {
            List<object> objs = obj as List<object>;
            if (objs != null)
            {
                List<object> objects = new List<object>();
                foreach (object o in objs)
                    objects.Add(o);
                propertyGrid.Bind(objects.ToArray());
            }
            else
                propertyGrid.Bind(obj);
        }
    }
}
