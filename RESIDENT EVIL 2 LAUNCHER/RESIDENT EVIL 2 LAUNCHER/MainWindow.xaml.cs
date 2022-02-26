using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ResidentEvil2Launcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LaunchClaireCommand = new DelegateCommand(LaunchClaire_Execute, LaunchGame_CanExecute);
            LaunchLeonCommand = new DelegateCommand(LaunchLeon_Execute, LaunchGame_CanExecute);
            QuitCommand = new DelegateCommand(QuitCommand_Execute);
            ControlsCommand = new DelegateCommand(ControlsCommand_Execute);
            OptionsCommand = new DelegateCommand(OptionsCommand_Execute);

            InitializeComponent();
        }

        public DelegateCommand ControlsCommand { get; private set; }

        public DelegateCommand LaunchClaireCommand { get; private set; }

        public DelegateCommand LaunchLeonCommand { get; private set; }

        public DelegateCommand OptionsCommand { get; private set; }

        public DelegateCommand QuitCommand { get; private set; }

        private static void AppendCommands(StringBuilder bigStr)
        {
            bigStr.AppendLine("Commandes par défaut au clavier :");
            bigStr.AppendLine(" Touches fléchées : se déplacer");
            bigStr.AppendLine(" C ou Espace ou Entrée : Action/Choisir/Frapper/Tirer");
            bigStr.AppendLine(" V : Courir/Annuler");
            bigStr.AppendLine(" X : Pointer/Viser");
            bigStr.AppendLine(" Z : Inventaire");
            bigStr.AppendLine(" A : Plan");
            bigStr.AppendLine(" Ctrl : Options du jeu (permet de configurer les commandes)");
            bigStr.AppendLine(" F7 ou F8 : Alterner la définition d'affichage du jeu entre 320x240 et 640x480");
            bigStr.AppendLine(" ALT + F4 : QUITTER LE JEU");
            bigStr.AppendLine();
            bigStr.AppendLine("Commandes par défaut à la manette (testé avec une manette Xbox 360) :");
            bigStr.AppendLine(" Pad directionnel : se déplacer");
            bigStr.AppendLine(" A : Action/Choisir/Frapper/Tirer");
            bigStr.AppendLine(" B : Courir/Annuler");
            bigStr.AppendLine(" LB : Pointer/Viser");
            bigStr.AppendLine(" Y : Inventaire");
            bigStr.AppendLine(" X : Plan");
            bigStr.AppendLine(" Back : Pause");
            bigStr.AppendLine(" Start : Options du jeu (permet de configurer les commandes)");
            bigStr.AppendLine();
            bigStr.AppendLine(@"/!\ Ne pas utiliser ALT-TAB, ni CTRL-ESC, ni la touche Windows en cours de jeu,");
            bigStr.AppendLine(@"/!\ sous peine de devoir redémarrer le jeu");
        }

        private void ControlsCommand_Execute(object parameters)
        {
            StringBuilder controlsString = new StringBuilder();
            AppendCommands(controlsString);
            MessageBox.Show(controlsString.ToString());
        }

        private void LaunchClaire_Execute(object parameters)
        {
            StartProcess("CLAIREF");
        }

        private bool LaunchGame_CanExecute(object parameters)
        {
            return Process.GetProcessesByName("LEONF").Length <= 0 || Process.GetProcessesByName("CLAIREF").Length <= 0;
        }

        private void LaunchLeon_Execute(object parameters)
        {
            StartProcess("LEONF");
        }

        private void OptionsCommand_Execute(object parameters)
        {
            var options = new OptionsWindow();
            options.Owner = this;
            options.ShowDialog();
        }

        private void QuitCommand_Execute(object parameters)
        {
            Application.Current.MainWindow.Close();
        }

        private bool StartProcess(string processName)
        {
            MessageBox.Show("Si le jeu ne se lance pas, re-essayez en activant le mode graphique logiciel dans les options graphiques");

            string exePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), String.Format("{0}.EXE", processName));
            if (File.Exists(exePath) == false)
            {
                MessageBox.Show(String.Format("{0} introuvable.", exePath));
                return false;
            }

            Process gameProcess = new Process();
            ProcessStartInfo psInfo = new ProcessStartInfo(exePath);
            psInfo.UseShellExecute = true;
            gameProcess.StartInfo = psInfo;
            return gameProcess.Start();
        }
    }
}