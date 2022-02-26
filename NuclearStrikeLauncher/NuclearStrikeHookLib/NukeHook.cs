using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Linq;
using EasyHook;
using System.Windows.Forms;

namespace NuclearStrikeHookLib
{
    public enum DriveType : uint
    {
        /// <summary>The drive type cannot be determined.</summary>
        Unknown = 0,    //DRIVE_UNKNOWN
                        /// <summary>The root path is invalid, for example, no volume is mounted at the path.</summary>
        Error = 1,        //DRIVE_NO_ROOT_DIR
                          /// <summary>The drive is a type that has removable media, for example, a floppy drive or removable hard disk.</summary>
        Removable = 2,    //DRIVE_REMOVABLE
                          /// <summary>The drive is a type that cannot be removed, for example, a fixed hard drive.</summary>
        Fixed = 3,        //DRIVE_FIXED
                          /// <summary>The drive is a remote (network) drive.</summary>
        Remote = 4,        //DRIVE_REMOTE
                           /// <summary>The drive is a CD-ROM drive.</summary>
        CDROM = 5,        //DRIVE_CDROM
                          /// <summary>The drive is a RAM disk.</summary>
        RAMDisk = 6        //DRIVE_RAMDISK
    }

    public class NukeHook : IEntryPoint
    {
        private static string _runningDirectory = Environment.CurrentDirectory;
        IpcInterface _ipcInterface;

        #region GetDriveTypeA
        LocalHook _getDriveTypeLocalHook;
        static bool _fakedDriveTypeAtStartup = false;

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

        #region mmioOpenW
        LocalHook _mmioOpenLocalHook;

        private static IntPtr MmioOpenHookMethod(
          [MarshalAs(UnmanagedType.LPWStr)]string szFilename,
          IntPtr lpmmioinfo,
          int dwOpenFlags)
        {
            return mmioOpenW(CorrectFilePath(szFilename), lpmmioinfo, dwOpenFlags);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        public delegate IntPtr MmioOpenDelegate(
          [MarshalAs(UnmanagedType.LPWStr)]string szFilename,
          IntPtr lpmmioinfo,
          int dwOpenFlags);

        [DllImport("WINMM.DLL", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr mmioOpenW(
           [MarshalAs(UnmanagedType.LPWStr)]string szFilename,
           IntPtr lpmmioinfo,
           int dwOpenFlags);

        #endregion mmioOpenW

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
            filename = CorrectFilePath(filename);

            return CreateFile(
                filename,
                access,
                share,
                securityAttributes,
                creationDisposition,
                flagsAndAttributes,
                templateFile);
        }

        /// <summary>
        /// E:\horr\FRA\movie\CAPCOM.AVI devient C:\Jeux\RESIDENT EVIL\FRA\MOVIE\CAPCOM.AVI
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string CorrectFilePath(string filename)
        {
            if (File.Exists(filename))
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

        #region GetVolumeInformation

        LocalHook _getVolumeInformationLocalHook;

        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool GetVolumeInformation(
            [MarshalAs(UnmanagedType.LPTStr)] string rootPathName,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder volumeNameBuffer,
            int volumeNameSize,
            [Optional] out uint volumeSerialNumber,
            [Optional] out uint maximumComponentLength,
            [Optional] out FileSystemFeature fileSystemFlags,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder fileSystemNameBuffer,
            int nFileSystemNameSize);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi)]
        public delegate bool GetVolumeInformationDelegate(
            [MarshalAs(UnmanagedType.LPTStr)] string rootPathName,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder volumeNameBuffer,
            int volumeNameSize,
            [Optional]out uint volumeSerialNumber,
            [Optional]out uint maximumComponentLength,
            [Optional]out FileSystemFeature fileSystemFlags,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder fileSystemNameBuffer,
            int nFileSystemNameSize);

        private static bool GetVolumeInformationHookMethod(
            [MarshalAs(UnmanagedType.LPTStr)] string rootPathName,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder volumeNameBuffer,
            int volumeNameSize,
            [Optional]out uint volumeSerialNumber,
            [Optional]out uint maximumComponentLength,
            [Optional]out FileSystemFeature fileSystemFlags,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder fileSystemNameBuffer,
            int nFileSystemNameSize)
        {
            if (Path.GetPathRoot(rootPathName) != Path.GetPathRoot(Assembly.GetExecutingAssembly().Location))
            {
                return GetVolumeInformation(
                    Path.GetPathRoot(Assembly.GetExecutingAssembly().Location),
                    volumeNameBuffer,
                    volumeNameSize,
                    out volumeSerialNumber,
                    out maximumComponentLength,
                    out fileSystemFlags,
                    fileSystemNameBuffer,
                    nFileSystemNameSize);
            }
            return GetVolumeInformation(
                rootPathName,
                volumeNameBuffer,
                volumeNameSize,
                out volumeSerialNumber,
                out maximumComponentLength,
                out fileSystemFlags,
                fileSystemNameBuffer,
                nFileSystemNameSize);
        }

        #endregion

        public NukeHook(RemoteHooking.IContext inContext, String inChannelName)
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

                IntPtr mmioOpenProcAddress = LocalHook.GetProcAddress("WINMM.dll", "mmioOpenW");

                _mmioOpenLocalHook = LocalHook.Create(
                    mmioOpenProcAddress,
                    new MmioOpenDelegate(MmioOpenHookMethod),
                    this);

                _mmioOpenLocalHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                IntPtr getDriveTypeProcAddress = LocalHook.GetProcAddress("kernel32.dll", "GetDriveTypeA");

                _getDriveTypeLocalHook = LocalHook.Create(
                    getDriveTypeProcAddress,
                    new GetDriveTypeDelegate(GetDriveTypeHookMethod),
                    this);

                _getDriveTypeLocalHook.ThreadACL.SetExclusiveACL(new int[] { 0 });

                /*
                IntPtr getVolumeeInformationProcAddress = LocalHook.GetProcAddress("kernel32.dll", "GetVolumeInformationA");

                _getVolumeInformationLocalHook = LocalHook.Create(
                    getVolumeeInformationProcAddress,
                    new GetVolumeInformationDelegate(GetVolumeInformationHookMethod),
                    this);

                _getVolumeInformationLocalHook.ThreadACL.SetExclusiveACL(new int[] { 0 });*
                */
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
