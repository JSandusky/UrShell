using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorWinForms.Controls
{
    /// <summary>
    /// Displays an XML document in the tree view
    /// The document is readonly
    /// </summary>
    public partial class XmlTreeControl : Sce.Atf.Controls.TreeControl
    {
        public XmlTreeControl()
        {
            InitializeComponent();
            ShowRoot = false;
        }

        XmlDocument document_;

        public int DocumentDepth
        {
            get;
            set;
        }

        public XmlDocument Document
        {
            get { return document_; }
            set
            {
                document_ = value;
                Root.Clear();
                Fill(Root, document_.DocumentElement, DocumentDepth);
            }
        }

        void Fill(Node treeNode, XmlElement element, int depth)
        {
            treeNode.Label = element.Name;
            treeNode.IsLeaf = element.ChildNodes.Count > 0 && depth > 1;

            if (depth > 0)
            {
                foreach (XmlElement child in element.ChildNodes)
                {
                    Node newNode = treeNode.Add(child);
                    Fill(newNode, child, depth - 1);
                }
            }
        }
    }
}
