using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RogueSpearLauncher
{
    public partial class MainForm : Form
    {
        private string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainForm()
        {
            InitializeComponent();

            if(Properties.Settings.Default.DdrawCompatCompatibleOS == false)
            {
                DeactivateDdrawCompatCheckbox.Visible = false;
                DeactivateDdrawCompatLabel.Visible = false;
            }

            DeactivateDdrawCompatCheckbox.Checked = Properties.Settings.Default.DeactivateDdrawCompat;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.DeactivateDdrawCompat = DeactivateDdrawCompatCheckbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void CopyConfigResource(string destDir, string resourceName)
        {
            try
            {
                var ddrawCompatDll = GetType().Assembly.GetManifestResourceStream(resourceName);
                byte[] bytes = new byte[(int)ddrawCompatDll.Length];
                ddrawCompatDll.Read(bytes, 0, bytes.Length);
                string destPath = Path.Combine(destDir, "ddraw.dll");
                if (File.Exists(destPath))
                {
                    FileInfo configFile = new FileInfo(destPath);
                    configFile.IsReadOnly = false;
                    configFile.Delete();
                }
                File.WriteAllBytes(destPath, bytes);
            }
            catch
            {

            }
        }

        private bool StartProcess(string fullPath)
        {
            if (Properties.Settings.Default.DeactivateDdrawCompat)
            {
                try
                {
                    if(File.Exists(Path.Combine(_workDir, "ddraw.dll")))
                    {
                        FileInfo file = new FileInfo(Path.Combine(_workDir, "ddraw.dll"));
                        file.IsReadOnly = false;
                        file.Delete();
                    }
                }
                catch
                {

                }
            }
            else
            {
                CopyConfigResource(_workDir, "RogueSpearLauncher.ddraw.dll");
            }

            try
            {
                var psi = new ProcessStartInfo(fullPath);
                var process = new Process();
                process.StartInfo = psi;
                return process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", fullPath, Environment.NewLine, ex.Message), "Erreur");
                return false;
            }
        }

        private void JouerButton_Click(object sender, EventArgs e)
        {
            if(StartProcess(Path.Combine(_workDir, "RogueSpear.exe")))
            {
                Application.Exit();
            }
        }

        private void DeactivateDdrawCompatCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DeactivateDdrawCompat = DeactivateDdrawCompatCheckbox.Checked;
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {
            if(StartProcess(Path.Combine(_workDir, "RSEditor.exe")))
            {
                Application.Exit();
            }
        }
    }
}
