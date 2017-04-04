namespace EditorWinForms.Documents.ObjectAnim
{
    partial class ObjectAnimView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topSplitter = new System.Windows.Forms.SplitContainer();
            this.subSplitter = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitter)).BeginInit();
            this.topSplitter.Panel2.SuspendLayout();
            this.topSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subSplitter)).BeginInit();
            this.subSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // topSplitter
            // 
            this.topSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitter.Location = new System.Drawing.Point(0, 0);
            this.topSplitter.Name = "topSplitter";
            this.topSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topSplitter.Panel2
            // 
            this.topSplitter.Panel2.Controls.Add(this.subSplitter);
            this.topSplitter.Size = new System.Drawing.Size(564, 418);
            this.topSplitter.SplitterDistance = 188;
            this.topSplitter.TabIndex = 0;
            // 
            // subSplitter
            // 
            this.subSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subSplitter.Location = new System.Drawing.Point(0, 0);
            this.subSplitter.Name = "subSplitter";
            this.subSplitter.Size = new System.Drawing.Size(564, 226);
            this.subSplitter.SplitterDistance = 188;
            this.subSplitter.TabIndex = 0;
            // 
            // ObjectAnimView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.topSplitter);
            this.Name = "ObjectAnimView";
            this.Size = new System.Drawing.Size(564, 418);
            this.topSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitter)).EndInit();
            this.topSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.subSplitter)).EndInit();
            this.subSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer topSplitter;
        private System.Windows.Forms.SplitContainer subSplitter;
    }
}
