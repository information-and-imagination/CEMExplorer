namespace CEMExplorer.Controls
{
    partial class ucFileSelector
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Button btnOpenFileExplorer = null!;
        private System.Windows.Forms.TextBox txtFullFileName = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOpenFileExplorer = new System.Windows.Forms.Button();
            this.txtFullFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenFileExplorer
            // 
            this.btnOpenFileExplorer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnOpenFileExplorer.Location = new System.Drawing.Point(590, 0);
            this.btnOpenFileExplorer.Name = "btnOpenFileExplorer";
            this.btnOpenFileExplorer.Size = new System.Drawing.Size(44, 29);
            this.btnOpenFileExplorer.TabIndex = 1;
            this.btnOpenFileExplorer.Text = "...";
            this.btnOpenFileExplorer.UseVisualStyleBackColor = true;
            this.btnOpenFileExplorer.Click += new System.EventHandler(this.btnOpenFileExplorer_Click);
            // 
            // txtFullFileName
            // 
            this.txtFullFileName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtFullFileName.Location = new System.Drawing.Point(0, 1);
            this.txtFullFileName.Name = "txtFullFileName";
            this.txtFullFileName.Size = new System.Drawing.Size(582, 27);
            this.txtFullFileName.TabIndex = 0;
            this.txtFullFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFullFileName_KeyDown);
            // 
            // ucFileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFullFileName);
            this.Controls.Add(this.btnOpenFileExplorer);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Name = "ucFileSelector";
            this.Size = new System.Drawing.Size(634, 30);
            this.Resize += new System.EventHandler(this.ucFileSelector_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
