using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ResidentEvil2Launcher
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public static readonly DependencyProperty IsSoftwareModeForcedProperty =
            DependencyProperty.Register(nameof(IsSoftwareModeForced), typeof(bool), typeof(OptionsWindow), new PropertyMetadata(false));

        public static readonly DependencyProperty IsUsingNewDdrawCompatProperty =
            DependencyProperty.Register(nameof(IsUsingNewDdrawCompat), typeof(bool), typeof(OptionsWindow), new PropertyMetadata(false));

        public static readonly DependencyProperty IsUsingOldDdrawCompatProperty =
            DependencyProperty.Register(nameof(IsUsingOldDdrawCompat), typeof(bool), typeof(OptionsWindow), new PropertyMetadata(false));

        public OptionsWindow()
        {
            SetCurrentValue(IsSoftwareModeForcedProperty, RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseSoftwareMode);
            SetCurrentValue(IsUsingOldDdrawCompatProperty, RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseOldDdrawCompat);
            SetCurrentValue(IsUsingNewDdrawCompatProperty, RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseNewDdrawCompat);

            Ok = new DelegateCommand(Ok_Execute);

            Annuler = new DelegateCommand(delegate { Close(); });

            InitializeComponent();
        }

        public DelegateCommand Annuler { get; private set; }

        public bool IsSoftwareModeForced
        {
            get { return (bool)GetValue(IsSoftwareModeForcedProperty); }
            set { SetValue(IsSoftwareModeForcedProperty, value); }
        }

        public bool IsUsingNewDdrawCompat
        {
            get { return (bool)GetValue(IsUsingNewDdrawCompatProperty); }
            set { SetValue(IsUsingNewDdrawCompatProperty, value); }
        }

        public bool IsUsingOldDdrawCompat
        {
            get { return (bool)GetValue(IsUsingOldDdrawCompatProperty); }
            set { SetValue(IsUsingOldDdrawCompatProperty, value); }
        }

        public DelegateCommand Ok { get; private set; }

        private void Ok_Execute(object parameters)
        {
            if (IsSoftwareModeForced != RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseSoftwareMode)
            {
                Process modificateurProcess = new Process();
                ProcessStartInfo psInfo = new ProcessStartInfo();
                psInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ModificateurOptionsResidentEvil2.exe");

                if (File.Exists(psInfo.FileName) == false)
                {
                    return;
                }

                modificateurProcess.StartInfo = psInfo;
                modificateurProcess.EnableRaisingEvents = true;

                if (IsSoftwareModeForced)
                {
                    psInfo.Arguments = "SOFTWARE";
                }

                bool processStarted = modificateurProcess.Start();
                modificateurProcess.WaitForExit();

                if (processStarted && modificateurProcess.HasExited && modificateurProcess.ExitCode == 0)
                {
                    RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseSoftwareMode = IsSoftwareModeForced;
                }
            }
            if (IsUsingNewDdrawCompat != RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseNewDdrawCompat)
            {
                try
                {
                    if (File.Exists(@"ddrawNew\ddraw.dll"))
                    {
                        File.Copy(@"ddrawNew\ddraw.dll", "ddraw.dll", true);
                    }
                    RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseNewDdrawCompat = IsUsingNewDdrawCompat;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            if (IsUsingOldDdrawCompat != RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseOldDdrawCompat)
            {
                try
                {
                    if (File.Exists(@"ddrawOld\ddraw.dll"))
                    {
                        File.Copy(@"ddrawOld\ddraw.dll", "ddraw.dll", true);
                    }
                    RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.UseOldDdrawCompat = IsUsingOldDdrawCompat;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            RESIDENT_EVIL_2_LAUNCHER.Properties.Settings.Default.Save();
            Close();
        }
    }
}