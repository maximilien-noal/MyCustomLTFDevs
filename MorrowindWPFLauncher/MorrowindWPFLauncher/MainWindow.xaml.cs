using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MorrowindWPFLauncher
{
    public partial class MainWindow : Window
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private string _workdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string MorrowindName = "Morrowind.exe";
        private const string MorrowindPatchedName = "Morrowind.Patched.bak";
        private const string MGEXEguiName = "MGEXEgui.exe";
        private const string MGSOOptionsName = "MGSO Options.exe";
        private const string MWCodePatchName = "Morrowind Code Patch.exe";
        private const string ModManagerRelativePath = @"Mash\Mopy\mash.exe";

        public DelegateCommand Jouer { get; private set; }
        public DelegateCommand Mgexegui { get; private set; }
        public DelegateCommand Mgso_Options { get; private set; }
        public DelegateCommand Morrowind_Code_Patch { get; private set; }
        public DelegateCommand Reset_Morrowind_Code_Patch { get; private set; }
        public DelegateCommand ModManager { get; private set; }
        public DelegateCommand Quitter { get; private set; }

        public MainWindow()
        {
            Jouer = new DelegateCommand(Jouer_Execute);
            Mgexegui = new DelegateCommand(Mgexegui_Execute);
            Mgso_Options = new DelegateCommand(Mgso_Options_Execute);
            Morrowind_Code_Patch = new DelegateCommand(Morrowind_Code_Patch_Execute);
            Reset_Morrowind_Code_Patch = new DelegateCommand(Reset_Morrowind_Code_Patch_Execute);
            ModManager = new DelegateCommand(ModManager_Execute);
            Quitter = new DelegateCommand(Quitter_Execute);
            InitializeComponent();
        }

        private void Jouer_Execute(object parameters)
        {
            ApplyCrack();
            Execute(MorrowindName);
        }

        private void ApplyCrack()
        {
            string path = Path.Combine(_workdir, MorrowindName);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 93812; //16E74
                stream.WriteByte(HexConverter.GetStringToBytes("EB")[0]); //was 75
                stream.Position = 93896; //16EC8
                stream.WriteByte(HexConverter.GetStringToBytes("EB")[0]); //was 75
                stream.Position = 94018; //16F42
                foreach (Byte newbyte in HexConverter.GetStringToBytes("E9FA00000090")) //was 0F85F9000090
                {
                    stream.WriteByte(newbyte);
                }
            }
        }

        private void RemoveCrack()
        {
            string path = Path.Combine(_workdir, MorrowindName);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 93812; //16E74
                stream.WriteByte(HexConverter.GetStringToBytes("75")[0]);
                stream.Position = 93896; //16EC8
                stream.WriteByte(HexConverter.GetStringToBytes("75")[0]);
                stream.Position = 94018; //16F42
                foreach (Byte newbyte in HexConverter.GetStringToBytes("0F85F9000000"))
                {
                    stream.WriteByte(newbyte);
                }
            }
        }

        private void Mgexegui_Execute(object parameters)
        {
            Execute(MGEXEguiName);
        }

        private void Mgso_Options_Execute(object parameters)
        {
            Execute(MGSOOptionsName);
        }

        private void Morrowind_Code_Patch_Execute(object parameters)
        {
            RemoveCrack();
            Execute(MWCodePatchName);
        }

        private void Reset_Morrowind_Code_Patch_Execute(object parameters)
        {
            string sourcePath = Path.Combine(_workdir, MorrowindPatchedName);
            string destPath = Path.Combine(_workdir, MorrowindName);

            if(File.Exists(sourcePath) == false || File.Exists(destPath) == false)
            {
                return;
            }

            MessageBoxResult mbr = MessageBox.Show("Remettre les patchs par défaut ?", "Restaurer patchs Morrowind", MessageBoxButton.OKCancel);

            if(mbr == MessageBoxResult.OK)
            {
                File.Copy(sourcePath, destPath, true);
            }
        }

        private void ModManager_Execute(object parameters)
        {
            Execute(ModManagerRelativePath);
        }

        private void Quitter_Execute(object parameters)
        {
            Application.Current.Shutdown();
        }

        private void Execute(string exeName)
        {
            string path = Path.Combine(_workdir, exeName);
            string processName = exeName.Replace(".exe", "");

            Process[] existingProcessArray = Process.GetProcessesByName(processName);
            Process existingProcess = null;
            if (existingProcessArray.Length >= 1)
            {
                existingProcess = existingProcessArray.FirstOrDefault();
                if(existingProcess != null)
                {
                    try
                    {
                        SetForegroundWindow(existingProcess.MainWindowHandle);
                    }
                    catch
                    {

                    }
                    return;
                }
            }

            try
            {
                var psi = new ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = path;
                psi.WorkingDirectory = Path.GetDirectoryName(path);

                Process.Start(psi);

                if(exeName == MorrowindName)
                {
                    Application.Current.Shutdown();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(String.Format("{0} : {1}", path, e.Message));
            }
        }
    }
}
