// Decompiled with JetBrains decompiler
// Type: NedLauncher.MainForm
// Assembly: NedLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE42E3DB-325E-499C-81CC-B0B4034A19CF
// Assembly location: D:\Téléchargements\setups\EnCours\NightmareNed\NedLauncher.exe

using NedLauncher.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NedLauncher
{
  public class MainForm : Form
  {
    private string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private IContainer components;
    private Button PlayButton;
    private CheckBox DgVoodooCheckBox;

    public MainForm()
    {
      this.InitializeComponent();
      Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
      this.DgVoodooCheckBox.Checked = Settings.Default.IsDgVoodooEnabled;
    }

    private void OnApplicationExit(object sender, EventArgs e)
    {
      Settings.Default.IsDgVoodooEnabled = this.DgVoodooCheckBox.Checked;
      Settings.Default.Save();
    }

    private void PlayButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (File.Exists(Path.Combine(this._workDir, "dgVoodoo.conf")))
          File.Delete(Path.Combine(this._workDir, "dgVoodoo.conf"));
        if (this.DgVoodooCheckBox.Checked)
          this.CopyResource("NedLauncher.dgVoodooEnabled.conf", Path.Combine(this._workDir, "dgVoodoo.conf"));
        else
          this.CopyResource("NedLauncher.dgVoodooDisabled.conf", Path.Combine(this._workDir, "dgVoodoo.conf"));
      }
      catch
      {
      }
      try
      {
        ProcessStartInfo processStartInfo = new ProcessStartInfo(Path.Combine(this._workDir, "NITENED.EXE"));
        if (new Process() { StartInfo = processStartInfo }.Start())
          Application.Exit();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(string.Format("{0} : {1}{2}", (object) ex.Message, (object) Environment.NewLine, (object) ex.StackTrace));
      }
      Application.Exit();
    }

    private void CopyResource(string resourceName, string outputFile)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
      {
        if (manifestResourceStream == null)
          return;
        using (Stream destination = (Stream) File.OpenWrite(outputFile))
          manifestResourceStream.CopyTo(destination);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
      this.PlayButton = new Button();
      this.DgVoodooCheckBox = new CheckBox();
      this.SuspendLayout();
      this.PlayButton.Location = new Point(185, 12);
      this.PlayButton.Name = "PlayButton";
      this.PlayButton.Size = new Size(75, 23);
      this.PlayButton.TabIndex = 0;
      this.PlayButton.Text = "Jouer";
      this.PlayButton.UseVisualStyleBackColor = true;
      this.PlayButton.Click += new EventHandler(this.PlayButton_Click);
      this.DgVoodooCheckBox.AutoSize = true;
      this.DgVoodooCheckBox.BackColor = Color.Transparent;
      this.DgVoodooCheckBox.ForeColor = SystemColors.ButtonHighlight;
      this.DgVoodooCheckBox.Location = new Point(112, 41);
      this.DgVoodooCheckBox.Name = "DgVoodooCheckBox";
      this.DgVoodooCheckBox.Size = new Size(240, 17);
      this.DgVoodooCheckBox.TabIndex = 1;
      this.DgVoodooCheckBox.Text = "Activer dgvoodoo2 (utile en cas de problème)";
      this.DgVoodooCheckBox.UseVisualStyleBackColor = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImage = (Image) Resources.launcher;
      this.BackgroundImageLayout = ImageLayout.Center;
      this.ClientSize = new Size(441, 271);
      this.Controls.Add((Control) this.DgVoodooCheckBox);
      this.Controls.Add((Control) this.PlayButton);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (MainForm);
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Lanceur Nightmare Ned";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
