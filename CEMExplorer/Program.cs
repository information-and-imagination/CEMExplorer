using System;
using System.Windows.Forms;

namespace CEMExplorer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using frmProjectWizard wizard = new frmProjectWizard();
            if (wizard.ShowDialog() == DialogResult.OK)
                Application.Run(new frmMain(wizard.ProjectRoot));
        }
    }
}
