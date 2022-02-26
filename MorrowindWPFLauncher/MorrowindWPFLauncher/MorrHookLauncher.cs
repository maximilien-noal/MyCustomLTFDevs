using EasyHook;
using MorrHookLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;

namespace MorrowindWPFLauncher
{
    public static class MorrHookLauncher
    {
        private static string _targetExe = "";
        private static String ChannelName;

        public static void Launch()
        {
            _targetExe = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Morrowind.exe");
            Int32 targetPID = 0;
            try
            {
                if (!File.Exists(_targetExe))
                {
                    Console.WriteLine(String.Format("{0} introuvable !", _targetExe));
                    return;
                }

                RemoteHooking.IpcCreateServer<IpcInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);
                string injectionLibrary = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "MorrHookLib.dll");
                RemoteHooking.CreateAndInject(_targetExe, "", 0, InjectionOptions.DoNotRequireStrongName, injectionLibrary, injectionLibrary, out targetPID, ChannelName);
                Console.WriteLine("Morrowind lancé avec succès.");

                string processName = Path.GetFileNameWithoutExtension(_targetExe);

                while (Process.GetProcessesByName(processName).Length >= 1)
                {
                }

                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Erreur grave apparue lors du lancement de Morrowind \r\n{0}", exception.ToString());
            }
        }
    }
}
