using Sce.Atf.Applications;
using Sce.Atf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorCore;

namespace EditorWinForms.Documents.SceneUI
{
    public static class SceneCmd
    {
        public static readonly CommandInfo CreateLocalNodeCmd = new CommandInfo("CMD_CREATE_LOCAL_NODE",
            "",
            "Create",
            "Create Local Node".Localize(),
            "Creates a new local node".Localize()) 
        { 
        Visibility = CommandVisibility.Toolbar 
        };

        public static readonly CommandInfo CreateReplicatedNodeCmd = new CommandInfo("CMD_CREATE_REPLICATED_NODE",
            "",
            "Create",
            "Create Replicated Node".Localize(),
            "Creates a new replicated node".Localize())
        {
            Visibility = CommandVisibility.Toolbar
        };

        public static readonly CommandInfo BuildNavigationCmd = new CommandInfo("CMD_BUILD_NAVIGATION",
            "Tools",
            "Tools",
            "Build Navigation Data".Localize(),
            "Builds navigation meshes for the scene".Localize());

        public static readonly CommandInfo CreateBuiltInBox = new CommandInfo("CMD_PREFAB_BOX",
            "Prefab",
            "Create",
            "Create Box".Localize(),
            "Create a box primitive".Localize());

        public static readonly CommandInfo CreateBuiltInPlane = new CommandInfo("CMD_PREFAB_PLANE",
            "Prefab",
            "Prefab",
            "Create Plane".Localize(),
            "Create a plane primitive".Localize());

        public static readonly CommandInfo CreateBuiltInSphere = new CommandInfo("CMD_PREFAB_SPHERE",
            "Prefab",
            "Prefab",
            "Create Sphere".Localize(),
            "Create a sphere primitive".Localize());

        public static readonly CommandInfo CreateBuiltInCone = new CommandInfo("CMD_PREFAB_CONE",
            "Prefab",
            "Prefab",
            "Create Cone".Localize(),
            "Create a cone primitive".Localize());

        public static readonly CommandInfo BuildLightmapsCmd = new CommandInfo("CMD_BUILD_LIGHTMAPS",
            "Tools",
            "Tools",
            "Build Lightmaps".Localize(),
            "Generates lightmaps for the scene".Localize());

        public static readonly CommandInfo DuplicateSelectionCmd = new CommandInfo("CMD_DUPLICATE_SELECTED",
            "Create",
            "Create",
            "Duplicate Selected".Localize(),
            "Creates a duplicate of the selection".Localize());
    }
}
