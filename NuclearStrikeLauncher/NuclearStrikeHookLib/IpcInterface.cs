﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuclearStrikeHookLib
{
    public class IpcInterface : MarshalByRefObject
    {
        public void NotifySucessfulInstallation(Int32 inClientPID)
        {
            Console.WriteLine("REHook has been installed in target {0}.\r\n", inClientPID);
        }

        public void OnHooking()
        {

        }

        public void ReportException(Exception inInfo)
        {
            Console.WriteLine("The target process has reported an error:\r\n" + inInfo.ToString());
        }

        public void Ping()
        {
        }
    }
}
