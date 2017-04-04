using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorCore.Controls
{
    public partial class MarkDownViewer : UserControl
    {
        Data.History<string> history_ = new Data.History<string>();

        string backingMarkdown_;

        public MarkDownViewer()
        {
            InitializeComponent();
            webBrowser.AllowNavigation = true;
            webBrowser.Navigating += webBrowser_Navigating;
        }

        public event EventHandler Navigating;

        void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string part = e.Url.OriginalString;
            if (part.Contains(":"))
            {
                string sub = part.Split(':')[1];
                if (!sub.Equals("blank"))
                {
                    if (Navigating != null)
                        Navigating(sub, new EventArgs { });
                }
            }
        }

        public void GoBack()
        {
            if (history_.HasPast)
                SetMarkdown(history_.Past());
        }

        public void GoForward()
        {
            if (history_.HasFuture)
                SetMarkdown(history_.Future());
        }

        public string Markdown
        {
            get
            {
                return backingMarkdown_;
            }

            set
            {
                backingMarkdown_ = value;
                history_.Add(value);
                SetMarkdown(value);
            }
        }

        void SetMarkdown(string value)
        {
            webBrowser.Navigate("about:blank");
            MarkdownSharp.Markdown markdown = new MarkdownSharp.Markdown(false);
            string trans = "<html><body>" + markdown.Transform(value) + "</body></html>"; ;
            if (webBrowser.Document != null)
                webBrowser.Document.Write(string.Empty);
            webBrowser.DocumentText = trans;
        }
    }
}
