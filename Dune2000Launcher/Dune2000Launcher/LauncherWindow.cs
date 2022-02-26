using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Dune2000Launcher
{
    public partial class LauncherWindow : Form
    {
        public LauncherWindow()
        {
            InitializeComponent();
            DesactiverDgVoodoo2CheckBox.Checked = (Properties.Settings.Default.IsDgVoodoo2Enabled == false);
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsDgVoodoo2Enabled = (DesactiverDgVoodoo2CheckBox.Checked == false);
            Properties.Settings.Default.Save();
        }

        private void JouerButton_Click(object sender, EventArgs e)
        {
            string workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (DesactiverDgVoodoo2CheckBox.Checked)
            {
                CopyConfigResource(workDir, "Dune2000Launcher.dgVoodooDisabled.conf");
            }
            else
            {
                CopyConfigResource(workDir, "Dune2000Launcher.dgVoodooEnabled.conf");
            }

            string exePath = Path.Combine(workDir, "Dune2000.exe");
            try
            {
                System.Diagnostics.Process.Start(exePath);
                Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("{0} : {1}", exePath, ex.Message), ex.ToString());
            }
        }

        private void CopyConfigResource(string destDir, string resourceName)
        {
            var mp3exeFileStream = GetType().Assembly.GetManifestResourceStream(resourceName);
            byte[] bytes = new byte[(int)mp3exeFileStream.Length];
            mp3exeFileStream.Read(bytes, 0, bytes.Length);
            string destPath = Path.Combine(destDir, "dgVoodoo.conf");
            if(File.Exists(destPath))
            {
                FileInfo configFile = new FileInfo(destPath);
                configFile.IsReadOnly = false;
                File.Delete(destPath);
            }
            File.WriteAllBytes(destPath, bytes);
        }
    }
}
