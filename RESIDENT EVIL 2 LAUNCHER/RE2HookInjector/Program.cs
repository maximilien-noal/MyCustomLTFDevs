using EasyHook;
using RE2LaunchLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;

namespace RE2HookInjector
{
    class Program
    {
        private static string _channelName;
        private static int _targetPID;

        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                return;
            }

            string processName = args[0];
            string exeFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), String.Format("{0}.EXE", processName));
            if (System.IO.File.Exists(exeFileName) == false)
            {
                Console.WriteLine(String.Format("{0} n'existe pas ou n'est pas accessible.", exeFileName));
                Console.WriteLine("Appuyez sur une touche pour quitter");
                Console.ReadLine();
                Environment.Exit(1);
            }

            RemoteHooking.IpcCreateServer<IpcInterface>(ref _channelName, WellKnownObjectMode.SingleCall);
            string injectedLibrary = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "RE2LaunchLib.dll");
            RemoteHooking.CreateAndInject(exeFileName, "", 0, InjectionOptions.DoNotRequireStrongName, injectedLibrary, injectedLibrary, out _targetPID, _channelName);
            while (true)
            {
                Thread.Sleep(2000);
                if (Process.GetProcessesByName(processName).Length <= 0)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
