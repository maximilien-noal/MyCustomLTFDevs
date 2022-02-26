
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace DXWNDLauncher
{
    class Program
    {
        static void Main()
        {
            StartGame();
        }

        private static void StartGame()
        {
            string startPath = GetStartPath();
            Process dxwndProc = new Process();
            dxwndProc = StartDXWND(startPath);
            Process gameProc = StartGame(startPath);

            gameProc.Exited += delegate
            {
                if (!dxwndProc.HasExited)
                {
                    dxwndProc.Kill();
                }
            };

            while (!gameProc.HasExited)
            {
                Thread.Sleep(400);
            }
        }

        private static string GetStartPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static Process StartDXWND(string startPath)
        {
            string dxwndPath = "dxwnd.exe";

            var dxwndProcess = new Process();

            var dxwndStartInfo = new ProcessStartInfo();
            dxwndStartInfo.WorkingDirectory = startPath;
            dxwndStartInfo.FileName = Path.Combine(startPath, dxwndPath);
            dxwndStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

            dxwndProcess.StartInfo = dxwndStartInfo;
            dxwndProcess.Start();
            return dxwndProcess;
        }

        private static Process StartGame(string startPath)
        {
            string gamePath = "sanguo.exe";

            var gameProcess = new Process();

            var gameStartInfo = new ProcessStartInfo();
            gameStartInfo.WorkingDirectory = startPath;
            gameStartInfo.FileName = Path.Combine(startPath, gamePath);

            gameProcess.StartInfo = gameStartInfo;
            gameProcess.EnableRaisingEvents = true;
            gameProcess.Start();
            return gameProcess;
        }
    }
}
