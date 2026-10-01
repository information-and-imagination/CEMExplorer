using System.Drawing;
using System.Windows.Forms;

namespace CEMExplorer
{
    internal sealed class SystemArchitectureDetailsPrompt : Form
    {
        private readonly TextBox title = new TextBox();
        private readonly TextBox modelVersion = new TextBox();

        private SystemArchitectureDetailsPrompt()
        {
            Text = "Create New System Architecture";
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(480, 215);
            Controls.Add(new Label { Text = "Title:", AutoSize = true, Location = new Point(18, 18) });
            title.SetBounds(20, 45, 438, 28);
            Controls.Add(title);
            Controls.Add(new Label { Text = "Model and/or version number (optional):", AutoSize = true, Location = new Point(18, 85) });
            modelVersion.SetBounds(20, 112, 438, 28);
            Controls.Add(modelVersion);
            Button create = new Button { Text = "Create", Bounds = new Rectangle(280, 163, 85, 32) };
            Button cancel = new Button { Text = "Cancel", Bounds = new Rectangle(373, 163, 85, 32), DialogResult = DialogResult.Cancel };
            create.Click += (_, _) =>
            {
                if (title.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "Enter a title for the new System Architecture.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    title.Focus();
                }
                else
                    DialogResult = DialogResult.OK;
            };
            Controls.Add(create);
            Controls.Add(cancel);
            AcceptButton = create;
            CancelButton = cancel;
        }

        public static bool TryGet(IWin32Window owner, out string architectureTitle, out string version)
        {
            using SystemArchitectureDetailsPrompt dialog = new SystemArchitectureDetailsPrompt();
            bool accepted = dialog.ShowDialog(owner) == DialogResult.OK;
            architectureTitle = accepted ? dialog.title.Text.Trim() : string.Empty;
            version = accepted ? dialog.modelVersion.Text.Trim() : string.Empty;
            return accepted;
        }
    }
}
