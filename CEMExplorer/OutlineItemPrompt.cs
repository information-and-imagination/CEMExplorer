using System.Drawing;
using System.Windows.Forms;

namespace CEMExplorer
{
    internal sealed class OutlineItemPrompt : Form
    {
        private readonly TextBox txtName;

        private OutlineItemPrompt(string title, string prompt, string initialValue)
        {
            Text = title;
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(470, 142);

            Label label = new Label { AutoSize = true, Location = new Point(18, 18), Text = prompt };
            txtName = new TextBox { Location = new Point(21, 48), Size = new Size(425, 27), Text = initialValue };

            Button btnOk = new Button { DialogResult = DialogResult.OK, Location = new Point(290, 94), Size = new Size(75, 30), Text = "OK" };
            Button btnCancel = new Button { DialogResult = DialogResult.Cancel, Location = new Point(371, 94), Size = new Size(75, 30), Text = "Cancel" };

            Controls.AddRange(new Control[] { label, txtName, btnOk, btnCancel });
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Shown += delegate
            {
                txtName.SelectAll();
                txtName.Focus();
            };
        }

        public static bool TryGet(IWin32Window owner, string title, string prompt, string initialValue, out string value)
        {
            using OutlineItemPrompt dialog = new OutlineItemPrompt(title, prompt, initialValue);
            if (dialog.ShowDialog(owner) == DialogResult.OK && dialog.txtName.Text.Trim().Length > 0)
            {
                value = dialog.txtName.Text.Trim();
                return true;
            }

            value = string.Empty;
            return false;
        }
    }
}
