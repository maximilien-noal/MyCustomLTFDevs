// Decompiled with JetBrains decompiler
// Type: NedLauncher.Properties.Settings
// Assembly: NedLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE42E3DB-325E-499C-81CC-B0B4034A19CF
// Assembly location: D:\Téléchargements\setups\EnCours\NightmareNed\NedLauncher.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NedLauncher.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.3.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool IsDgVoodooEnabled
    {
      get => (bool) this[nameof (IsDgVoodooEnabled)];
      set => this[nameof (IsDgVoodooEnabled)] = (object) value;
    }
  }
}
