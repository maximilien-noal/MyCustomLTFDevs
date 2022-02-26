using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace ModificateurOptionsResidentEvil2
{
    class Program
    {
        static void Main(string[] args)
        {

            Process cmdProc = new Process();
            ProcessStartInfo psInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DisplayDriverHardware.cmd"));
            cmdProc.StartInfo = psInfo;
            cmdProc.EnableRaisingEvents = true;

            if (args.Length != 0 && args[0] == "SOFTWARE")
            {
                psInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DisplayDriverSoftware.cmd");
            }

            if(File.Exists(psInfo.FileName) == false)
            {
                Environment.ExitCode = 1;
            }

            cmdProc.Start();

            cmdProc.WaitForExit();

            Environment.ExitCode = cmdProc.ExitCode;
            Environment.Exit(Environment.ExitCode);
        }
    }
}
