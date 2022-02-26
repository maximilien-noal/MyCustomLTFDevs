using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddityHookLib
{
    using System.IO;
    using System.Runtime.InteropServices;

    public class MP3Player
    {
        private string _command;
        private string _playedFile;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        public MP3Player()
        {
            System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            CloseMediaFile();
        }

        public void CloseMediaFile()
        {
            if(String.IsNullOrEmpty(_playedFile) || File.Exists(_playedFile) == false)
            {
                return;
            }
            _command = "close MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
            _playedFile = "";
        }

        private void OpenMediaFile()
        {
            if (String.IsNullOrEmpty(_playedFile) || File.Exists(_playedFile) == false)
            {
                return;
            }

            _command = "open \"" + _playedFile + "\" type mpegvideo alias MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }

        public void Play(string filename)
        {
            if (String.IsNullOrEmpty(filename) || File.Exists(filename) == false)
            {
                return;
            }

            if(_playedFile == filename)
            {
                return;
            }
            if(String.IsNullOrEmpty(_playedFile) == false)
            {
                CloseMediaFile();
            }
            _playedFile = filename;
            OpenMediaFile();
            _command = "play MediaFile";
            _command += " REPEAT";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }
    }
}
