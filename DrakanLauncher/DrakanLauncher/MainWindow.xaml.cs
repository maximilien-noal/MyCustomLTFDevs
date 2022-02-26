using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DrakanLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DelegateCommand LaunchGame { get; private set; }
        public DelegateCommand ShowDefaultCommands { get; private set; }
        public DelegateCommand RiotEngineSetup { get; private set; }
        public DelegateCommand DgVoodooSetup { get; private set; }
        public DelegateCommand ShowGameFiles { get; private set; }
        public DelegateCommand ExitLauncher { get; private set; }

        public bool CanLaunchDgVoodoo { get; private set; }

        private string _workingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainWindow()
        {
            LaunchGame = new DelegateCommand(LaunchGame_Execute, LaunchGame_CanExecute);
            ShowDefaultCommands = new DelegateCommand(ShowDefaultCommands_Execute);
            RiotEngineSetup = new DelegateCommand(RiotEngineSetup_Execute, RiotEngineSetup_CanExecute);
            DgVoodooSetup = new DelegateCommand(DgVoodooSetup_Execute, DgVoodooSetup_CanExecute);
            ShowGameFiles = new DelegateCommand(ShowGameFiles_Execute);
            ExitLauncher = new DelegateCommand(ExitLauncher_Execute);
            CanLaunchDgVoodoo = DgVoodooSetup_CanExecute(this);
            InitializeComponent();
        }

        public void LaunchGame_Execute(object parameters)
        {
            bool result = false;
            try
            {
                if(System.Diagnostics.Process.Start(Path.Combine(_workingDir, "Drakan.exe")) != null)
                {
                    result = true;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if(result == true)
                {
                    ExitLauncher.Execute(this);
                }
            }
        }

        public bool LaunchGame_CanExecute(object parameters)
        {
            return File.Exists(Path.Combine(_workingDir, "Drakan.exe")) && Process.GetProcessesByName("Drakan").Length <= 0;
        }

        public void ShowDefaultCommands_Execute(object parameters)
        {
            DefaultCommandsWindow defaultCommands = new DefaultCommandsWindow();
            defaultCommands.ShowDialog();
        }

        public void RiotEngineSetup_Execute(object parameters)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.Combine(_workingDir, "Drakan.exe"), "-setup");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool RiotEngineSetup_CanExecute(object parameters)
        {
            return File.Exists(Path.Combine(_workingDir, "Drakan.exe")) && Process.GetProcessesByName("Drakan").Length <= 0;
        }

        public void DgVoodooSetup_Execute(object parameters)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.Combine(_workingDir, "dgVoodooSetup.exe"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool DgVoodooSetup_CanExecute(object parameters)
        {
            return File.Exists(Path.Combine(_workingDir, "dgVoodooSetup.exe")) && Process.GetProcessesByName("dgVoodooSetup").Length <= 0;
        }

        public void ShowGameFiles_Execute(object parameters)
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = _workingDir;
            Process.Start(pInfo);
        }

        public void ExitLauncher_Execute(object parameters)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
