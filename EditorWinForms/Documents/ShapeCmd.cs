using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf;

namespace EditorWinForms.Documents
{
    public static class ShapeCmd
    {
        public static readonly CommandInfo BoxShapeCmd = new CommandInfo("BoxShapeCmd",
            "Shape",
            "Shape",
            "Box".Localize(),
            "Displays the preview as a box".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/Box.png"
            };

        public static readonly CommandInfo SphereShapeCmd = new CommandInfo("SphereShapeCmd",
            "Shape",
            "Shape",
            "Sphere".Localize(),
            "Displays the preview as a sphere".Localize()) 
            { 
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/Sphere.png"
            };

        public static readonly CommandInfo PlaneShapeCmd = new CommandInfo("PlaneShapeCmd",
            "Shape",
            "Shape",
            "Plane".Localize(),
            "Displays the preview as a plane".Localize())
            {
                Visibility = CommandVisibility.Toolbar,
                ImageName = "Resources/images/Plane.png"
            };
    }
}
