using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf;

namespace EditorWinForms.Documents
{
    public static class AnimCmd
    {
        public static readonly CommandInfo PlayCmd = new CommandInfo("PlayCmd",
            "Animation",
            "Animation",
            "Play".Localize(),
            "Starts or resumes animation playback".Localize())
            {
                Visibility = CommandVisibility.Toolbar
            };

        public static readonly CommandInfo PauseCmd = new CommandInfo("PauseCmd",
            "Animation",
            "Animation",
            "Pause".Localize(),
            "Pauses active animation playback".Localize())
            {
                Visibility = CommandVisibility.Toolbar
            };

        public static readonly CommandInfo StopCmd = new CommandInfo("StopCmd",
            "Animation",
            "Animation",
            "Stop".Localize(),
            "Halts and resets animation playback".Localize())
            {
                Visibility = CommandVisibility.Toolbar
            };
    }
}
