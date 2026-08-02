using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CEMExplorer.Services;

namespace CEMExplorer
{
    public partial class frmMain : Form
    {
        private readonly SkeletonService skeletonService = new SkeletonService();
        private readonly CeoOutlineService ceoOutlineService = new CeoOutlineService();
        private static readonly Regex TemplatePlaceholderPattern = new Regex("X{3,}", RegexOptions.IgnoreCase);
        private string? selectedFilePath;
        private bool fileContentsDirty;
        private bool loadingFile;
        private bool selectedFileIsCeo;

        public frmMain()
        {
            InitializeComponent();
            UpdateCommandState();
        }

        private void fileSelector_FileNameChanged(object? sender, EventArgs e)
        {
            if (!ConfirmDiscardChanges())
                return;

            LoadRootFolder(fileSelector.FileName.Trim());
        }

        private void LoadRootFolder(string rootFolder)
        {
            tvProject.BeginUpdate();
            try
            {
                tvProject.Nodes.Clear();
                selectedFilePath = null;
                ShowEmptyDetail();

                if (!Directory.Exists(rootFolder))
                {
                    txtTitle.Text = string.Empty;
                    SetStatus(string.IsNullOrWhiteSpace(rootFolder) ? "Select a root folder." : "The selected root folder does not exist.");
                    return;
                }

                DirectoryInfo root = new DirectoryInfo(rootFolder);
                TreeNode rootNode = CreateTreeNode(root);
                tvProject.Nodes.Add(rootNode);
                AddChildren(rootNode, root);
                rootNode.Expand();
                tvProject.SelectedNode = rootNode;

                string title = ReadProjectTitle(rootFolder);
                txtTitle.Text = title;
                SetStatus("Loaded " + root.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("The root folder could not be loaded.");
            }
            finally
            {
                tvProject.EndUpdate();
                UpdateCommandState();
            }
        }

        private void AddChildren(TreeNode parentNode, DirectoryInfo directory)
        {
            try
            {
                foreach (DirectoryInfo childDirectory in directory.EnumerateDirectories().OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase))
                {
                    TreeNode childNode = CreateTreeNode(childDirectory);
                    parentNode.Nodes.Add(childNode);
                    AddChildren(childNode, childDirectory);
                }

                foreach (FileInfo file in directory.EnumerateFiles().OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase))
                    parentNode.Nodes.Add(CreateTreeNode(file));
            }
            catch (UnauthorizedAccessException)
            {
                parentNode.Nodes.Add(new TreeNode("[Access denied]") { ForeColor = System.Drawing.Color.Firebrick });
            }
            catch (IOException ex)
            {
                parentNode.Nodes.Add(new TreeNode("[" + ex.Message + "]") { ForeColor = System.Drawing.Color.Firebrick });
            }
        }

        private static TreeNode CreateTreeNode(FileSystemInfo item)
        {
            return new TreeNode(item.Name)
            {
                Tag = new NodeInfo(item.FullName, item is DirectoryInfo)
            };
        }

        private void tvProject_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is not NodeInfo info)
                return;

            if (!ConfirmDiscardChanges())
                return;

            if (info.IsDirectory)
                ShowFolder(info.FullPath);
            else
                ShowFile(info.FullPath);
        }

        private void ShowFolder(string folderPath)
        {
            selectedFilePath = null;
            selectedFileIsCeo = false;
            fileContentsDirty = false;
            lblDetail.Text = "Folder Contents — " + Path.GetFileName(folderPath);
            txtFileContents.Visible = false;
            pnlOutline.Visible = false;
            lvFolder.Visible = true;
            lvFolder.BringToFront();
            lvFolder.BeginUpdate();
            lvFolder.Items.Clear();

            try
            {
                DirectoryInfo directory = new DirectoryInfo(folderPath);
                foreach (DirectoryInfo childDirectory in directory.EnumerateDirectories().OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase))
                    lvFolder.Items.Add(CreateListItem(childDirectory));
                foreach (FileInfo file in directory.EnumerateFiles().OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase))
                    lvFolder.Items.Add(CreateListItem(file));
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                SetStatus(ex.Message);
            }
            finally
            {
                lvFolder.EndUpdate();
                UpdateCommandState();
            }
        }

        private static ListViewItem CreateListItem(FileSystemInfo item)
        {
            bool isDirectory = item is DirectoryInfo;
            string size = isDirectory ? string.Empty : FormatFileSize(((FileInfo)item).Length);
            ListViewItem row = new ListViewItem(item.Name);
            row.SubItems.Add(isDirectory ? "Folder" : item.Extension.TrimStart('.').ToUpperInvariant() + " File");
            row.SubItems.Add(size);
            row.SubItems.Add(item.LastWriteTime.ToString("g"));
            row.Tag = new NodeInfo(item.FullName, isDirectory);
            return row;
        }

        private void lvFolder_ItemActivate(object? sender, EventArgs e)
        {
            if (lvFolder.SelectedItems.Count == 0 || lvFolder.SelectedItems[0].Tag is not NodeInfo info)
                return;

            TreeNode? node = FindNode(tvProject.Nodes, info.FullPath);
            if (node != null)
                tvProject.SelectedNode = node;
        }

        private static TreeNode? FindNode(TreeNodeCollection nodes, string fullPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is NodeInfo info && string.Equals(info.FullPath, fullPath, StringComparison.OrdinalIgnoreCase))
                    return node;

                TreeNode? found = FindNode(node.Nodes, fullPath);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void ShowFile(string filePath)
        {
            selectedFilePath = filePath;
            selectedFileIsCeo = string.Equals(Path.GetExtension(filePath), ".ceo", StringComparison.OrdinalIgnoreCase);
            lblDetail.Text = "Selected Document — " + Path.GetFileName(filePath);
            lvFolder.Visible = false;
            txtFileContents.Visible = !selectedFileIsCeo;
            pnlOutline.Visible = selectedFileIsCeo;
            if (selectedFileIsCeo)
                pnlOutline.BringToFront();
            else
                txtFileContents.BringToFront();

            loadingFile = true;
            try
            {
                if (selectedFileIsCeo)
                {
                    txtFileContents.ReadOnly = false;
                    ceoOutlineService.Load(filePath, tvOutline);
                }
                else if (IsProbablyBinary(filePath))
                {
                    txtFileContents.ReadOnly = true;
                    txtFileContents.Text = "This document appears to be binary and cannot be previewed as text.";
                }
                else
                {
                    txtFileContents.ReadOnly = false;
                    txtFileContents.Text = File.ReadAllText(filePath);
                    txtFileContents.SelectionStart = 0;
                    txtFileContents.SelectionLength = 0;
                }

                fileContentsDirty = false;
                SetStatus(filePath);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                txtFileContents.ReadOnly = true;
                txtFileContents.Text = ex.Message;
                SetStatus("The selected file could not be opened.");
            }
            finally
            {
                loadingFile = false;
                UpdateCommandState();
            }
        }

        private void txtFileContents_TextChanged(object? sender, EventArgs e)
        {
            if (loadingFile || txtFileContents.ReadOnly || selectedFilePath == null)
                return;

            fileContentsDirty = true;
            UpdateCommandState();
        }

        private void btnCreate_Click(object? sender, EventArgs e)
        {
            string baseFolder = fileSelector.FileName.Trim();
            if (!Directory.Exists(baseFolder))
            {
                MessageBox.Show(this, "Select an existing root folder first.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!AbbreviationPrompt.TryGet(this, out string abbreviation))
                return;

            string skeletonFile = Path.Combine(AppContext.BaseDirectory, "CEMEXPLORERSKELETON.txt");
            try
            {
                string projectRoot = skeletonService.Create(baseFolder, skeletonFile, abbreviation, txtTitle.Text.Trim());
                fileSelector.FileName = projectRoot;
                SetStatus("Created CEM project structure in " + projectRoot);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetup_Click(object? sender, EventArgs e)
        {
            string rootFolder = fileSelector.FileName.Trim();
            string title = txtTitle.Text.Trim();
            if (!Directory.Exists(rootFolder))
            {
                MessageBox.Show(this, "Select an existing root folder first.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (title.Length == 0)
            {
                MessageBox.Show(this, "Enter a project title first.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTitle.Focus();
                return;
            }

            bool isEmpty = !Directory.EnumerateFileSystemEntries(rootFolder).Any();
            if (isEmpty)
            {
                SetStatus("Title is ready. Click Create to generate the project structure.");
                return;
            }

            try
            {
                WriteProjectTitle(rootFolder, title);
                RefreshTreePreservingSelection();
                SetStatus("Project title saved to README.md.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            SaveSelectedFile();
        }

        private bool SaveSelectedFile()
        {
            if (selectedFilePath == null || (!selectedFileIsCeo && txtFileContents.ReadOnly))
                return true;

            try
            {
                if (selectedFileIsCeo)
                    ceoOutlineService.Save(selectedFilePath, tvOutline);
                else
                    File.WriteAllText(selectedFilePath, txtFileContents.Text, new UTF8Encoding(false));
                fileContentsDirty = false;
                SetStatus("Saved " + selectedFilePath);
                UpdateCommandState();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!ConfirmDiscardChanges())
                e.Cancel = true;
        }

        private bool ConfirmDiscardChanges()
        {
            if (!fileContentsDirty)
                return true;

            DialogResult result = MessageBox.Show(this,
                "Save changes to the selected file?",
                "CEM Explorer",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
                return false;
            if (result == DialogResult.Yes)
                return SaveSelectedFile();

            fileContentsDirty = false;
            return true;
        }

        private void RefreshTreePreservingSelection()
        {
            string root = fileSelector.FileName.Trim();
            LoadRootFolder(root);
        }

        private static string ReadProjectTitle(string rootFolder)
        {
            string readme = Path.Combine(rootFolder, "README.md");
            if (!File.Exists(readme))
                return Directory.EnumerateFileSystemEntries(rootFolder).Any()
                    ? new DirectoryInfo(rootFolder).Name
                    : string.Empty;

            foreach (string line in File.ReadLines(readme))
            {
                if (line.StartsWith("# ", StringComparison.Ordinal))
                    return line.Substring(2).Trim();
            }

            return new DirectoryInfo(rootFolder).Name;
        }

        private static void WriteProjectTitle(string rootFolder, string title)
        {
            string readme = Path.Combine(rootFolder, "README.md");
            List<string> lines = File.Exists(readme) ? File.ReadAllLines(readme).ToList() : new List<string>();
            int headingIndex = lines.FindIndex(line => line.StartsWith("# ", StringComparison.Ordinal));
            if (headingIndex >= 0)
                lines[headingIndex] = "# " + title;
            else
                lines.Insert(0, "# " + title);

            File.WriteAllLines(readme, lines, new UTF8Encoding(false));
        }

        private static bool IsProbablyBinary(string filePath)
        {
            byte[] buffer = new byte[4096];
            using FileStream stream = File.OpenRead(filePath);
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            for (int index = 0; index < bytesRead; index++)
            {
                if (buffer[index] == 0)
                    return true;
            }
            return false;
        }

        private static string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return bytes + " B";
            if (bytes < 1024 * 1024)
                return (bytes / 1024d).ToString("0.0") + " KB";
            if (bytes < 1024L * 1024L * 1024L)
                return (bytes / 1024d / 1024d).ToString("0.0") + " MB";
            return (bytes / 1024d / 1024d / 1024d).ToString("0.0") + " GB";
        }

        private void ShowEmptyDetail()
        {
            lblDetail.Text = "Folder Contents";
            lvFolder.Items.Clear();
            lvFolder.Visible = true;
            txtFileContents.Visible = false;
            pnlOutline.Visible = false;
            selectedFileIsCeo = false;
            fileContentsDirty = false;
        }

        private void UpdateCommandState()
        {
            btnCreate.Enabled = Directory.Exists(fileSelector.FileName.Trim());
            btnSetup.Enabled = Directory.Exists(fileSelector.FileName.Trim());
            bool templateSelected = selectedFilePath != null && TemplatePlaceholderPattern.IsMatch(Path.GetFileName(selectedFilePath));
            btnNameTemplate.Enabled = templateSelected;
            btnAddNumberedFile.Enabled = templateSelected;
            btnSave.Enabled = selectedFilePath != null && (selectedFileIsCeo || !txtFileContents.ReadOnly) && fileContentsDirty;

            bool outlineNodeSelected = selectedFileIsCeo && tvOutline.SelectedNode != null;
            btnOutlineAddChild.Enabled = selectedFileIsCeo && (outlineNodeSelected || tvOutline.Nodes.Count == 0);
            btnOutlineAddChild.Text = tvOutline.Nodes.Count == 0 ? "Add Root" : "Add Child";
            btnOutlineAddSibling.Enabled = outlineNodeSelected && tvOutline.SelectedNode!.Parent != null;
            btnOutlineRename.Enabled = outlineNodeSelected;
            btnOutlineRemove.Enabled = outlineNodeSelected && tvOutline.SelectedNode!.Parent != null;
        }

        private void btnOutlineAddChild_Click(object? sender, EventArgs e)
        {
            TreeNode? parent = tvOutline.SelectedNode;
            bool addingRoot = parent == null && tvOutline.Nodes.Count == 0;
            if (!addingRoot && parent == null)
                return;

            string title = addingRoot ? "Add Outline Root" : "Add Outline Child";
            string prompt = addingRoot ? "Enter the root name:" : "Enter the child branch name:";
            if (!TryGetOutlineName(title, prompt, string.Empty, out string name))
                return;

            TreeNode child = addingRoot ? tvOutline.Nodes.Add(name) : parent!.Nodes.Add(name);
            parent?.Expand();
            tvOutline.SelectedNode = child;
            MarkOutlineDirty();
        }

        private void btnOutlineAddSibling_Click(object? sender, EventArgs e)
        {
            TreeNode? selected = tvOutline.SelectedNode;
            if (selected?.Parent == null || !TryGetOutlineName("Add Outline Sibling", "Enter the sibling branch name:", string.Empty, out string name))
                return;

            TreeNode sibling = new TreeNode(name);
            selected.Parent.Nodes.Insert(selected.Index + 1, sibling);
            tvOutline.SelectedNode = sibling;
            MarkOutlineDirty();
        }

        private void btnOutlineRename_Click(object? sender, EventArgs e)
        {
            tvOutline.SelectedNode?.BeginEdit();
        }

        private void btnOutlineRemove_Click(object? sender, EventArgs e)
        {
            TreeNode? selected = tvOutline.SelectedNode;
            if (selected?.Parent == null)
                return;

            string message = selected.Nodes.Count == 0
                ? "Remove the selected branch '" + selected.Text + "'?"
                : "Remove the selected branch '" + selected.Text + "' and all of its children?";
            if (MessageBox.Show(this, message, "CEM Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            TreeNode parent = selected.Parent;
            selected.Remove();
            tvOutline.SelectedNode = parent;
            MarkOutlineDirty();
        }

        private void tvOutline_AfterLabelEdit(object? sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                return;

            string value = e.Label.Trim();
            if (!IsValidOutlineName(value))
            {
                e.CancelEdit = true;
                MessageBox.Show(this, "Enter a non-empty branch name that does not begin with a hyphen or contain a line break.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            e.Node.Text = value;
            MarkOutlineDirty();
        }

        private void tvOutline_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            UpdateCommandState();
        }

        private void tvOutline_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnOutlineRename_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnOutlineRemove_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Insert)
            {
                btnOutlineAddChild_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private bool TryGetOutlineName(string title, string prompt, string initialValue, out string name)
        {
            while (OutlineItemPrompt.TryGet(this, title, prompt, initialValue, out name))
            {
                if (IsValidOutlineName(name))
                    return true;

                MessageBox.Show(this, "Enter a non-empty branch name that does not begin with a hyphen or contain a line break.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                initialValue = name;
            }
            return false;
        }

        private static bool IsValidOutlineName(string value)
        {
            return value.Trim().Length > 0 && !value.TrimStart().StartsWith("-", StringComparison.Ordinal) &&
                   value.IndexOf('\r') < 0 && value.IndexOf('\n') < 0;
        }

        private void MarkOutlineDirty()
        {
            if (loadingFile)
                return;
            fileContentsDirty = true;
            UpdateCommandState();
        }

        private void btnNameTemplate_Click(object? sender, EventArgs e)
        {
            if (selectedFilePath == null || !TemplatePlaceholderPattern.IsMatch(Path.GetFileName(selectedFilePath)))
                return;
            if (!ConfirmDiscardChanges())
                return;

            string originalName = Path.GetFileName(selectedFilePath);
            if (!TemplateFilePrompt.TryGet(this, "Name Template File", originalName, originalName, "Rename", out string newName))
                return;

            string? folder = Path.GetDirectoryName(selectedFilePath);
            if (folder == null)
                return;
            string newPath = Path.Combine(folder, newName);
            if (File.Exists(newPath))
            {
                MessageBox.Show(this, "A file with that name already exists.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                File.Move(selectedFilePath, newPath);
                ReloadAndSelect(newPath);
                SetStatus("Renamed " + originalName + " to " + newName);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show(this, ex.Message, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNumberedFile_Click(object? sender, EventArgs e)
        {
            if (selectedFilePath == null)
                return;
            if (!ConfirmDiscardChanges())
                return;

            string originalName = Path.GetFileName(selectedFilePath);
            if (!TryBuildNextTemplateName(selectedFilePath, out string nextTemplateName, out string error))
            {
                MessageBox.Show(this, error, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!TemplateFilePrompt.TryGet(this, "Add Numbered File", originalName, nextTemplateName, "Create", out string newName))
                return;

            string folder = Path.GetDirectoryName(selectedFilePath)!;
            string newPath = Path.Combine(folder, newName);
            if (File.Exists(newPath))
            {
                MessageBox.Show(this, "A file with that name already exists.", "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                File.WriteAllText(newPath, string.Empty, new UTF8Encoding(false));
                ReloadAndSelect(newPath);
                SetStatus("Created " + newName);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show(this, ex.Message, "CEM Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool TryBuildNextTemplateName(string selectedPath, out string nextName, out string error)
        {
            string selectedName = Path.GetFileName(selectedPath);
            Match placeholder = TemplatePlaceholderPattern.Match(selectedName);
            if (!placeholder.Success)
            {
                nextName = string.Empty;
                error = "The selected filename does not contain an XXXXXX placeholder.";
                return false;
            }

            MatchCollection numbers = Regex.Matches(selectedName.Substring(0, placeholder.Index), @"\d+");
            if (numbers.Count == 0)
            {
                nextName = string.Empty;
                error = "The selected template filename needs a number before its XXXXXX placeholder.";
                return false;
            }

            Match sequence = numbers[numbers.Count - 1];
            int width = sequence.Length;
            string prefix = selectedName.Substring(0, sequence.Index);
            string between = selectedName.Substring(sequence.Index + sequence.Length, placeholder.Index - sequence.Index - sequence.Length);
            string suffix = selectedName.Substring(placeholder.Index + placeholder.Length);
            Regex familyPattern = new Regex("^" + Regex.Escape(prefix) + "(?<number>\\d{" + width + "})" + Regex.Escape(between) + ".+" + Regex.Escape(suffix) + "$", RegexOptions.IgnoreCase);

            int highest = 0;
            string folder = Path.GetDirectoryName(selectedPath)!;
            foreach (string path in Directory.EnumerateFiles(folder))
            {
                Match match = familyPattern.Match(Path.GetFileName(path));
                if (match.Success && int.TryParse(match.Groups["number"].Value, out int number))
                    highest = Math.Max(highest, number);
            }

            int next = Math.Max(highest, int.Parse(sequence.Value)) + 1;
            string nextSequence = next.ToString(new string('0', width));
            if (nextSequence.Length > width)
            {
                nextName = string.Empty;
                error = "The numbered filename sequence has exceeded its available digits.";
                return false;
            }

            nextName = prefix + nextSequence + between + placeholder.Value + suffix;
            error = string.Empty;
            return true;
        }

        private void ReloadAndSelect(string fullPath)
        {
            string root = fileSelector.FileName.Trim();
            LoadRootFolder(root);
            TreeNode? node = FindNode(tvProject.Nodes, fullPath);
            if (node != null)
                tvProject.SelectedNode = node;
        }

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        private sealed class NodeInfo
        {
            public NodeInfo(string fullPath, bool isDirectory)
            {
                FullPath = fullPath;
                IsDirectory = isDirectory;
            }

            public string FullPath { get; }
            public bool IsDirectory { get; }
        }
    }
}
