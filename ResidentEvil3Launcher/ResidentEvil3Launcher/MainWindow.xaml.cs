using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ResidentEvil3Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _gameExeName = "ResidentEvil3.exe";

        private string _mercenariesExeName = "RE3_MERCE.exe";

        private string _settingsExeName = "RE3Settings.exe";

        public MainWindow()
        {
            LaunchCommand = new DelegateCommand(Launch_Execute, Launch_CanExecute);
            MercenariesCommand = new DelegateCommand(Mercenaries_Execute, Mercenaries_CanExecute);
            ReglagesCommand = new DelegateCommand(Reglages_Execute, Reglages_CanExecute);
            ControlsCommand = new DelegateCommand(ControlsCommand_Execute);
            QuitterCommand = new DelegateCommand(Quitter_Execute);

            InitializeComponent();
        }

        public DelegateCommand ControlsCommand { get; private set; }

        public DelegateCommand LaunchCommand { get; private set; }

        public DelegateCommand MercenariesCommand { get; private set; }

        public DelegateCommand QuitterCommand { get; private set; }

        public DelegateCommand ReglagesCommand { get; private set; }

        private static void AppendControls(StringBuilder bigStr)
        {
            bigStr.AppendLine("Commandes par défaut au clavier :");
            bigStr.AppendLine(" Touches fléchées ou pavé numérique : se déplacer");
            bigStr.AppendLine(" C ou Espace ou Entrée : Action/Choisir/Frapper/Tirer/Zoom avant/Zoom arrière");
            bigStr.AppendLine(" V ou Échap : Courir/Annuler");
            bigStr.AppendLine(" , ou F6 : Plan/Passer cinématique/Passer ouverture porte");
            bigStr.AppendLine(" W ou F4 : Inventaire");
            bigStr.AppendLine(" S : Viser les ennemis uniquement");
            bigStr.AppendLine(" B : Changer de cible");
            bigStr.AppendLine(" X : Viser les ennemis et les objets");
            bigStr.AppendLine(" F5 : Options du jeu");
            bigStr.AppendLine(" ALT + F4 : QUITTER LE JEU");
            bigStr.AppendLine();
            bigStr.AppendLine("Commandes par défaut à la manette (testé avec une manette Xbox 360) :");
            bigStr.AppendLine(" Pad directionnel : se déplacer");
            bigStr.AppendLine(" A : Action/Choisir/Frapper/Tirer/Zoom avant/Zoom arrière");
            bigStr.AppendLine(" B : Courir/Annuler");
            bigStr.AppendLine(" X : Plan/Passer cinématique/Passer ouverture porte");
            bigStr.AppendLine(" Y : Inventaire");
            bigStr.AppendLine(" LB : Viser les ennemis uniquement");
            bigStr.AppendLine(" RB : Changer de cible");
            bigStr.AppendLine(" Appui sur pad directionnel : Viser les ennemis et les objets");
        }

        private void ControlsCommand_Execute(object parameters)
        {
            StringBuilder controlsString = new StringBuilder();
            AppendControls(controlsString);
            MessageBox.Show(controlsString.ToString());
        }

        private bool IsProcessNotPresent(string processName)
        {
            return Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(processName)).Length <= 0;
        }

        private bool Launch_CanExecute(object parameters)
        {
            return IsProcessNotPresent(_gameExeName);
        }

        private void Launch_Execute(object parameters)
        {
            TryStartProcess(_gameExeName);
        }

        private bool Mercenaries_CanExecute(object parameters)
        {
            return IsProcessNotPresent(_mercenariesExeName);
        }

        private void Mercenaries_Execute(object parameters)
        {
            TryStartProcess(_mercenariesExeName);
        }

        private void Quitter_Execute(object parameters)
        {
            Close();
        }

        private bool Reglages_CanExecute(object parameters)
        {
            return IsProcessNotPresent(_settingsExeName);
        }

        private void Reglages_Execute(object parameters)
        {
            TryStartProcess(_settingsExeName);
        }

        private void TryStartProcess(string exeFileName)
        {
            try
            {
                Process.Start(exeFileName);
            }
            catch (Win32Exception we)
            {
                MessageBox.Show(String.Format("{0} : {1} {2}", we.Message, Environment.NewLine, System.IO.Path.Combine(Assembly.GetExecutingAssembly().Location, exeFileName)));
            }
        }
    }
}