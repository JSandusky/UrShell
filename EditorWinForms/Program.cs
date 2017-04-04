using Sce.Atf;
using Sce.Atf.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorWinForms
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) 
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            EditorCore.Interfaces.IProgramInitializer.Initialize();

            Application.Idle += Application_Idle;
            Application.ApplicationExit += Application_ApplicationExit;

            Type[] documents = {
                                   typeof(Documents.SceneUI.SceneDocument),
                                   typeof(Documents.Particle.ParticleDocument),
                                   typeof(Documents.Material.MaterialDocument),
                                   typeof(Documents.ObjectAnim.ObjectAnimationDocument),
                                   typeof(Documents.Model.ModelDocument),
                                   typeof(Documents.Cooker.CookerDocument)
                               };
            Type[] panels = {
                                typeof(EditorCore.Panels.HierarchyPanel),
                                typeof(EditorCore.Panels.PropertyEditor),
                                typeof(EditorCore.Panels.PropertyTable),
                                typeof(EditorCore.Panels.LogPanel),
                                typeof(EditorCore.Panels.NotesPanel),
                                typeof(EditorCore.Panels.FileSystemPanel),
                                typeof(EditorCore.Panels.HelpPanel)
                            };

            Application.Run(new EditorCore.MainWindow(documents, panels));
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            EditorCore.ErrorHandler.GetInst().Error(e.ExceptionObject as Exception);
        }

        static void Application_Idle(object sender, EventArgs e)
        {
            
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
        }

        public static void Terminate()
        {
            EditorCore.Interfaces.IProgramTerminated.Terminate();
            Application.Exit();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
