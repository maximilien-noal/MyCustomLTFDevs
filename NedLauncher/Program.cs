// Decompiled with JetBrains decompiler
// Type: NedLauncher.Program
// Assembly: NedLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE42E3DB-325E-499C-81CC-B0B4034A19CF
// Assembly location: D:\Téléchargements\setups\EnCours\NightmareNed\NedLauncher.exe

using System;
using System.Windows.Forms;

namespace NedLauncher
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new MainForm());
    }
  }
}
