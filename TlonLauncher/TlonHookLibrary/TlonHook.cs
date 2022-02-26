using EasyHook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace TlonHookLibrary
{
    public enum DriveType : uint
    {
                        /// <summary>The drive type cannot be determined.</summary>
        Unknown = 0,    //DRIVE_UNKNOWN
                        /// <summary>The root path is invalid, for example, no volume is mounted at the path.</summary>
        Error = 1,      //DRIVE_NO_ROOT_DIR
                        /// <summary>The drive is a type that has removable media, for example, a floppy drive or removable hard disk.</summary>
        Removable = 2,  //DRIVE_REMOVABLE
                        /// <summary>The drive is a type that cannot be removed, for example, a fixed hard drive.</summary>
        Fixed = 3,      //DRIVE_FIXED
                        /// <summary>The drive is a remote (network) drive.</summary>
        Remote = 4,     //DRIVE_REMOTE
                        /// <summary>The drive is a CD-ROM drive.</summary>
        CDROM = 5,      //DRIVE_CDROM
                        /// <summary>The drive is a RAM disk.</summary>
        RAMDisk = 6     //DRIVE_RAMDISK
    }

    public class TlonHook : IEntryPoint
    {
        private static string _runningDirectory = Assembly.GetExecutingAssembly().Location;
        IpcInterface _ipcInterface;
        private static bool _alreadyStarted = false;
        #region GetDriveTypeA
        LocalHook _getDriveTypeLocalHook;

        /// <summary>
        /// The GetDriveType function determines whether a disk drive is a removable, fixed, CD-ROM, RAM disk, or network drive
        /// </summary>
        /// <param name="lpRootPathName">A pointer to a null-terminated string that specifies the root directory and returns information about the disk.A trailing backslash is required. If this parameter is NULL, the function uses the root of the current directory.</param>
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern DriveType GetDriveType(
            [MarshalAs(UnmanagedType.LPTStr)] string lpRootPathName);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi)]
        public delegate DriveType GetDriveTypeDelegate(
            [MarshalAs(UnmanagedType.LPTStr)] string lpRootPathName);

        private static DriveType GetDriveTypeHookMethod(
            [MarshalAs(UnmanagedType.LPTStr)] string lpRootPathName)
        {
            return DriveType.CDROM;
        }

        #endregion GetDriveTypeA

        #region CreateFile
        LocalHook _createFileLocalHook;

        public static IntPtr CreateFileHookMethod(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile)
        {
            return CreateFile(
                CorrectFilePath(filename),
                access,
                share,
                securityAttributes,
                creationDisposition,
                flagsAndAttributes,
                templateFile);
        }

        /// <summary>
        /// D:\data\data00.grp devient {app}\Data\data00.grp
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string CorrectFilePath(string filename)
        {
            if(filename.Contains("SAVEGAME") || filename.EndsWith(".sav"))
            {
                return filename;
            }
            if (File.Exists(filename))
            {
                return filename;
            }
            if(Path.IsPathRooted(filename) == false)
            {
                return filename;
            }

            string correctedString = Path.Combine(_runningDirectory, filename.Substring(Path.GetPathRoot(filename).Length));
            return correctedString;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi)]
        public delegate IntPtr CreateFileDelegate(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateFile(
            [MarshalAs(UnmanagedType.LPStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile);

        #endregion CreateFile

        public TlonHook(RemoteHooking.IContext inContext, String inChannelName)
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
                _runningDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                IntPtr createFileProcAddress = LocalHook.GetProcAddress("kernel32.dll", "CreateFileA");

                _createFileLocalHook = LocalHook.Create(
                    createFileProcAddress,
                    new CreateFileDelegate(CreateFileHookMethod),
                    this);

                _createFileLocalHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                IntPtr getDriveTypeProcAddress = LocalHook.GetProcAddress("kernel32.dll", "GetDriveTypeA");

                _getDriveTypeLocalHook = LocalHook.Create(
                    getDriveTypeProcAddress,
                    new GetDriveTypeDelegate(GetDriveTypeHookMethod),
                    this);

                _getDriveTypeLocalHook.ThreadACL.SetExclusiveACL(new int[] { 0 });
            }
            catch (Exception exception)
            {
                _ipcInterface.ReportException(exception);

                return;
            }

            _ipcInterface.NotifySucessfulInstallation(RemoteHooking.GetCurrentProcessId());
            RemoteHooking.WakeUpProcess();

            // wait for host process termination...
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
