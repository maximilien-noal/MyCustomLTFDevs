using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;
using TlonHookLibrary;

namespace TlonHookInjector
{
    class Program
    {
        private static String ChannelName;

        static void Main()
        {
            string _targetExe = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tlon.exe");
            Int32 targetPID = 0;
            try
            {
                if (!File.Exists(_targetExe))
                {
                    Console.WriteLine(String.Format("{0} introuvable !", _targetExe));
                    Console.WriteLine("Appuyez sur une touche pour quitter... ");
                    Console.ReadKey();
                    return;
                }

                RemoteHooking.IpcCreateServer<IpcInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);
                string injectionLibrary = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "TlonHookLibrary.dll");
                if(File.Exists(injectionLibrary) == false)
                {
                    Console.WriteLine(String.Format("{0} introuvable !", injectionLibrary));
                    Console.WriteLine("Appuyez sur une touche pour quitter... ");
                    Console.ReadKey();
                    return;
                }
                RemoteHooking.CreateAndInject(_targetExe, "", 0, InjectionOptions.DoNotRequireStrongName, injectionLibrary, injectionLibrary, out targetPID, ChannelName);
                Console.WriteLine("Created and injected process {0}", targetPID);
                while (true)
                {
                    if (Process.GetProcessesByName("Tlon").Length <= 0)
                    {
                        Environment.Exit(0);
                    }
                    Thread.Sleep(2000);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("There was an error while connecting to target:\r\n{0}", exception.ToString());
                Debug.WriteLine("<Press any key to exit>");
            }
        }
    }
}
