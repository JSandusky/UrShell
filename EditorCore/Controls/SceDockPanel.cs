using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace EditorCore.Controls
{
    public class SceDockPanel : WeifenLuo.WinFormsUI.Docking.DockPanel
    {
        public DockColors DockColors
        {
            get
            {
                if (Skin == null)
                    Skin = new DockPanelSkin();

                return new DockColors()
                {
                    AutoHideDockStripGradient = GetControlGradientFromDockPanelGradient(Skin.AutoHideStripSkin.DockStripGradient),
                    AutoHideTabGradient = GetControlGradientFromDockPanelGradient(Skin.AutoHideStripSkin.TabGradient),
                    DocumentActiveTabGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient),
                    DocumentInactiveTabGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.DocumentGradient.InactiveTabGradient),
                    DocumentDockStripGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient),
                    ToolWindowActiveTabGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.ToolWindowGradient.ActiveTabGradient),
                    ToolWindowInactiveTabGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient),
                    ToolWindowActiveCaptionGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient),
                    ToolWindowInactiveCaptionGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient),
                    ToolWindowDockStripGradient = GetControlGradientFromDockPanelGradient(Skin.DockPaneStripSkin.ToolWindowGradient.DockStripGradient)
                };
            }
            set
            {
                if (Skin == null)
                    Skin = new DockPanelSkin();

                Skin.AutoHideStripSkin = new AutoHideStripSkin()
                {
                    DockStripGradient = GetTabGradientFromControlGradient(value.AutoHideDockStripGradient),
                    TabGradient = GetTabGradientFromControlGradient(value.AutoHideTabGradient)
                };

                Skin.DockPaneStripSkin = new DockPaneStripSkin()
                {
                    DocumentGradient = new DockPaneStripGradient()
                    {
                        ActiveTabGradient = GetTabGradientFromControlGradient(value.DocumentActiveTabGradient),
                        InactiveTabGradient = GetTabGradientFromControlGradient(value.DocumentInactiveTabGradient),
                        DockStripGradient = GetTabGradientFromControlGradient(value.DocumentDockStripGradient)
                    },
                    ToolWindowGradient = new DockPaneStripToolWindowGradient()
                    {
                        ActiveTabGradient = GetTabGradientFromControlGradient(value.ToolWindowActiveTabGradient),
                        InactiveTabGradient = GetTabGradientFromControlGradient(value.ToolWindowInactiveTabGradient),
                        ActiveCaptionGradient = GetTabGradientFromControlGradient(value.ToolWindowActiveCaptionGradient),
                        InactiveCaptionGradient = GetTabGradientFromControlGradient(value.ToolWindowInactiveCaptionGradient),
                        DockStripGradient = GetTabGradientFromControlGradient(value.ToolWindowDockStripGradient)
                    }
                };
            }
        }

        private static TabGradient GetTabGradientFromControlGradient(ControlGradient controlGradient)
        {
            return new TabGradient()
            {
                StartColor = controlGradient.StartColor,
                EndColor = controlGradient.EndColor,
                LinearGradientMode = controlGradient.LinearGradientMode,
                TextColor = controlGradient.TextColor
            };
        }

        private static ControlGradient GetControlGradientFromDockPanelGradient(DockPanelGradient dockPanelGradient)
        {
            var controlGradient = new ControlGradient()
            {
                StartColor = dockPanelGradient.StartColor,
                EndColor = dockPanelGradient.EndColor,
                LinearGradientMode = dockPanelGradient.LinearGradientMode
            };

            var tabGradient = dockPanelGradient as TabGradient;
            if (tabGradient != null)
                controlGradient.TextColor = tabGradient.TextColor;

            return controlGradient;
        }
    }
}
