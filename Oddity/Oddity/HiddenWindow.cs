using EasyHook;
using OddityHookLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oddity
{

    public partial class HiddenWindow : Form
    {
        private static string _targetExe = "";
        private static String _channelName;

        public HiddenWindow()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            _targetExe = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ODTGame.exe");
            Int32 targetPID = 0;
            try
            {
                if (!File.Exists(_targetExe))
                {
                    Console.WriteLine(String.Format("{0} introuvable !", _targetExe));
                    return;
                }

                RemoteHooking.IpcCreateServer<IpcInterface>(ref _channelName, WellKnownObjectMode.SingleCall);
                string injectionLibrary = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "OddityHookLib.dll");
                RemoteHooking.CreateAndInject(_targetExe, "", 0, InjectionOptions.DoNotRequireStrongName, injectionLibrary, injectionLibrary, out targetPID, _channelName);
                Console.WriteLine("ODT lancé avec succès.");

                string processName = Path.GetFileNameWithoutExtension(_targetExe);
                while(Process.GetProcessesByName(processName).Length >= 1)
                {
                    Application.DoEvents();
                }
                Application.Exit();

            }
            catch
            {
                try
                {
                    System.Diagnostics.Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ODTGame.exe"));
                }
                catch(Exception exception)
                {
                    Console.WriteLine("Erreur grave apparue lors du lancement de ODT \r\n{0}", exception.ToString());
                }
            }
        }
    }
}
