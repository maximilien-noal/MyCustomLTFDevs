using EasyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Linq;
using System.Windows.Forms;

namespace OddityHookLib
{
    public class OdtHoook : IEntryPoint
    {
        private static DateTime _lastMovieTime;
        private static string _workDir = Environment.CurrentDirectory;
        IpcInterface _ipcInterface;
        private static MP3Player _songPlayer = new MP3Player();
        
        LocalHook _createFileALocalHook;

        public static IntPtr CreateFileAHookMethod(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile)
        {
            PlayMusic(filename);

            return CreateFileA(
                filename,
                access,
                share,
                securityAttributes,
                creationDisposition,
                flagsAndAttributes,
                templateFile);
        }

        private static void PlayMusic(string filename)
        {
            var demandTime = DateTime.Now;
            if (filename != null && filename.ToLower().Contains("movies"))
            {
                _lastMovieTime = demandTime;
                _songPlayer.CloseMediaFile();
            }

            var timeSpan = demandTime.Subtract(_lastMovieTime).Duration().TotalMilliseconds;

            if (_lastMovieTime != DateTime.MinValue && timeSpan <= 100)
            {
                _songPlayer.CloseMediaFile();
                return;
            }

            if (filename != null && filename.ToLower().Contains("menu"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\09.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level00"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\01.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level01"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\02.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level02"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\03.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level03"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\04.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level04"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\05.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level05"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\06.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level06"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\07.mp3"));
            }
            if (filename != null && filename.ToLower().Contains("level.txt") && filename.ToLower().Contains("level07"))
            {
                _songPlayer.Play(Path.Combine(_workDir, @"Music\08.mp3"));
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi)]
        public delegate IntPtr CreateFileADelegate(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateFileA(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile);


        public OdtHoook(RemoteHooking.IContext inContext, String inChannelName)
        {
            // connect to host...
            _ipcInterface = RemoteHooking.IpcConnectClient<IpcInterface>(inChannelName);
            _ipcInterface.Ping();
        }

        public void Run(RemoteHooking.IContext inContext, String inChannelName)
        {
            // install hook...
            try
            {
                _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                IntPtr createFileAProcAddress = LocalHook.GetProcAddress("kernel32.dll", "CreateFileA");

                _createFileALocalHook = LocalHook.Create(
                    createFileAProcAddress,
                    new CreateFileADelegate(CreateFileAHookMethod),
                    this);

                _createFileALocalHook.ThreadACL.SetExclusiveACL(new int[] { 0 });
                
            }
            catch (Exception exception)
            {
                _ipcInterface.ReportException(exception);
                return;
            }

            _ipcInterface.NotifySucessfulInstallation(RemoteHooking.GetCurrentProcessId());
            RemoteHooking.WakeUpProcess();

            // wait until we are not needed anymore...
            try
            {
                while (true)
                {
                    _ipcInterface.OnHooking();
                }
            }
            catch
            {
                // Ping() will raise an exception if host is unreachable
            }
        }
    }
}
