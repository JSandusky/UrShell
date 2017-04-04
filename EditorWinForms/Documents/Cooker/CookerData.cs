using Sce.Atf.Controls.PropertyEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorWinForms.Documents.Cooker
{
    public interface ICookBase : INotifyPropertyChanged
    {
        string GetName();
        bool AcceptsChild(Type type);
    }

    public abstract class CookTask : Data.DataObject
    {
        bool setWorkingDir_ = false;

        public CookTask()
        {
            Targets = new List<CookTarget>();
        }

        [Browsable(false)]
        public List<CookTarget> Targets { get; private set; }

        [Description("The working directory will be assigned to the current directory being processed")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        public bool SetWorkingDir { get { return setWorkingDir_; } set { setWorkingDir_ = value; PropertyChange("SetWorkingDir"); } }

        public void Cook()
        {
            // Expands groups
            foreach (CookTarget target in Targets)
                FlatRun(target);
        }

        void FlatRun(CookTarget target)
        {
            if (target is CookGroupTarget)
            {
                foreach (CookTarget tgt in ((CookGroupTarget)target).Children)
                    FlatRun(tgt);
            }
            else
                Run(target);
        }

        public abstract void Run(CookTarget item);
    }

    public abstract class CookTarget : Data.DataObject, ICookBase
    {
        public abstract string GetPath();

        public CookTarget()
        {
        }

        public abstract string GetName();
        public abstract bool AcceptsChild(Type type);
    }

    public class CookGroupTarget : CookTarget
    {
        string name_ = "";
        public string Name { get { return name_; } set { name_ = value; PropertyChange("Name"); } }

        public CookGroupTarget()
        {
            Children = new List<CookTarget>();
        }

        public List<CookTarget> Children { get; private set; }

        public override string GetPath()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return string.Format("Group: {0}", Name);
        }

        public override bool AcceptsChild(Type type)
        {
            return typeof(CookTarget).IsAssignableFrom(type);
        }
    }

    public class CookFileTarget : CookTarget
    {
        string path_ = "";

        [Description("Path to the file to process")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.FileUriEditor), typeof(UITypeEditor))]
        public string Path { get { return path_; } set { path_ = value; PropertyChange("Path"); } }

        public override string GetPath() { return Path; }

        public override string GetName()
        {
            return string.Format("File Target: {0}", Path);
        }

        public override bool AcceptsChild(Type type)
        {
            return false;
        }
    }

    public class CookFolderTarget : CookTarget
    {
        string path_ = "";

        [Description("Path to the directory to process")]
        [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(UITypeEditor))]
        public string Path { get { return path_; } set { path_ = value; PropertyChange("Path"); } }

        public override string GetPath() { return Path; }

        public override string GetName()
        {
            return string.Format("Folder Target: {0}", Path);
        }

        public override bool AcceptsChild(Type type)
        {
            return false;
        }
    }

    public abstract class CookFiles : CookTask
    {
        bool recurse_ = false;

        [Description("If path is a directory then all directories and files processed")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        public bool Recurse { get { return recurse_; } set { recurse_ = value; PropertyChange("Recurse"); } }

        public override void Run(CookTarget item)
        {
            Run(item.GetPath());
        }

        void Run(string file)
        {
            if (System.IO.Directory.Exists(file)) //Is it a directory
            {
                foreach (string f in System.IO.Directory.EnumerateFiles(file))
                    Run(f);
                if (Recurse)
                {
                    foreach (string dir in System.IO.Directory.EnumerateDirectories(file))
                        Run(dir);
                }
            }
            else
                RunOperation(file);
        }

        protected abstract void RunOperation(string path);
    }

    /// <summary>
    /// Runs a batch file
    /// </summary>
    public class CookBatch : CookTask, ICookBase
    {
        string file_ = "";

        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.FileUriEditor), typeof(UITypeEditor))]
        public string File { get { return file_; } set { file_ = value; PropertyChange("File"); } }

        public string GetName() { return "Batch: " + File; }

        public override void Run(CookTarget item)
        {
            Interop.ProcessWrapper wrapper = new Interop.ProcessWrapper("cmd.exe");
            wrapper.ProcessStart.Arguments = String.Format("/C {0} {1}", File.ToSpaceQuoted(), item.GetPath().ToSpaceQuoted());
            wrapper.Run();
        }

        public bool AcceptsChild(Type type)
        {
            return typeof(CookTarget).IsAssignableFrom(type);
        }
    }

    public class CookProcess : CookFiles, ICookBase
    {
        string program_;
        string args_;

        public string GetName() { return "Process: " + Program; }

        [Description("Executable process to run")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.FileUriEditor), typeof(UITypeEditor))]
        public string Program { get { return program_; } set { program_ = value; PropertyChange("Program"); } }
        [Description("Commandline arguments to be given to the process")]
        public string Arguments { get { return args_; } set { args_ = value; PropertyChange("Arguments"); } }

        protected override void RunOperation(string path)
        {
            Interop.ProcessWrapper wrapper = new Interop.ProcessWrapper(Program);
            wrapper.ProcessStart.Arguments = Arguments.Contains("{0}") ? String.Format(Arguments, path.ToSpaceQuoted()) : Arguments;
            wrapper.Run();
        }

        public bool AcceptsChild(Type type)
        {
            return typeof(CookTarget).IsAssignableFrom(type);
        }
    }

    public class CookPackageBuilder : CookTask, ICookBase
    {
        string packageName_;
        string outputPath_;
        bool compress_;

        public string GetName() { return "Package: " + PackageName; }

        [Description("Filename of the package (ie. MyPackage.pak)")]
        public string PackageName { get { return packageName_; } set { packageName_ = value; PropertyChange("PackageName"); } }

        [Description("Path at which to write the package")]
        [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(UITypeEditor))]
        public string OutputPath { get { return outputPath_; } set { outputPath_ = value; PropertyChange("OutputPath"); } }

        [Description("Package will use ZLib compression")]
        [Editor(typeof(Sce.Atf.Controls.PropertyEditing.BoolEditor), typeof(IPropertyEditor))]
        public bool Compress { get { return compress_; } set { compress_ = value; PropertyChange("Compress"); } }

        public override void Run(CookTarget item)
        {
            if (!System.IO.Directory.Exists(item.GetPath()))
                return;
            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            appPath = System.IO.Path.Combine(appPath, "bin");
            appPath = System.IO.Path.Combine(appPath, "PackageTool.exe");

            Interop.ProcessWrapper wrapper = new Interop.ProcessWrapper(appPath);
            wrapper.ProcessStart.Arguments = String.Format("{0} {1}", item.GetPath(), PackageName);
            if (Compress)
                wrapper.ProcessStart.Arguments += " -c";
            wrapper.Run();
        }

        public bool AcceptsChild(Type type)
        {
            return typeof(CookTarget).IsAssignableFrom(type);
        }
    }
}
