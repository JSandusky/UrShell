using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Menu
{
    public static class MenuUtil
    {
        public static ToolStripMenuItem CreateMenuItem(CommandInfo command)
        {
            // Check for an image to load
            if (!String.IsNullOrWhiteSpace(command.ImageName) && command.ImageKey == null)
                command.ImageKey = System.Drawing.Image.FromFile(command.ImageName);

            ToolStripMenuItem ret = new ToolStripMenuItem(command.MenuText, command.ImageKey as System.Drawing.Image) { 
                Tag = command,
                CheckOnClick = command.CheckOnClick
            };
            ret.Click += (sender, args) =>
            {
                // Send the command to the application
                MainWindow.inst().SendCommand(command);
            };
            command.VisibilityChanged += (sender, args) =>
            {
                ret.Visible = (((CommandInfo)sender).Visibility | CommandVisibility.Menu) != 0;
            };
            return ret;
        }

        public static ToolStripButton CreateButton(CommandInfo command)
        {
            // If there's an imagename, check to see if the image is loaded
            if (!String.IsNullOrWhiteSpace(command.ImageName) && command.ImageKey == null)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(command.ImageName);
                command.ImageKey = img; // Media.ImageUtil.ScaleImage(img, 64, 64);
            }

            ToolStripButton btn = new ToolStripButton("", command.ImageKey as System.Drawing.Image) { 
                Tag = command,
                Name = command.MenuText,
                ToolTipText = command.Description,
                CheckOnClick = command.CheckOnClick
            };

            btn.ImageScaling = ToolStripItemImageScaling.None;
            // If we have no image then show text
            if (command.ImageKey == null)
                btn.Text = command.MenuText;

            btn.Click += (sender, args) =>
            {
                // Send the command to the application
                MainWindow.inst().SendCommand(command);
            };

            command.VisibilityChanged += (sender, args) => {
                // Update visibility, it may change again, therefore it is not removed
                btn.Visible = (((CommandInfo)sender).Visibility | CommandVisibility.Toolbar) != 0;
            };
            return btn;
        }
    }
}

