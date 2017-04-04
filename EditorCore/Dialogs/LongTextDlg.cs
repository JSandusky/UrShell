using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Dialogs
{
    /// <summary>
    /// Simple dialog for filling out large amounts of text such as notes
    /// </summary>
    public partial class LongTextDlg : Form
    {
        string text_;

        public static string GetText(string title, string txt)
        {
            LongTextDlg dlg = new LongTextDlg(title, txt);
            dlg.ShowDialog();
            return dlg.text_;
        }

        LongTextDlg(string title, string txt)
        {
            InitializeComponent();
            txtText.Text = txt;
            Text = title;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
