namespace CEMExplorer
{
    partial class frmProjectWizard
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblHeading = null!;
        private System.Windows.Forms.Label lblIntroduction = null!;
        private System.Windows.Forms.TabControl tabProjectActions = null!;
        private System.Windows.Forms.TabPage tabOpen = null!;
        private System.Windows.Forms.TabPage tabCreate = null!;
        private System.Windows.Forms.Label lblOpenFolder = null!;
        private Controls.ucFileSelector openFolderSelector = null!;
        private System.Windows.Forms.Button btnOpen = null!;
        private System.Windows.Forms.Label lblParentFolder = null!;
        private Controls.ucFileSelector createFolderSelector = null!;
        private System.Windows.Forms.Label lblProjectTitle = null!;
        private System.Windows.Forms.TextBox txtProjectTitle = null!;
        private System.Windows.Forms.Label lblAbbreviation = null!;
        private System.Windows.Forms.TextBox txtAbbreviation = null!;
        private System.Windows.Forms.Label lblAbbreviationHelp = null!;
        private System.Windows.Forms.Button btnCreate = null!;
        private System.Windows.Forms.Label lblMessage = null!;
        private System.Windows.Forms.Button btnCancel = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblIntroduction = new System.Windows.Forms.Label();
            this.tabProjectActions = new System.Windows.Forms.TabControl();
            this.tabOpen = new System.Windows.Forms.TabPage();
            this.lblOpenFolder = new System.Windows.Forms.Label();
            this.openFolderSelector = new CEMExplorer.Controls.ucFileSelector();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.lblParentFolder = new System.Windows.Forms.Label();
            this.createFolderSelector = new CEMExplorer.Controls.ucFileSelector();
            this.lblProjectTitle = new System.Windows.Forms.Label();
            this.txtProjectTitle = new System.Windows.Forms.TextBox();
            this.lblAbbreviation = new System.Windows.Forms.Label();
            this.txtAbbreviation = new System.Windows.Forms.TextBox();
            this.lblAbbreviationHelp = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabProjectActions.SuspendLayout();
            this.tabOpen.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.SuspendLayout();
            // lblHeading
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeading.Location = new System.Drawing.Point(22, 18);
            this.lblHeading.Text = "CEM Explorer";
            // lblIntroduction
            this.lblIntroduction.AutoSize = true;
            this.lblIntroduction.Location = new System.Drawing.Point(26, 59);
            this.lblIntroduction.Text = "Open an existing CEM project or create a new one from the project skeleton.";
            // tabProjectActions
            this.tabProjectActions.Controls.Add(this.tabOpen);
            this.tabProjectActions.Controls.Add(this.tabCreate);
            this.tabProjectActions.Location = new System.Drawing.Point(26, 94);
            this.tabProjectActions.Name = "tabProjectActions";
            this.tabProjectActions.SelectedIndex = 0;
            this.tabProjectActions.Size = new System.Drawing.Size(650, 283);
            // tabOpen
            this.tabOpen.Controls.Add(this.lblOpenFolder);
            this.tabOpen.Controls.Add(this.openFolderSelector);
            this.tabOpen.Controls.Add(this.btnOpen);
            this.tabOpen.Location = new System.Drawing.Point(4, 29);
            this.tabOpen.Padding = new System.Windows.Forms.Padding(18);
            this.tabOpen.Text = "Open Existing Project";
            this.tabOpen.UseVisualStyleBackColor = true;
            // lblOpenFolder
            this.lblOpenFolder.AutoSize = true;
            this.lblOpenFolder.Location = new System.Drawing.Point(20, 29);
            this.lblOpenFolder.Text = "CEM project folder:";
            // openFolderSelector
            this.openFolderSelector.FileType = "";
            this.openFolderSelector.Location = new System.Drawing.Point(23, 57);
            this.openFolderSelector.Name = "openFolderSelector";
            this.openFolderSelector.SelectFolder = true;
            this.openFolderSelector.Size = new System.Drawing.Size(580, 30);
            this.openFolderSelector.FileNameChanged += new System.EventHandler(this.selectionChanged);
            // btnOpen
            this.btnOpen.Location = new System.Drawing.Point(473, 174);
            this.btnOpen.Size = new System.Drawing.Size(130, 38);
            this.btnOpen.Text = "Open Project";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // tabCreate
            this.tabCreate.Controls.Add(this.lblParentFolder);
            this.tabCreate.Controls.Add(this.createFolderSelector);
            this.tabCreate.Controls.Add(this.lblProjectTitle);
            this.tabCreate.Controls.Add(this.txtProjectTitle);
            this.tabCreate.Controls.Add(this.lblAbbreviation);
            this.tabCreate.Controls.Add(this.txtAbbreviation);
            this.tabCreate.Controls.Add(this.lblAbbreviationHelp);
            this.tabCreate.Controls.Add(this.btnCreate);
            this.tabCreate.Location = new System.Drawing.Point(4, 29);
            this.tabCreate.Padding = new System.Windows.Forms.Padding(18);
            this.tabCreate.Text = "Create New Project";
            this.tabCreate.UseVisualStyleBackColor = true;
            // create fields
            this.lblParentFolder.AutoSize = true;
            this.lblParentFolder.Location = new System.Drawing.Point(20, 16);
            this.lblParentFolder.Text = "Parent folder:";
            this.createFolderSelector.FileType = "";
            this.createFolderSelector.Location = new System.Drawing.Point(23, 41);
            this.createFolderSelector.Name = "createFolderSelector";
            this.createFolderSelector.SelectFolder = true;
            this.createFolderSelector.Size = new System.Drawing.Size(580, 30);
            this.createFolderSelector.FileNameChanged += new System.EventHandler(this.selectionChanged);
            this.lblProjectTitle.AutoSize = true;
            this.lblProjectTitle.Location = new System.Drawing.Point(20, 84);
            this.lblProjectTitle.Text = "Project title:";
            this.txtProjectTitle.Location = new System.Drawing.Point(145, 80);
            this.txtProjectTitle.Size = new System.Drawing.Size(458, 27);
            this.txtProjectTitle.TextChanged += new System.EventHandler(this.selectionChanged);
            this.lblAbbreviation.AutoSize = true;
            this.lblAbbreviation.Location = new System.Drawing.Point(20, 124);
            this.lblAbbreviation.Text = "Abbreviation:";
            this.txtAbbreviation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAbbreviation.Location = new System.Drawing.Point(145, 120);
            this.txtAbbreviation.MaxLength = 16;
            this.txtAbbreviation.Size = new System.Drawing.Size(160, 27);
            this.txtAbbreviation.TextChanged += new System.EventHandler(this.selectionChanged);
            this.lblAbbreviationHelp.AutoSize = true;
            this.lblAbbreviationHelp.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAbbreviationHelp.Location = new System.Drawing.Point(315, 123);
            this.lblAbbreviationHelp.Text = "Replaces SKLTN in the skeleton";
            this.btnCreate.Location = new System.Drawing.Point(453, 174);
            this.btnCreate.Size = new System.Drawing.Size(150, 38);
            this.btnCreate.Text = "Create Project";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // message and cancel
            this.lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            this.lblMessage.Location = new System.Drawing.Point(26, 389);
            this.lblMessage.Size = new System.Drawing.Size(520, 48);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(572, 395);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // frmProjectWizard
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(702, 452);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblIntroduction);
            this.Controls.Add(this.tabProjectActions);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CEM Explorer — Open or Create Project";
            this.tabProjectActions.ResumeLayout(false);
            this.tabOpen.ResumeLayout(false);
            this.tabOpen.PerformLayout();
            this.tabCreate.ResumeLayout(false);
            this.tabCreate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
