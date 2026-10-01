using System;
using System.Drawing;
using System.Windows.Forms;
using CEMExplorer.Services;

namespace CEMExplorer
{
    internal sealed class NewSystemArchitectureDialog : Form
    {
        private readonly string projectRoot;
        private readonly TreeView tree = new TreeView();
        private bool changingChecks;

        public NewSystemArchitectureDialog(string projectRoot, string conceptFile)
        {
            this.projectRoot = projectRoot;
            Text = "New System Architecture — Concept";
            Font = new Font("Segoe UI", 10F);
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(620, 430);
            ClientSize = new Size(760, 570);

            Label heading = new Label
            {
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(10, 8, 0, 0),
                Text = "Select the Concept items for this System Architecture:"
            };
            tree.Dock = DockStyle.Fill;
            tree.CheckBoxes = true;
            tree.HideSelection = false;
            tree.AfterCheck += Tree_AfterCheck;

            FlowLayoutPanel commands = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 53,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(8),
                WrapContents = false
            };
            commands.Controls.Add(Command("Select All", (_, _) => SetAll(true), 105));
            commands.Controls.Add(Command("Unselect All", (_, _) => SetAll(false), 120));
            commands.Controls.Add(Command("Create New SA", Create_Click, 145));
            Button close = Command("Close", (_, _) => Close(), 90);
            commands.Controls.Add(close);
            CancelButton = close;

            Controls.Add(tree);
            Controls.Add(commands);
            Controls.Add(heading);
            new CeoOutlineService().Load(conceptFile, tree);
        }

        public string CreatedPath { get; private set; } = string.Empty;

        private static Button Command(string caption, EventHandler handler, int width)
        {
            Button button = new Button { Text = caption, Width = width, Height = 32, AutoSize = false };
            button.Click += handler;
            return button;
        }

        private void SetAll(bool value)
        {
            changingChecks = true;
            try
            {
                foreach (TreeNode node in tree.Nodes)
                    SetBranch(node, value);
            }
            finally { changingChecks = false; }
        }

        private static void SetBranch(TreeNode node, bool value)
        {
            node.Checked = value;
            foreach (TreeNode child in node.Nodes)
                SetBranch(child, value);
        }

        private void Tree_AfterCheck(object? sender, TreeViewEventArgs e)
        {
            if (changingChecks || e.Action == TreeViewAction.Unknown)
                return;
            changingChecks = true;
            try
            {
                foreach (TreeNode child in e.Node.Nodes)
                    SetBranch(child, e.Node.Checked);
                if (e.Node.Checked)
                {
                    for (TreeNode? parent = e.Node.Parent; parent != null; parent = parent.Parent)
                        parent.Checked = true;
                }
            }
            finally { changingChecks = false; }
        }

        private void Create_Click(object? sender, EventArgs e)
        {
            if (!SystemArchitectureDetailsPrompt.TryGet(this, out string title, out string modelVersion))
                return;
            try
            {
                CreatedPath = new SystemArchitectureService().Create(projectRoot, title, modelVersion, tree);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Create New SA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
