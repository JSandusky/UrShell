using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorCore.Interop
{
    public delegate void TaskFunction();

    public class TaskObject
    {
        public string Name { get; set; }
        public TaskFunction Task {get; set; }
    }

    public class TaskWorker
    {
        BackgroundWorker worker = new BackgroundWorker();
        Sce.Atf.Controls.ProgressDialog progressDialog;
        List<TaskObject> tasks = new List<TaskObject>();

        public TaskWorker(params TaskObject[] cmds)
        {
            progressDialog = new Sce.Atf.Controls.ProgressDialog();
            progressDialog.Show();
            tasks.AddRange(cmds);
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            worker.WorkerReportsProgress = true;
        }

        public bool Complete
        {
            get
            {
                return !worker.IsBusy;
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressDialog.Hide();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressDialog.Label = e.UserState as string;
            progressDialog.Percent = e.ProgressPercentage;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (TaskObject obj in tasks)
            {
                int prog = (tasks.IndexOf(obj) / tasks.Count) * 100;
                worker.ReportProgress(prog, obj.Name);
                obj.Task();
            }
        }
    }
}
