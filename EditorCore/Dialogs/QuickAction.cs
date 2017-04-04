using Sce.Atf.Applications;
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
    public partial class QuickAction : Form
    {
        List<CommandInfo> lastResults = new List<CommandInfo>();
        public QuickAction()
        {
            InitializeComponent();
            txtQuery.KeyUp += txtQuery_KeyUp;
            itemsList.DoubleClick += itemsList_DoubleClick;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void itemsList_DoubleClick(object sender, EventArgs e)
        {
            DoCommand();
        }

        void txtQuery_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (lastResults.Count > 0)
                {
                    MainWindow.inst().SendCommand(lastResults[0]);
                    Close();
                }
            }
        }

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            UpdateQuery();
        }

        void UpdateQuery()
        {
            lastResults = CommandRegistry.GetInst().QueryCommands(txtQuery.Text);
            itemsList.Items.Clear();
            if (lastResults.Count > 0)
            {
                foreach (CommandInfo ci in lastResults)
                    itemsList.Items.Add(ci.MenuText);
            }
        }

        private void itemsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoCommand();
        }

        void DoCommand()
        {
            if (itemsList.SelectedItem != null)
            {
                string str = itemsList.SelectedItem.ToString();
                foreach (CommandInfo ci in lastResults)
                {
                    if (ci.MenuText.Equals(str))
                    {
                        MainWindow.inst().SendCommand(ci);
                        Close();
                    }
                }
            }
        }
    }
}
