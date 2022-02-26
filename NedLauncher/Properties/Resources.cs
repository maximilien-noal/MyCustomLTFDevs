// Decompiled with JetBrains decompiler
// Type: NedLauncher.Properties.Resources
// Assembly: NedLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE42E3DB-325E-499C-81CC-B0B4034A19CF
// Assembly location: D:\Téléchargements\setups\EnCours\NightmareNed\NedLauncher.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace NedLauncher.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (NedLauncher.Properties.Resources.resourceMan == null)
          NedLauncher.Properties.Resources.resourceMan = new ResourceManager("NedLauncher.Properties.Resources", typeof (NedLauncher.Properties.Resources).Assembly);
        return NedLauncher.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => NedLauncher.Properties.Resources.resourceCulture;
      set => NedLauncher.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap launcher => (Bitmap) NedLauncher.Properties.Resources.ResourceManager.GetObject(nameof (launcher), NedLauncher.Properties.Resources.resourceCulture);
  }
}
