using Sce.Atf.Applications;
using Sce.Atf.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Sce.Atf;

namespace EditorCore
{
    public static class ApplicationCmd
    {
        public static readonly CommandInfo SaveCmd = new CommandInfo("SaveCmd",
            "File",
            "File",
            "Save".Localize(),
            "Saves the active document".Localize())
            {
                Visibility = CommandVisibility.ApplicationMenu
            };

        public static readonly CommandInfo SaveAsCmd = new CommandInfo("SaveAsCmd",
            "File",
            "File",
            "Save As".Localize(),
            "Saves the active document under a new name".Localize())
            {
                Visibility = CommandVisibility.ApplicationMenu
            };

        public static readonly CommandInfo CloseCmd = new CommandInfo("CloseCmd",
            "File",
            "File",
            "Close".Localize(),
            "Closes the active document, prompting to save if it has been edited".Localize())
            {
                Visibility = CommandVisibility.ApplicationMenu
            };

        public static readonly CommandInfo ExitCmd = new CommandInfo("ExitCmd",
            "File",
            "File",
            "Exit".Localize(),
            "Terminates the application".Localize())
            {
                Visibility = CommandVisibility.ApplicationMenu
            };
    }

    public class CommandRegistry : List<CommandInfo>
    {
        Dictionary<CommandInfo, List<Control>> userInterfaces_= new Dictionary<CommandInfo,List<Control>>();
        static CommandRegistry inst_;


        public static CommandRegistry GetInst() {
            if (inst_ == null)
                inst_ = new CommandRegistry();
            return inst_;
        }

        CommandRegistry()
        {
            Add(ApplicationCmd.SaveCmd);
            Add(ApplicationCmd.SaveAsCmd);
            Add(ApplicationCmd.CloseCmd);
            Add(ApplicationCmd.ExitCmd);
        }

        public void Add(params CommandInfo[] commands)
        {
            AddRange(commands);
        }

        public void AddUI(CommandInfo cmd, Control control)
        {
            if (!userInterfaces_.ContainsKey(cmd))
                userInterfaces_[cmd] = new List<Control>();
            userInterfaces_[cmd].Add(control);
        }

        public List<Control> GetUserInterfaces(CommandInfo key)
        {
            if (userInterfaces_.ContainsKey(key))
                return userInterfaces_[key];
            return null;
        }

        public CommandInfo GetCommand(System.Windows.Forms.Keys keys)
        {
            foreach (CommandInfo ci in this)
            {
                foreach (Sce.Atf.Input.Keys shortcut in ci.Shortcuts)
                {
                    if (shortcut == Sce.Atf.Input.Keys.None)
                        continue;
                    int keyValue = (int)keys;
                    if ((((int)shortcut) == keyValue))
                        return ci;
                }
            }
            return null;
        }

        public List<System.Windows.Forms.ToolStrip> GetToolstrips()
        {
            return Toolbars.ToolbarBuilder.BuildToolbars(this);
        }

        public List<Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut> GetShortcuts()
        {
            List<Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut> ret = new List<Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut>();
            IEnumerable< IGrouping<object, CommandInfo> > groups = this.GroupBy(ci => ci.GroupTag);
            IEnumerable<CommandInfo> grouped = groups.SelectMany(group => group);

            foreach (CommandInfo ci in grouped)
            {
                Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut shortcut = new Sce.Atf.Controls.CustomizeKeyboardDialog.Shortcut(ci, ci.MenuText);
                ret.Add(shortcut);
            }

            return ret;
        }

        public List<CommandInfo> GetCommands(params string[] names)
        {
            List<CommandInfo> ret = new List<CommandInfo>();
            foreach (CommandInfo ci in this)
            {
                if (names.Contains(ci.MenuText))
                    ret.Add(ci);
            }
            return ret;
        }

        public CommandInfo GetCommand(string name)
        {
            foreach (CommandInfo ci in this)
            {
                if (ci.MenuText.Equals(name))
                    return ci;
            }
            return null;
        }

        public List<CommandInfo> QueryCommands(string query)
        {
            List<CommandInfo> ret = new List<CommandInfo>();
            foreach (CommandInfo ci in this)
            {
                if (ci.MenuText.Contains(query))
                    ret.Add(ci);
            }
            ret.Sort((lhs, rhs) =>
            {
                int leftDistance = Data.Levenstein.Compute(lhs.MenuText.ToLowerInvariant(), query.ToLowerInvariant());
                int rightDistance = Data.Levenstein.Compute(rhs.MenuText.ToLowerInvariant(), query.ToLowerInvariant());
                if (leftDistance < rightDistance)
                    return -1;
                if (rightDistance < leftDistance)
                    return 1;
                return 0;
            });
            return ret;
        }

        /// <summary>
        /// Scan's the given type for static CommandInfo properties.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<CommandInfo> ListFromProperties(Type type)
        {
            List<CommandInfo> ret = new List<CommandInfo>();
            foreach (FieldInfo pi in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (pi.FieldType == typeof(CommandInfo))
                {
                    CommandInfo ci = pi.GetValue(null) as CommandInfo;
                    if (ci != null)
                        ret.Add(ci);
                }
            }
            return ret;
        }

        /// <summary>
        /// Save keyboard shortcuts to appdata
        /// </summary>
        internal static void SaveToFile()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appData = System.IO.Path.Combine(appData, Assembly.GetEntryAssembly().GetName().Name);
            appData = System.IO.Path.Combine(appData, "keybindings.xml");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            XmlElement root = xmlDoc.CreateElement("Shortcuts");
            xmlDoc.AppendChild(root);

            foreach (CommandInfo info in inst_)
            {
                // We don't want to save shortcuts that are at their default value since this
                //  prevents the default from being changed programmatically or via DefaultSettings.xml.
                //  http://forums.ship.scea.com/jive/thread.jspa?messageID=51034
                if (info.ShortcutsAreDefault)
                    continue;

                int numShortcuts = 0;
                foreach (Sce.Atf.Input.Keys k in info.Shortcuts)
                {
                    XmlElement elem = xmlDoc.CreateElement("shortcut");
                    elem.SetAttribute("name", info.CommandTag.ToString());
                    elem.SetAttribute("value", k.ToString());
                    root.AppendChild(elem);
                    numShortcuts++;
                }

                if (numShortcuts < 1)
                {
                    XmlElement elem = xmlDoc.CreateElement("shortcut");
                    elem.SetAttribute("name", info.CommandTag.ToString());
                    elem.SetAttribute("value", Sce.Atf.Input.Keys.None.ToString());
                    root.AppendChild(elem);
                }
            }

            xmlDoc.Save(appData);
        }

        /// <summary>
        /// Restore keyboard shortcuts
        /// </summary>
        internal static void Restore()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appData = System.IO.Path.Combine(appData, Assembly.GetEntryAssembly().GetName().Name);
            appData = System.IO.Path.Combine(appData, "keybindings.xml");
            
            if (System.IO.File.Exists(appData))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(appData);
                XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("shortcut");
                if (nodes == null || nodes.Count == 0)
                    return;

                Dictionary<string, CommandInfo> commands = new Dictionary<string, CommandInfo>(inst_.Count);
                foreach (CommandInfo info in inst_)
                {
                    string commandPath = info.CommandTag.ToString();
                    commands.Add(commandPath, info);
                }

                Dictionary<CommandInfo, CommandInfo> changedCommands = new Dictionary<CommandInfo, CommandInfo>(inst_.Count);

                // m_shortcuts contains the default shortcuts currently. We need to override the defaults with
                //  the user's preferences. The preference file does not necessarily contain all shortcuts and
                //  some of the shortcuts may be blank (i.e., Keys.None).
                foreach (XmlElement elem in nodes)
                {
                    string strCmdTag = elem.GetAttribute("name"); //the command tag or "path", made up of menu and command name
                    string strShortcut = elem.GetAttribute("value");

                    if (commands.ContainsKey(strCmdTag))
                    {
                        // Blow away any old shortcuts before adding the first new one
                        CommandInfo cmdInfo = commands[strCmdTag];
                        if (!changedCommands.ContainsKey(cmdInfo))
                        {
                            List<Sce.Atf.Input.Keys> shortcuts = new List<Sce.Atf.Input.Keys>(cmdInfo.Shortcuts);
                            //\todo foreach (Sce.Atf.Input.Keys k in shortcuts)
                            //\todo     EraseShortcut(k);
                            changedCommands.Add(cmdInfo, cmdInfo);
                        }
                        Sce.Atf.Input.Keys shortcut = (Sce.Atf.Input.Keys)Enum.Parse(typeof(Sce.Atf.Input.Keys), strShortcut);
                        shortcut = Sce.Atf.Input.KeysUtil.NumPadToNum(shortcut);
                        //\todo cmdInfo.Shortcuts = shortcut;
                    }
                }
            }
        }
    }
}
