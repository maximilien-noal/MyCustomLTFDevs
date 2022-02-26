namespace Dune2000Launcher
{
    partial class LauncherWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherWindow));
            this.JouerButton = new System.Windows.Forms.Button();
            this.DesactiverDgVoodoo2CheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // JouerButton
            // 
            this.JouerButton.Location = new System.Drawing.Point(115, 12);
            this.JouerButton.Name = "JouerButton";
            this.JouerButton.Size = new System.Drawing.Size(75, 23);
            this.JouerButton.TabIndex = 0;
            this.JouerButton.Text = "Jouer";
            this.JouerButton.UseVisualStyleBackColor = true;
            this.JouerButton.Click += new System.EventHandler(this.JouerButton_Click);
            // 
            // DesactiverDgVoodoo2CheckBox
            // 
            this.DesactiverDgVoodoo2CheckBox.AutoSize = true;
            this.DesactiverDgVoodoo2CheckBox.Location = new System.Drawing.Point(80, 50);
            this.DesactiverDgVoodoo2CheckBox.Name = "DesactiverDgVoodoo2CheckBox";
            this.DesactiverDgVoodoo2CheckBox.Size = new System.Drawing.Size(135, 17);
            this.DesactiverDgVoodoo2CheckBox.TabIndex = 1;
            this.DesactiverDgVoodoo2CheckBox.Text = "Désactiver dgVoodoo2";
            this.DesactiverDgVoodoo2CheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = " (utile si le jeu ne fonctionne pas)";
            // 
            // LauncherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DesactiverDgVoodoo2CheckBox);
            this.Controls.Add(this.JouerButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LauncherWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lanceur Dune 2000";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button JouerButton;
        private System.Windows.Forms.CheckBox DesactiverDgVoodoo2CheckBox;
        private System.Windows.Forms.Label label1;
    }
}

