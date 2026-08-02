using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CEMExplorer
{
    internal sealed class AbbreviationPrompt : Form
    {
        private readonly TextBox txtAbbreviation;
        private readonly Label lblError;

        private AbbreviationPrompt()
        {
            Text = "Project Abbreviation";
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(430, 170);

            Label prompt = new Label
            {
                AutoSize = true,
                Location = new Point(18, 18),
                Text = "Enter the abbreviation that will replace SKLTN:"
            };

            txtAbbreviation = new TextBox
            {
                CharacterCasing = CharacterCasing.Upper,
                Location = new Point(21, 49),
                MaxLength = 16,
                Size = new Size(385, 25)
            };

            lblError = new Label
            {
                AutoSize = false,
                ForeColor = Color.Firebrick,
                Location = new Point(21, 78),
                Size = new Size(385, 26)
            };

            Button btnOk = new Button
            {
                Location = new Point(250, 119),
                Size = new Size(75, 30),
                Text = "OK"
            };
            btnOk.Click += btnOk_Click;

            Button btnCancel = new Button
            {
                DialogResult = DialogResult.Cancel,
                Location = new Point(331, 119),
                Size = new Size(75, 30),
                Text = "Cancel"
            };

            Controls.AddRange(new Control[] { prompt, txtAbbreviation, lblError, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        public string Abbreviation { get { return txtAbbreviation.Text.Trim(); } }

        public static bool TryGet(IWin32Window owner, out string abbreviation)
        {
            using AbbreviationPrompt dialog = new AbbreviationPrompt();
            bool accepted = dialog.ShowDialog(owner) == DialogResult.OK;
            abbreviation = accepted ? dialog.Abbreviation : string.Empty;
            return accepted;
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            string value = Abbreviation;
            if (!Regex.IsMatch(value, "^[A-Z][A-Z0-9_-]{1,15}$"))
            {
                lblError.Text = "Use 2–16 letters, numbers, underscores, or hyphens; start with a letter.";
                txtAbbreviation.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
