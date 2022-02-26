using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace MGSLauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Play = new DelegateCommand(Play_Execute);
            PlayVR = new DelegateCommand(PlayVR_Execute);
            ShowVideoFolder = new DelegateCommand(ShowVideoFolder_Execute);
            SeeDefaultCommands = new DelegateCommand(SeeDefaultCommands_Execute);

            InitializeComponent();
        }

        public DelegateCommand Play { get; private set; }

        public DelegateCommand PlayVR { get; private set; }

        public DelegateCommand SeeDefaultCommands { get; private set; }

        public DelegateCommand ShowVideoFolder { get; private set; }

        private void LaunchGame(string executable)
        {
            var psInfo = new ProcessStartInfo(executable);
            var gameProcess = new Process();
            gameProcess.StartInfo = psInfo;

            if (gameProcess.Start())
            {
                Application.Current.Shutdown();
            }
        }

        private void Play_Execute(object parameters)
        {
            LaunchGame("MGSI.EXE");
        }

        private void PlayVR_Execute(object parameters)
        {
            LaunchGame("MGSVR.EXE");
        }

        private void SeeDefaultCommands_Execute(object parameters)
        {
            DefaultControlsWindow defaultControlsWindow = new DefaultControlsWindow();
            defaultControlsWindow.Owner = this;
            defaultControlsWindow.ShowDialog();
        }

        private void ShowVideoFolder_Execute(object parameters)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WMV");

            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show($"Le dossier {path} est introuvable.");
            }
        }
    }
}