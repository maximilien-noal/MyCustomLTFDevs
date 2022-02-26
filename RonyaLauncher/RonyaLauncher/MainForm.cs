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

namespace RonyaLauncher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DeactivateDdrawCompatCheckBox.Checked = Properties.Settings.Default.DisableDdrawCompat;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisableDdrawCompat = DeactivateDdrawCompatCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void JouerButton_Click(object sender, EventArgs e)
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(basePath, "Game.exe");
            string ddrawCompatPath = Path.Combine(basePath, "ddraw.dll");
            try
            {
                try
                {
                    if (DeactivateDdrawCompatCheckBox.Checked)
                    {
                        if(File.Exists(ddrawCompatPath))
                        {
                            File.Delete(ddrawCompatPath);
                        }
                    }
                    else
                    {
                        CopyConfigResource(basePath, "RonyaLauncher.ddraw.dll");
                    }
                }
                catch
                {

                }

                var psinfo = new ProcessStartInfo(path);
                var process = new Process();
                process.StartInfo = psinfo;
                if(process.Start())
                {
                    Application.Exit();
                }
                else
                {
                    throw new Exception("Jeu non démarrable");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", path, Environment.NewLine, ex.Message));
            }
        }

        private void CopyConfigResource(string destDir, string resourceName)
        {
            var ddrawCompatDllFileStream = GetType().Assembly.GetManifestResourceStream(resourceName);
            byte[] bytes = new byte[(int)ddrawCompatDllFileStream.Length];
            ddrawCompatDllFileStream.Read(bytes, 0, bytes.Length);
            string destPath = Path.Combine(destDir, "ddraw.dll");
            if (File.Exists(destPath))
            {
                FileInfo dll = new FileInfo(destPath);
                dll.IsReadOnly = false;
                File.Delete(destPath);
            }
            File.WriteAllBytes(destPath, bytes);
        }

        private void LisezMoiButton_Click(object sender, EventArgs e)
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(basePath, "Lisezmoi.htm");
            try
            {
                var psinfo = new ProcessStartInfo(path);
                psinfo.UseShellExecute = true;
                var process = new Process();
                process.StartInfo = psinfo;
                if (process.Start() == false)
                {
                    throw new Exception("Lisez-moi non démarrable");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0} :{1} {2}", path, Environment.NewLine, ex.Message));
            }
        }
    }
}
