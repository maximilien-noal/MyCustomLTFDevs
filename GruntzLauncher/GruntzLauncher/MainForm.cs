using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace GruntzLauncher
{
    public partial class MainForm : Form
    {
        private readonly string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(Path.Combine(_workDir, "D3DImm.dll")) == false)
            {
                DeactivateDgVoodoo2CheckBox.Visible = false;
                DeactivateDgVoodoo2Label.Visible = false;
            }
            else
            {
                DeactivateDgVoodoo2CheckBox.Checked = Properties.Settings.Default.DeactivateDgVoodoo2;
            }

            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(_workDir, "D3DImm.dll")))
            {
                Properties.Settings.Default.DeactivateDgVoodoo2 = DeactivateDgVoodoo2CheckBox.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void CopyConfigResource(string destDir, string resourceName)
        {
            var dgVoodooConfigFile = GetType().Assembly.GetManifestResourceStream(resourceName);
            byte[] bytes = new byte[(int)dgVoodooConfigFile.Length];
            dgVoodooConfigFile.Read(bytes, 0, bytes.Length);
            string destPath = Path.Combine(destDir, "dgVoodoo.conf");
            if (File.Exists(destPath))
            {
                FileInfo configFile = new FileInfo(destPath)
                {
                    IsReadOnly = false
                };
                File.Delete(destPath);
            }
            File.WriteAllBytes(destPath, bytes);
        }

        private void DeactivateDgVoodoo2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DeactivateDgVoodoo2 = DeactivateDgVoodoo2CheckBox.Checked;
        }

        private void DgVoodoo2OptionsButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(_workDir, "dgVoodooCpl.exe");
            try
            {
                if (StartProcess(path))
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", path, Environment.NewLine, ex.Message), "Erreur");
            }
        }

        private void EditeurButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(_workDir, "GruntzEdit.exe");
            try
            {
                if (StartProcess(path))
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", path, Environment.NewLine, ex.Message), "Erreur");
            }
        }

        private void HelpEditorButton_Click(object sender, EventArgs e)
        {
            string helpPath = Path.Combine(_workDir, "gruntzEdit.hlp");
            try
            {
                if (File.Exists(Path.Combine(_workDir, "gruntzEdit.chm")))
                {
                    helpPath = Path.Combine(_workDir, "gruntzEdit.chm");
                }

                if (StartProcess(helpPath))
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", helpPath, Environment.NewLine, ex.Message), "Erreur");
            }
        }

        private void HelpGameButton_Click(object sender, EventArgs e)
        {
            string helpPath = Path.Combine(_workDir, "gruntz.hlp");
            try
            {
                if (File.Exists(Path.Combine(_workDir, "gruntz.chm")))
                {
                    helpPath = Path.Combine(_workDir, "gruntz.chm");
                }

                if (StartProcess(helpPath))
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", helpPath, Environment.NewLine, ex.Message), "Erreur");
            }
        }

        private void JouerButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(_workDir, "Gruntz.exe");
            try
            {
                if (StartProcess(path))
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", path, Environment.NewLine, ex.Message), "Erreur");
            }
        }

        private bool StartProcess(string fullPath)
        {
            if (File.Exists(Path.Combine(_workDir, "D3DImm.dll")))
            {
                if (Properties.Settings.Default.DeactivateDgVoodoo2)
                {
                    CopyConfigResource(_workDir, "GruntzLauncher.dgVoodooDisabled.conf");
                }
                else
                {
                    CopyConfigResource(_workDir, "GruntzLauncher.dgVoodooEnabled.conf");
                }
            }

            var psi = new ProcessStartInfo(fullPath);
            var process = new Process
            {
                StartInfo = psi
            };
            return process.Start();
        }
    }
}