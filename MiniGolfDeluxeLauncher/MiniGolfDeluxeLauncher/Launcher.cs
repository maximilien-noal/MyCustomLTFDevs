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

namespace MiniGolfDeluxeLauncher
{
    public partial class RootForm : Form
    {
        private string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public RootForm()
        {
            InitializeComponent();
            radioButtonMIDI.Checked = IsMIDIEnabled();
            radioButtonMP3.Checked = IsMP3Enabled();
        }

        private bool IsMP3Enabled()
        {
            return File.Exists(Path.Combine(_workDir, "mp3play.exe"));
        }

        private bool IsMIDIEnabled()
        {
            return IsMP3Enabled() == false;
        }

        private void JouerButton_Click(object sender, EventArgs e)
        {
            try
            {
                string playerDestPath = Path.Combine(_workDir, "mp3play.exe");
                if (radioButtonMP3.Checked)
                {
                    if(File.Exists(playerDestPath) == false)
                    {
                        var mp3exeFileStream = GetType().Assembly.GetManifestResourceStream("MiniGolfDeluxeLauncher.mp3play.exe");
                        byte[] bytes = new byte[(int)mp3exeFileStream.Length];
                        mp3exeFileStream.Read(bytes, 0, bytes.Length);
                        File.WriteAllBytes(playerDestPath, bytes);
                    }
                }
                else if (radioButtonMIDI.Checked)
                {
                    if(File.Exists(playerDestPath))
                    {
                        FileInfo mp3playerFile = new FileInfo(playerDestPath);
                        mp3playerFile.IsReadOnly = false;
                        File.Delete(playerDestPath);
                    }
                }
            }
            catch
            {

            }

            string exePath = Path.Combine(_workDir, "Minigolf.exe");

            try
            {
                Process.Start(exePath);
                Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(exePath, ex.Message);
            }
        }
    }
}
