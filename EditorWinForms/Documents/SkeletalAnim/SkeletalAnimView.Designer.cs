namespace EditorWinForms.Documents.SkeletalAnim
{
    partial class SkeletalAnimView
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
            this.topSplit = new System.Windows.Forms.SplitContainer();
            this.bottomSplit = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.topSplit)).BeginInit();
            this.topSplit.Panel2.SuspendLayout();
            this.topSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplit)).BeginInit();
            this.bottomSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // topSplit
            // 
            this.topSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplit.Location = new System.Drawing.Point(0, 0);
            this.topSplit.Name = "topSplit";
            this.topSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topSplit.Panel2
            // 
            this.topSplit.Panel2.Controls.Add(this.bottomSplit);
            this.topSplit.Size = new System.Drawing.Size(425, 366);
            this.topSplit.SplitterDistance = 242;
            this.topSplit.TabIndex = 0;
            // 
            // bottomSplit
            // 
            this.bottomSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplit.Location = new System.Drawing.Point(0, 0);
            this.bottomSplit.Name = "bottomSplit";
            this.bottomSplit.Size = new System.Drawing.Size(425, 120);
            this.bottomSplit.SplitterDistance = 141;
            this.bottomSplit.TabIndex = 0;
            // 
            // SkeletalAnimView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.topSplit);
            this.Name = "SkeletalAnimView";
            this.Size = new System.Drawing.Size(425, 366);
            this.topSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplit)).EndInit();
            this.topSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplit)).EndInit();
            this.bottomSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer topSplit;
        private System.Windows.Forms.SplitContainer bottomSplit;
    }
}
