using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CEMExplorer.Services;

namespace CEMExplorer
{
    internal partial class frmProjectWizard : Form
    {
        private readonly SkeletonService skeletonService = new SkeletonService();

        public frmProjectWizard()
        {
            InitializeComponent();
            UpdateCommandState();
        }

        public string ProjectRoot { get; private set; } = string.Empty;

        private void selectionChanged(object? sender, EventArgs e)
        {
            UpdateCommandState();
        }

        private void btnOpen_Click(object? sender, EventArgs e)
        {
            string folder = openFolderSelector.FileName.Trim();
            if (!Directory.Exists(folder))
            {
                ShowValidation("Select an existing CEM project folder.", openFolderSelector);
                return;
            }

            ProjectRoot = Path.GetFullPath(folder);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCreate_Click(object? sender, EventArgs e)
        {
            string baseFolder = createFolderSelector.FileName.Trim();
            string title = txtProjectTitle.Text.Trim();
            string abbreviation = txtAbbreviation.Text.Trim().ToUpperInvariant();
            //string baseFolderToCheck = Path.Combine(baseFolder, abbreviation);
            if (!Directory.Exists(baseFolder))
            {
                ShowValidation("Select an existing parent folder for the new project.", createFolderSelector);
                return;
            }
            if (title.Length == 0)
            {
                ShowValidation("Enter the project title.", txtProjectTitle);
                return;
            }
            if (!Regex.IsMatch(abbreviation, "^[A-Z][A-Z0-9_-]{1,15}$"))
            {
                ShowValidation("Use a 2–16 character abbreviation made from letters, numbers, underscores, or hyphens, beginning with a letter.", txtAbbreviation);
                return;
            }

            string skeletonFile = Path.Combine(AppContext.BaseDirectory, "CEMEXPLORERSKELETON.txt");
            try
            {
                ProjectRoot = skeletonService.Create(baseFolder, skeletonFile, abbreviation, title);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Create CEM Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowValidation(string message, Control control)
        {
            lblMessage.Text = message;
            tabProjectActions.SelectedTab = control == openFolderSelector ? tabOpen : tabCreate;
            control.Focus();
        }

        private void UpdateCommandState()
        {
            btnOpen.Enabled = Directory.Exists(openFolderSelector.FileName.Trim());
            btnCreate.Enabled = Directory.Exists(createFolderSelector.FileName.Trim()) &&
                                txtProjectTitle.Text.Trim().Length > 0 &&
                                txtAbbreviation.Text.Trim().Length > 0;
            lblMessage.Text = string.Empty;
        }
    }
}
