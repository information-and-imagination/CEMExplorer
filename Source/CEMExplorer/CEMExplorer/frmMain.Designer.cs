namespace CEMExplorer
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer? components = null;
        private Controls.ucFileSelector fileSelector = null!;
        private System.Windows.Forms.Label lblRootFolder = null!;
        private System.Windows.Forms.Label lblTitle = null!;
        private System.Windows.Forms.TextBox txtTitle = null!;
        private System.Windows.Forms.Button btnSetup = null!;
        private System.Windows.Forms.SplitContainer splitMain = null!;
        private System.Windows.Forms.Label lblStructure = null!;
        private System.Windows.Forms.TreeView tvProject = null!;
        private System.Windows.Forms.Label lblDetail = null!;
        private System.Windows.Forms.Panel pnlDetail = null!;
        private System.Windows.Forms.ListView lvFolder = null!;
        private System.Windows.Forms.ColumnHeader colName = null!;
        private System.Windows.Forms.ColumnHeader colType = null!;
        private System.Windows.Forms.ColumnHeader colSize = null!;
        private System.Windows.Forms.ColumnHeader colModified = null!;
        private System.Windows.Forms.TextBox txtFileContents = null!;
        private System.Windows.Forms.Panel pnlOutline = null!;
        private System.Windows.Forms.FlowLayoutPanel pnlOutlineCommands = null!;
        private System.Windows.Forms.Button btnOutlineAddChild = null!;
        private System.Windows.Forms.Button btnOutlineAddSibling = null!;
        private System.Windows.Forms.Button btnOutlineRename = null!;
        private System.Windows.Forms.Button btnOutlineRemove = null!;
        private System.Windows.Forms.TreeView tvOutline = null!;
        private System.Windows.Forms.Panel pnlButtons = null!;
        private System.Windows.Forms.Button btnCreate = null!;
        private System.Windows.Forms.Button btnNameTemplate = null!;
        private System.Windows.Forms.Button btnAddNumberedFile = null!;
        private System.Windows.Forms.Button btnSave = null!;
        private System.Windows.Forms.Button btnClose = null!;
        private System.Windows.Forms.StatusStrip statusStrip = null!;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fileSelector = new CEMExplorer.Controls.ucFileSelector();
            this.lblRootFolder = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnSetup = new System.Windows.Forms.Button();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lblStructure = new System.Windows.Forms.Label();
            this.tvProject = new System.Windows.Forms.TreeView();
            this.lblDetail = new System.Windows.Forms.Label();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lvFolder = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colModified = new System.Windows.Forms.ColumnHeader();
            this.txtFileContents = new System.Windows.Forms.TextBox();
            this.pnlOutline = new System.Windows.Forms.Panel();
            this.tvOutline = new System.Windows.Forms.TreeView();
            this.pnlOutlineCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOutlineAddChild = new System.Windows.Forms.Button();
            this.btnOutlineAddSibling = new System.Windows.Forms.Button();
            this.btnOutlineRename = new System.Windows.Forms.Button();
            this.btnOutlineRemove = new System.Windows.Forms.Button();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnNameTemplate = new System.Windows.Forms.Button();
            this.btnAddNumberedFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.pnlOutline.SuspendLayout();
            this.pnlOutlineCommands.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // lblRootFolder
            this.lblRootFolder.AutoSize = true;
            this.lblRootFolder.Location = new System.Drawing.Point(14, 17);
            this.lblRootFolder.Name = "lblRootFolder";
            this.lblRootFolder.Size = new System.Drawing.Size(87, 20);
            this.lblRootFolder.Text = "Root folder:";
            // fileSelector
            this.fileSelector.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.fileSelector.FileType = "";
            this.fileSelector.Location = new System.Drawing.Point(107, 12);
            this.fileSelector.Name = "fileSelector";
            this.fileSelector.SelectFolder = true;
            this.fileSelector.Size = new System.Drawing.Size(859, 30);
            this.fileSelector.TabIndex = 0;
            this.fileSelector.FileNameChanged += new System.EventHandler(this.fileSelector_FileNameChanged);
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(14, 58);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(41, 20);
            this.lblTitle.Text = "Title:";
            // txtTitle
            this.txtTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtTitle.Location = new System.Drawing.Point(107, 54);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(764, 27);
            this.txtTitle.TabIndex = 1;
            // btnSetup
            this.btnSetup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnSetup.Location = new System.Drawing.Point(880, 52);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(86, 31);
            this.btnSetup.TabIndex = 2;
            this.btnSetup.Text = "Setup";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // splitMain
            this.splitMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.splitMain.Location = new System.Drawing.Point(14, 94);
            this.splitMain.Name = "splitMain";
            // splitMain.Panel1
            this.splitMain.Panel1.Controls.Add(this.tvProject);
            this.splitMain.Panel1.Controls.Add(this.lblStructure);
            // splitMain.Panel2
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Panel2.Controls.Add(this.lblDetail);
            this.splitMain.Size = new System.Drawing.Size(952, 516);
            this.splitMain.SplitterDistance = 370;
            this.splitMain.TabIndex = 3;
            // lblStructure
            this.lblStructure.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStructure.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStructure.Location = new System.Drawing.Point(0, 0);
            this.lblStructure.Name = "lblStructure";
            this.lblStructure.Padding = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this.lblStructure.Size = new System.Drawing.Size(370, 29);
            this.lblStructure.Text = "Project Structure";
            // tvProject
            this.tvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProject.HideSelection = false;
            this.tvProject.Location = new System.Drawing.Point(0, 29);
            this.tvProject.Name = "tvProject";
            this.tvProject.Size = new System.Drawing.Size(370, 487);
            this.tvProject.TabIndex = 0;
            this.tvProject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterSelect);
            // lblDetail
            this.lblDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetail.Location = new System.Drawing.Point(0, 0);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Padding = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this.lblDetail.Size = new System.Drawing.Size(578, 29);
            this.lblDetail.Text = "Folder Contents";
            // pnlDetail
            this.pnlDetail.Controls.Add(this.lvFolder);
            this.pnlDetail.Controls.Add(this.txtFileContents);
            this.pnlDetail.Controls.Add(this.pnlOutline);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 29);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(578, 487);
            // lvFolder
            this.lvFolder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colName, this.colType, this.colSize, this.colModified });
            this.lvFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFolder.FullRowSelect = true;
            this.lvFolder.HideSelection = false;
            this.lvFolder.Location = new System.Drawing.Point(0, 0);
            this.lvFolder.MultiSelect = false;
            this.lvFolder.Name = "lvFolder";
            this.lvFolder.Size = new System.Drawing.Size(578, 487);
            this.lvFolder.TabIndex = 0;
            this.lvFolder.UseCompatibleStateImageBehavior = false;
            this.lvFolder.View = System.Windows.Forms.View.Details;
            this.lvFolder.ItemActivate += new System.EventHandler(this.lvFolder_ItemActivate);
            // columns
            this.colName.Text = "Name";
            this.colName.Width = 240;
            this.colType.Text = "Type";
            this.colType.Width = 90;
            this.colSize.Text = "Size";
            this.colSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSize.Width = 80;
            this.colModified.Text = "Modified";
            this.colModified.Width = 145;
            // txtFileContents
            this.txtFileContents.AcceptsTab = true;
            this.txtFileContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileContents.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtFileContents.Location = new System.Drawing.Point(0, 0);
            this.txtFileContents.Multiline = true;
            this.txtFileContents.Name = "txtFileContents";
            this.txtFileContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFileContents.Size = new System.Drawing.Size(578, 487);
            this.txtFileContents.TabIndex = 1;
            this.txtFileContents.Visible = false;
            this.txtFileContents.WordWrap = false;
            this.txtFileContents.TextChanged += new System.EventHandler(this.txtFileContents_TextChanged);
            // pnlOutline
            this.pnlOutline.Controls.Add(this.tvOutline);
            this.pnlOutline.Controls.Add(this.pnlOutlineCommands);
            this.pnlOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutline.Location = new System.Drawing.Point(0, 0);
            this.pnlOutline.Name = "pnlOutline";
            this.pnlOutline.Size = new System.Drawing.Size(578, 487);
            this.pnlOutline.TabIndex = 2;
            this.pnlOutline.Visible = false;
            // tvOutline
            this.tvOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOutline.HideSelection = false;
            this.tvOutline.LabelEdit = true;
            this.tvOutline.Location = new System.Drawing.Point(0, 43);
            this.tvOutline.Name = "tvOutline";
            this.tvOutline.Size = new System.Drawing.Size(578, 444);
            this.tvOutline.TabIndex = 1;
            this.tvOutline.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvOutline_AfterLabelEdit);
            this.tvOutline.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvOutline_AfterSelect);
            this.tvOutline.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvOutline_KeyDown);
            // pnlOutlineCommands
            this.pnlOutlineCommands.Controls.Add(this.btnOutlineAddChild);
            this.pnlOutlineCommands.Controls.Add(this.btnOutlineAddSibling);
            this.pnlOutlineCommands.Controls.Add(this.btnOutlineRename);
            this.pnlOutlineCommands.Controls.Add(this.btnOutlineRemove);
            this.pnlOutlineCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOutlineCommands.Location = new System.Drawing.Point(0, 0);
            this.pnlOutlineCommands.Name = "pnlOutlineCommands";
            this.pnlOutlineCommands.Padding = new System.Windows.Forms.Padding(3, 4, 0, 3);
            this.pnlOutlineCommands.Size = new System.Drawing.Size(578, 43);
            this.pnlOutlineCommands.TabIndex = 0;
            // outline buttons
            this.btnOutlineAddChild.Size = new System.Drawing.Size(96, 31);
            this.btnOutlineAddChild.Text = "Add Child";
            this.btnOutlineAddChild.UseVisualStyleBackColor = true;
            this.btnOutlineAddChild.Click += new System.EventHandler(this.btnOutlineAddChild_Click);
            this.btnOutlineAddSibling.Size = new System.Drawing.Size(108, 31);
            this.btnOutlineAddSibling.Text = "Add Sibling";
            this.btnOutlineAddSibling.UseVisualStyleBackColor = true;
            this.btnOutlineAddSibling.Click += new System.EventHandler(this.btnOutlineAddSibling_Click);
            this.btnOutlineRename.Size = new System.Drawing.Size(86, 31);
            this.btnOutlineRename.Text = "Rename";
            this.btnOutlineRename.UseVisualStyleBackColor = true;
            this.btnOutlineRename.Click += new System.EventHandler(this.btnOutlineRename_Click);
            this.btnOutlineRemove.Size = new System.Drawing.Size(86, 31);
            this.btnOutlineRemove.Text = "Remove";
            this.btnOutlineRemove.UseVisualStyleBackColor = true;
            this.btnOutlineRemove.Click += new System.EventHandler(this.btnOutlineRemove_Click);
            // pnlButtons
            this.pnlButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlButtons.Controls.Add(this.btnCreate);
            this.pnlButtons.Controls.Add(this.btnNameTemplate);
            this.pnlButtons.Controls.Add(this.btnAddNumberedFile);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Location = new System.Drawing.Point(14, 619);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(952, 43);
            // btnCreate
            this.btnCreate.Location = new System.Drawing.Point(0, 5);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(90, 33);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // btnNameTemplate
            this.btnNameTemplate.Location = new System.Drawing.Point(97, 5);
            this.btnNameTemplate.Name = "btnNameTemplate";
            this.btnNameTemplate.Size = new System.Drawing.Size(128, 33);
            this.btnNameTemplate.TabIndex = 1;
            this.btnNameTemplate.Text = "Name X File";
            this.btnNameTemplate.UseVisualStyleBackColor = true;
            this.btnNameTemplate.Click += new System.EventHandler(this.btnNameTemplate_Click);
            // btnAddNumberedFile
            this.btnAddNumberedFile.Location = new System.Drawing.Point(232, 5);
            this.btnAddNumberedFile.Name = "btnAddNumberedFile";
            this.btnAddNumberedFile.Size = new System.Drawing.Size(155, 33);
            this.btnAddNumberedFile.TabIndex = 2;
            this.btnAddNumberedFile.Text = "Add Numbered File";
            this.btnAddNumberedFile.UseVisualStyleBackColor = true;
            this.btnAddNumberedFile.Click += new System.EventHandler(this.btnAddNumberedFile_Click);
            // btnSave
            this.btnSave.Location = new System.Drawing.Point(394, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 33);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // btnClose
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(862, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 33);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
            this.statusStrip.Location = new System.Drawing.Point(0, 670);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(980, 22);
            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // frmMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 692);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.fileSelector);
            this.Controls.Add(this.lblRootFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(760, 520);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CEM Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.pnlOutline.ResumeLayout(false);
            this.pnlOutlineCommands.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
