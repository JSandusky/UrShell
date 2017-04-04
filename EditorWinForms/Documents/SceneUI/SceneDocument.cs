using EditorCore;
using EditorCore.Documents;
using PluginLibrary;
using Sce.Atf;
using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UrhoBackend;

namespace EditorWinForms.Documents.SceneUI
{
    [DocumentFilter("Scenes (*.xml)|*.xml", "Scene")]
    [FileFiltered("Scenes (*.xml)|*.xml")]
    public class SceneDocument : DocumentBase, ISceneDocument, ILoggingDocument, ICommandClient, IHierarchialDocument
    {
        Required<SceneUI.SceneSettings> Settings = new Required<SceneUI.SceneSettings>();

        List<string> LogMessages;
        Scene scene;
        public SceneView Renderer;
        public UrControl Urho3D;

        [OpenDocumentFactory("Scene", new string[] { ".xml" })]
        [HandlesExtensions(new string[] { ".xml" })]
        public static SceneDocument FactoryMethod(string path)
        {
            if (path != null && System.IO.Path.GetExtension(path).Equals(".xml"))
            {
                // Verify it's a valid scene
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    if (!doc.DocumentElement.Name.Equals("scene"))
                    {
                        return null;
                    }
                }
                catch (Exception ex) {
                    ErrorHandler.GetInst().Error(ex);
                }
            }
            SceneDocument ret = new SceneDocument { };
            ret.Renderer = new SceneView("Data/Scripts/Editor.as", ret);
            ret.Urho3D = ret.Renderer.Urho3D;
            if (path != null)
            {
                ret.Urho3D.ExecuteMethod = "void StartForFile(String)";
                ret.Urho3D.ExecuteParams = new VariantVector();
                path = path.Replace(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\", "");
                path = path.Replace("\\", "/");
                ret.Urho3D.ExecuteParams.Add(new Variant(path));
            }
            ret.Urho3D.SubscribeCallback(ret);
            ret.Renderer.Show(MainWindow.inst().DockingPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            return ret;
        }

        [NewDocumentFactory("Scene", "Scenes (*.xml)|*.xml", new string[] { ".xml" })]
        [HandlesExtensions(new string[] { ".xml" })]
        [FileFiltered("Scenes (*.xml)|*.xml")]
        public static SceneDocument NewMethod()
        {
            return FactoryMethod("");
        }

        private SceneDocument()
        {
            LogMessages = new List<string>();
            
        }

        public Scene Scene { 
            get { return scene; } 
            private set { 
                scene = value;
                if (TreeRootChanged != null)
                    TreeRootChanged(this, new EventArgs { });
            } 
        }

        public void ExecuteScript(string code, VariantVector args)
        {
            if (Urho3D != null)
            {
                if (args == null)
                    Urho3D.Execute(code);
                else
                    Urho3D.Execute(code, args);
            }
        }

        [HandleEvent(EventName = "SceneLoaded")]
        public void OnSceneLoaded(uint eventType, VariantMap eventData)
        {
            Scene = eventData.Get("Scene").GetScene();
            if (SceneChanged != null)
                SceneChanged(Scene, new EventArgs { });
            if (TreeRootChanged != null)
                TreeRootChanged(this, new EventArgs { });
        }

        [HandleEvent(EventName = "LogMessage")]
        public void OnLogMessage(uint eventType, UrhoBackend.VariantMap eventData)
        {
            int level = eventData.Get("Level").GetInt();
            string msg = eventData.Get("Message").GetString();
            
            AddLogMessage(msg);
        }

        public event EventHandler NodeSelected;
        [HandleEvent(EventName = "NodeSelected")]
        public void OnNodeSelected(uint hash, VariantMap eventData)
        {
            Node node = eventData.Get("Node").GetNode();
            Component comp = eventData.Get("Component").GetComponent();
            if (comp != null)
            {
                Selection = new object[] { comp };
            }
            else if (node != null)
            {
                Selection = new object[] { node };
            }
        }

        public event EventHandler NodeDeleted;
        [HandleEvent(EventName = "NodeDeleted")]
        public void OnNodeDeleted(uint hash, VariantMap eventData)
        {
            Node node = eventData.Get("Node").GetNode();
            Component comp = eventData.Get("Component").GetComponent();
            if (comp != null)
            {
                if (NodeDeleted != null)
                    NodeDeleted(comp, new EventArgs { });
            }
            else if (node != null)
            {
                if (NodeDeleted != null)
                    NodeDeleted(node, new EventArgs { });
            }
        }

        public List<string> Log {
            get { return LogMessages; }
        }

        public event LogMessageAdded LogAppended;

        public void AddLogMessage(string msg)
        {
            LogMessages.Add(msg);
            if (LogAppended != null)
                LogAppended(this, new EventArgs());
        }

        public event EventHandler SceneChanged;

        static List<ToolStripMenuItem> CreateMenus()
        {
            CommandRegistry.GetInst().AddRange(CommandRegistry.ListFromProperties(typeof(SceneUI.SceneCmd)));
            CommandRegistry.GetInst().AddRange(CommandRegistry.ListFromProperties(typeof(SpaceCmd)));
            return SceneGUI.CreateMenus();
        }

        public bool CanDoCommand(object commandTag)
        {
            if (commandTag.ToString().Equals("CMD_CREATE_LOCAL_NODE"))
                return true;
            else if (commandTag.ToString().Equals("CMD_CREATE_REPLICATED_NODE"))
                return true;
            else if (commandTag.ToString().Equals("SaveCmd"))
                return true;
            else if (commandTag.ToString().Equals("SaveAsCmd"))
                return true;
            return false;
        }

        public void DoCommand(object commandTag)
        {
            if (commandTag.ToString().Equals("CMD_CREATE_LOCAL_NODE"))
            {
                Node nd = Selection.First() as Node;
                Component comp = Selection.First() as Component;
                if (nd != null)
                    nd.CreateChild("", false);
                if (comp != null)
                    comp.GetNode().CreateChild("", false);
            }
            else if (commandTag.ToString().Equals("CMD_CREATE_REPLICATED_NODE"))
            {
                Node nd = Selection.First() as Node;
                Component comp = Selection.First() as Component;
                if (nd != null)
                    nd.CreateChild("", true);
                if (comp != null)
                    comp.GetNode().CreateChild("", true);
            }
            else if (commandTag.ToString().Equals("SaveCmd"))
            {
                if (Uri != null)
                { // Save to our current URI

                }
                else
                { // Needs to actually be a "Save As"
                }
            }
            else if (commandTag.ToString().Equals("SaveAsCmd"))
            {

            }
        }

        public void UpdateCommand(object commandTag, CommandState commandState)
        {
            
        }

        public event EventHandler TreeRootChanged;
        public object TreeRoot
        {
            get { return Scene; }
        }
    }
}
