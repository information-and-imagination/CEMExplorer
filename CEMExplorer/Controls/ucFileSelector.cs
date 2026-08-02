using System;
using System.IO;
using System.Windows.Forms;

namespace CEMExplorer.Controls
{
    public partial class ucFileSelector : UserControl
    {
        public event EventHandler? FileNameChanged;

        private string fileType = string.Empty;
        private string lastCommittedFileName = string.Empty;

        public ucFileSelector()
        {
            InitializeComponent();
            txtFullFileName.Leave += txtFullFileName_Leave;
        }

        public string FileType
        {
            get { return fileType; }
            set { fileType = value ?? string.Empty; }
        }

        public bool SelectFolder { get; set; }

        public override string Text
        {
            get { return txtFullFileName.Text; }
            set
            {
                txtFullFileName.Text = value ?? string.Empty;
                CommitFileNameChange();
            }
        }

        public string FileName
        {
            get { return txtFullFileName.Text; }
            set
            {
                txtFullFileName.Text = value ?? string.Empty;
                CommitFileNameChange();
            }
        }

        private void txtFullFileName_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            CommitFileNameChange();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void btnOpenFileExplorer_Click(object? sender, EventArgs e)
        {
            if (SelectFolder)
            {
                using FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Select the CEM root folder";
                dialog.UseDescriptionForTitle = true;

                if (Directory.Exists(txtFullFileName.Text))
                    dialog.SelectedPath = txtFullFileName.Text;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFullFileName.Text = dialog.SelectedPath;
                    CommitFileNameChange();
                }

                return;
            }

            using OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = string.IsNullOrWhiteSpace(fileType)
                ? "All Files (*.*)|*.*"
                : fileType;

            if (File.Exists(txtFullFileName.Text))
                fileDialog.FileName = txtFullFileName.Text;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFullFileName.Text = fileDialog.FileName;
                CommitFileNameChange();
            }
        }

        private void txtFullFileName_Leave(object? sender, EventArgs e)
        {
            CommitFileNameChange();
        }

        private void CommitFileNameChange()
        {
            string currentValue = txtFullFileName.Text ?? string.Empty;
            if (string.Equals(currentValue, lastCommittedFileName, StringComparison.Ordinal))
                return;

            lastCommittedFileName = currentValue;
            FileNameChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ucFileSelector_Resize(object? sender, EventArgs e)
        {
            btnOpenFileExplorer.Left = Width - btnOpenFileExplorer.Width;
            txtFullFileName.Width = Math.Max(20, btnOpenFileExplorer.Left - 8);
        }
    }
}
