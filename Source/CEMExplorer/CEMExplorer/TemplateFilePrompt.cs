using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CEMExplorer
{
    internal sealed class TemplateFilePrompt : Form
    {
        private static readonly Regex PlaceholderPattern = new Regex("X{3,}", RegexOptions.IgnoreCase);
        private readonly string templateName;
        private readonly TextBox txtDescription;
        private readonly Label lblNewName;
        private readonly Label lblError;

        private TemplateFilePrompt(string title, string originalName, string templateName, string actionText)
        {
            this.templateName = templateName;
            Text = title;
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(580, 247);

            Label lblOriginalCaption = new Label { AutoSize = true, Location = new Point(18, 18), Text = "Original name:" };
            Label lblOriginal = new Label { AutoEllipsis = true, BorderStyle = BorderStyle.FixedSingle, Location = new Point(21, 44), Size = new Size(535, 29), Text = originalName, TextAlign = ContentAlignment.MiddleLeft };
            Label lblDescription = new Label { AutoSize = true, Location = new Point(18, 88), Text = "Text to replace the XXXXXX placeholder:" };
            txtDescription = new TextBox { Location = new Point(21, 113), Size = new Size(535, 27) };
            Label lblNewCaption = new Label { AutoSize = true, Location = new Point(18, 149), Text = "New name:" };
            lblNewName = new Label { AutoEllipsis = true, BorderStyle = BorderStyle.FixedSingle, Location = new Point(21, 174), Size = new Size(535, 29), TextAlign = ContentAlignment.MiddleLeft };
            lblError = new Label { AutoSize = false, ForeColor = Color.Firebrick, Location = new Point(21, 207), Size = new Size(350, 27) };

            Button btnOk = new Button { Location = new Point(400, 209), Size = new Size(75, 30), Text = actionText };
            btnOk.Click += btnOk_Click;
            Button btnCancel = new Button { DialogResult = DialogResult.Cancel, Location = new Point(481, 209), Size = new Size(75, 30), Text = "Cancel" };

            txtDescription.TextChanged += delegate { UpdatePreview(); };
            Controls.AddRange(new Control[] { lblOriginalCaption, lblOriginal, lblDescription, txtDescription, lblNewCaption, lblNewName, lblError, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
            UpdatePreview();
        }

        public string NewName { get { return lblNewName.Text; } }

        public static bool TryGet(IWin32Window owner, string title, string originalName, string templateName, string actionText, out string newName)
        {
            using TemplateFilePrompt dialog = new TemplateFilePrompt(title, originalName, templateName, actionText);
            bool accepted = dialog.ShowDialog(owner) == DialogResult.OK;
            newName = accepted ? dialog.NewName : string.Empty;
            return accepted;
        }

        private void UpdatePreview()
        {
            lblNewName.Text = PlaceholderPattern.Replace(templateName, txtDescription.Text.Trim(), 1);
            lblError.Text = string.Empty;
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            string value = txtDescription.Text.Trim();
            if (value.Length == 0)
            {
                lblError.Text = "Enter the replacement text.";
                txtDescription.Focus();
                return;
            }
            if (value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                lblError.Text = "The replacement contains a character that is not valid in a filename.";
                txtDescription.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
