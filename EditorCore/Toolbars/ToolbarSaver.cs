using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorCore.Toolbars
{
    public static class ToolbarSaver
    {
        public static void RestoreToolbarStates(ToolStripContainer container, XmlDocument data)
        {
            // TODO:  Instead of bailing here, which means we will ignore saved settings, we need to either
            //        cache the settings we're given here until we CAN apply them, or else we need to
            //        ensure that we don't register for settings until we're able to process them since we
            //        do not know when the settings service will decide to deliver values to us.
            //        A third option would be to continue to bail here, but FORCE a redelivery of the settings
            //        later when we're ready for them (ie: OnLoad).
            //        See also other comments in this file labled SCREAM_TOOLBAR_STATE_ISSUE
            var comparer = new ElementSortComparer<XmlElement>();
            Dictionary<string, ToolStrip> toolStrips = new Dictionary<string, ToolStrip>();
            Dictionary<string, ToolStripItem> toolStripItems = new Dictionary<string, ToolStripItem>();

            try
            {
                // build dictionaries of existing toolstrips and items
                PrepareLoadPanelState(container.TopToolStripPanel, toolStrips, toolStripItems);
                PrepareLoadPanelState(container.LeftToolStripPanel, toolStrips, toolStripItems);
                PrepareLoadPanelState(container.BottomToolStripPanel, toolStrips, toolStripItems);
                PrepareLoadPanelState(container.RightToolStripPanel, toolStrips, toolStripItems);

                container.SuspendLayout();

                XmlDocument xmlDoc = data;

                XmlElement root = xmlDoc.DocumentElement;
                if (root == null || root.Name != "ToolStripContainerSettings")
                    throw new InvalidOperationException("Invalid Toolstrip settings");

                // walk xml to restore matching toolstrips and items to their previous state
                XmlNodeList Panels = root.SelectNodes("ToolStripPanel");
                foreach (XmlElement panelElement in Panels)
                {
                    string panelName = panelElement.GetAttribute("Name");
                    ToolStripPanel panel;
                    if (panelName == "TopToolStripPanel")
                        panel = container.TopToolStripPanel;
                    else if (panelName == "LeftToolStripPanel")
                        panel = container.LeftToolStripPanel;
                    else if (panelName == "BottomToolStripPanel")
                        panel = container.BottomToolStripPanel;
                    else if (panelName == "RightToolStripPanel")
                        panel = container.RightToolStripPanel;
                    else
                        continue;

                    List<XmlElement> stripElements = new List<XmlElement>();
                    foreach (XmlElement toolStripElement in panelElement.ChildNodes)
                        stripElements.Add(toolStripElement);

                    // sort toolstrips on Y then X.
                    // create list of ToolStrips for each row.
                    // rows are sorted.
                    SortedDictionary<int, List<XmlElement>>
                        rowOrder = new SortedDictionary<int, List<XmlElement>>();
                    foreach (XmlElement toolStripElement in stripElements)
                    {
                        string[] location = toolStripElement.GetAttribute("Location").Split(',');
                        int yloc = int.Parse(location[1]);
                        List<XmlElement> toolstripList;
                        if (!rowOrder.TryGetValue(yloc, out toolstripList))
                        {
                            toolstripList = new List<XmlElement>();
                            rowOrder.Add(yloc, toolstripList);
                        }
                        toolstripList.Add(toolStripElement);
                    }
                    // sort on x.
                    foreach (var toolstripList in rowOrder.Values)
                        toolstripList.Sort(comparer);

                    int cIndex = 0; // keeps track of the toolstrip's index in Controls Collection.
                    int prevHeight = 0; // height of the previous toolstrip in row order.
                    // Don't use persisted y-position directly, instead compute y-position from cumulative heights of all the previous ToolStrips.  
                    // Persisted Y-position is only used for determining Row number for each ToolStrip.
                    int yPos = rowOrder.Count == 0 ? 0 : rowOrder.Keys.First();
                    foreach (var toolStripElements in rowOrder.Values)
                    {
                        yPos += prevHeight;
                        foreach (XmlElement toolStripElement in toolStripElements)
                        {
                            string toolStripName = toolStripElement.GetAttribute("Name");
                            ToolStrip toolStrip;
                            if (!toolStrips.TryGetValue(toolStripName, out toolStrip))
                                continue;

                            toolStrip.Parent.Controls.Remove(toolStrip);
                            panel.Controls.Add(toolStrip);
                            panel.Controls.SetChildIndex(toolStrip, cIndex++);
                            if (toolStrip.Height > prevHeight) prevHeight = toolStrip.Height;
                            string[] coords = toolStripElement.GetAttribute("Location").Split(',');
                            int xPos = int.Parse(coords[0]);
                            toolStrip.Location = new Point(xPos, yPos);
                            XmlNodeList itemNodes = toolStripElement.ChildNodes;
                            int j = 0;
                            foreach (XmlElement itemElement in itemNodes)
                            {
                                string itemName = itemElement.GetAttribute("Name");
                                ToolStripItem item;
                                if (toolStripItems.TryGetValue(itemName, out item))
                                {
                                    item.Owner.Items.Remove(item);
                                    toolStrip.Items.Insert(j, item);

                                    // ToolStripItem has two visibility properties: Visible and Available. 
                                    // Visible indicates whether the item is displayed,  
                                    // Available indicates whether the ToolStripItem should be placed on a ToolStrip.
                                    // Use Available property here for toolstrip layout.
                                    string visible = itemElement.GetAttribute("Visible");
                                    if (visible == "false")
                                        item.Available = false;
                                    else
                                        item.Available = true;

                                    if (item is ToolStripButton)
                                    {
                                        var button = item as ToolStripButton;
                                        // Only if the attribute is available and is set to "true"...
                                        string isChecked = itemElement.GetAttribute("Checked");
                                        if (isChecked == "true")
                                            button.Checked = true;
                                    }

                                    j++;
                                }
                            }
                        } // foreach (XmlElement toolStripElement in toolStripElements)
                    } // foreach (var toolStripElements in rowOrder.Values)                       
                } // foreach (XmlElement panelElement in Panels)
            }
            finally
            {
                foreach (ToolStrip toolStrip in toolStrips.Values)
                    toolStrip.ResumeLayout(true);

                container.TopToolStripPanel.ResumeLayout(true);
                container.LeftToolStripPanel.ResumeLayout(true);
                container.BottomToolStripPanel.ResumeLayout(true);
                container.RightToolStripPanel.ResumeLayout(true);
                container.ResumeLayout(true);
            }
        }

        public static XmlDocument SaveToolbarStates(ToolStripContainer container)
        {
            XmlDocument ret = new XmlDocument();

            XmlElement docRoot = ret.CreateElement("ToolStripContainerSettings");
            ret.AppendChild(docRoot);

            SavePanelState(container.LeftToolStripPanel, "LeftToolStripPanel", ret, docRoot);
            SavePanelState(container.TopToolStripPanel, "TopToolStripPanel", ret, docRoot);
            SavePanelState(container.RightToolStripPanel, "RightToolStripPanel", ret, docRoot);
            SavePanelState(container.BottomToolStripPanel, "BottomToolStripPanel", ret, docRoot);

            return ret;
        }

        static void SavePanelState(ToolStripPanel panel, string panelName, XmlDocument xmlDoc, XmlElement root)
        {
            XmlElement panelElement = xmlDoc.CreateElement("ToolStripPanel");
            root.AppendChild(panelElement);
            panelElement.SetAttribute("Name", panelName);

            foreach (ToolStrip toolStrip in panel.Controls)
            {
                // skip invisible or unnamed  toolStrip
                if (!toolStrip.Visible || toolStrip.Name == null || toolStrip.Name.Trim().Length == 0)
                    continue;

                XmlElement toolStripElement = xmlDoc.CreateElement("ToolStrip");
                panelElement.AppendChild(toolStripElement);

                toolStripElement.SetAttribute("Name", toolStrip.Name);
                toolStripElement.SetAttribute("Location", string.Format("{0},{1}", toolStrip.Location.X, toolStrip.Location.Y));

                // don't persist menu strip items
                if (toolStrip is MenuStrip)
                    continue;

                foreach (ToolStripItem item in toolStrip.Items)
                {
                    // skip unnamed tool strip items
                    if (item.Name == null || item.Name.Trim().Length == 0)
                        continue;

                    XmlElement itemElement = xmlDoc.CreateElement("ToolStripItem");
                    toolStripElement.AppendChild(itemElement);

                    itemElement.SetAttribute("Name", item.Name);

                    // Buttons that are in the Overflow area have Visible set to false but Available to true.
                    // We want to identify buttons that have been intentially made not visible
                    //  through use of the Customize drop-down menu. See AddCustomizationDropDown()
                    //  in CommandService.
                    if (!(item is ToolStripDropDownButton) &&
                        item.Placement == ToolStripItemPlacement.None &&
                        item.Available == false)
                    {
                        itemElement.SetAttribute("Visible", "false");
                    }

                    if ((item is ToolStripButton) && ((ToolStripButton)item).Checked)
                        itemElement.SetAttribute("Checked", "true");
                }
            }
        }

        static void PrepareLoadPanelState(ToolStripPanel panel, Dictionary<string, ToolStrip> toolStrips, Dictionary<string, ToolStripItem> toolStripItems)
        {
            panel.SuspendLayout();

            foreach (ToolStrip toolStrip in panel.Controls)
            {
                // skip invisible toolstrip
                if (!toolStrip.Visible)
                    continue;
                toolStrips.Add(toolStrip.Name, toolStrip);
                toolStrip.SuspendLayout();
                foreach (ToolStripItem toolStripItem in toolStrip.Items)
                    toolStripItems.Add(toolStripItem.Name, toolStripItem);
            }
        }
    }

    class ElementSortComparer<T> : IComparer<XmlElement>
    {
        int IComparer<XmlElement>.Compare(XmlElement element1, XmlElement element2)
        {
            string[] coords1 = element1.GetAttribute("Location").Split(',');
            string[] coords2 = element2.GetAttribute("Location").Split(',');
            return int.Parse(coords1[0]) - int.Parse(coords2[0]);
        }
    }
}
