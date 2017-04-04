namespace EditorCore.Dialogs
{
    partial class SettingsDlg
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topSplitter = new System.Windows.Forms.SplitContainer();
            this.propertiesSplitter = new System.Windows.Forms.SplitContainer();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitter)).BeginInit();
            this.topSplitter.Panel2.SuspendLayout();
            this.topSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesSplitter)).BeginInit();
            this.propertiesSplitter.Panel2.SuspendLayout();
            this.propertiesSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // topSplitter
            // 
            this.topSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitter.Location = new System.Drawing.Point(0, 0);
            this.topSplitter.Name = "topSplitter";
            // 
            // topSplitter.Panel2
            // 
            this.topSplitter.Panel2.Controls.Add(this.propertiesSplitter);
            this.topSplitter.Size = new System.Drawing.Size(604, 348);
            this.topSplitter.SplitterDistance = 201;
            this.topSplitter.TabIndex = 0;
            // 
            // propertiesSplitter
            // 
            this.propertiesSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.propertiesSplitter.IsSplitterFixed = true;
            this.propertiesSplitter.Location = new System.Drawing.Point(0, 0);
            this.propertiesSplitter.Name = "propertiesSplitter";
            this.propertiesSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // propertiesSplitter.Panel2
            // 
            this.propertiesSplitter.Panel2.Controls.Add(this.btnClose);
            this.propertiesSplitter.Size = new System.Drawing.Size(399, 348);
            this.propertiesSplitter.SplitterDistance = 305;
            this.propertiesSplitter.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(272, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 348);
            this.Controls.Add(this.topSplitter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDlg";
            this.Text = "Settings";
            this.topSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitter)).EndInit();
            this.topSplitter.ResumeLayout(false);
            this.propertiesSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesSplitter)).EndInit();
            this.propertiesSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer topSplitter;
        private System.Windows.Forms.SplitContainer propertiesSplitter;
        private System.Windows.Forms.Button btnClose;
    }
}