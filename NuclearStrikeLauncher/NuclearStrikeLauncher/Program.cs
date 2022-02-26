using EasyHook;
using NuclearStrikeHookLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;

namespace NuclearStrikeLauncher
{
    class Program
    {
        private static string _targetExe = "";
        private static String ChannelName;

        static void Main(string[] args)
        {
            _targetExe = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "NUKEPC.EXE");
            Int32 targetPID = 0;

            try
            {
                RemoteHooking.IpcCreateServer<IpcInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);
                string injectionLibrary = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "NuclearStrikeHookLib.dll");
                RemoteHooking.CreateAndInject(_targetExe, "", 0, InjectionOptions.DoNotRequireStrongName, injectionLibrary, injectionLibrary, out targetPID, ChannelName);
                Console.WriteLine("Created and injected process {0}", targetPID);
                while (true)
                {
                    Thread.Sleep(2000);
                    if (Process.GetProcessesByName("NUKEPC").Length <= 0)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("There was an error while connecting to target:\r\n{0}", exception.ToString());
                Console.WriteLine("<Press any key to exit>");
            }
        }
    }
}
