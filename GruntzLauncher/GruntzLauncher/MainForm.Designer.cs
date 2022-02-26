namespace GruntzLauncher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.JouerButton = new System.Windows.Forms.Button();
            this.EditeurButton = new System.Windows.Forms.Button();
            this.HelpEditorButton = new System.Windows.Forms.Button();
            this.HelpGameButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DeactivateDgVoodoo2Label = new System.Windows.Forms.Label();
            this.DeactivateDgVoodoo2CheckBox = new System.Windows.Forms.CheckBox();
            this.DgVoodoo2OptionsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // JouerButton
            // 
            this.JouerButton.Location = new System.Drawing.Point(114, 10);
            this.JouerButton.Name = "JouerButton";
            this.JouerButton.Size = new System.Drawing.Size(75, 23);
            this.JouerButton.TabIndex = 0;
            this.JouerButton.Text = "Jouer";
            this.JouerButton.UseVisualStyleBackColor = true;
            this.JouerButton.Click += new System.EventHandler(this.JouerButton_Click);
            // 
            // EditeurButton
            // 
            this.EditeurButton.Location = new System.Drawing.Point(114, 60);
            this.EditeurButton.Name = "EditeurButton";
            this.EditeurButton.Size = new System.Drawing.Size(75, 23);
            this.EditeurButton.TabIndex = 1;
            this.EditeurButton.Text = "Éditeur";
            this.EditeurButton.UseVisualStyleBackColor = true;
            this.EditeurButton.Click += new System.EventHandler(this.EditeurButton_Click);
            // 
            // HelpEditorButton
            // 
            this.HelpEditorButton.Location = new System.Drawing.Point(114, 160);
            this.HelpEditorButton.Name = "HelpEditorButton";
            this.HelpEditorButton.Size = new System.Drawing.Size(75, 23);
            this.HelpEditorButton.TabIndex = 2;
            this.HelpEditorButton.Text = "Aide éditeur";
            this.HelpEditorButton.UseVisualStyleBackColor = true;
            this.HelpEditorButton.Click += new System.EventHandler(this.HelpEditorButton_Click);
            // 
            // HelpGameButton
            // 
            this.HelpGameButton.Location = new System.Drawing.Point(114, 110);
            this.HelpGameButton.Name = "HelpGameButton";
            this.HelpGameButton.Size = new System.Drawing.Size(75, 23);
            this.HelpGameButton.TabIndex = 3;
            this.HelpGameButton.Text = "Aide jeu";
            this.HelpGameButton.UseVisualStyleBackColor = true;
            this.HelpGameButton.Click += new System.EventHandler(this.HelpGameButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgVoodoo2OptionsButton);
            this.groupBox1.Controls.Add(this.DeactivateDgVoodoo2Label);
            this.groupBox1.Controls.Add(this.DeactivateDgVoodoo2CheckBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options avancées...";
            // 
            // DeactivateDgVoodoo2Label
            // 
            this.DeactivateDgVoodoo2Label.AutoSize = true;
            this.DeactivateDgVoodoo2Label.Location = new System.Drawing.Point(6, 55);
            this.DeactivateDgVoodoo2Label.Name = "DeactivateDgVoodoo2Label";
            this.DeactivateDgVoodoo2Label.Size = new System.Drawing.Size(128, 13);
            this.DeactivateDgVoodoo2Label.TabIndex = 7;
            this.DeactivateDgVoodoo2Label.Text = "(utile en cas de problème)";
            // 
            // DeactivateDgVoodoo2CheckBox
            // 
            this.DeactivateDgVoodoo2CheckBox.AutoSize = true;
            this.DeactivateDgVoodoo2CheckBox.Location = new System.Drawing.Point(6, 34);
            this.DeactivateDgVoodoo2CheckBox.Name = "DeactivateDgVoodoo2CheckBox";
            this.DeactivateDgVoodoo2CheckBox.Size = new System.Drawing.Size(135, 17);
            this.DeactivateDgVoodoo2CheckBox.TabIndex = 6;
            this.DeactivateDgVoodoo2CheckBox.Text = "Désactiver dgVoodoo2";
            this.DeactivateDgVoodoo2CheckBox.UseVisualStyleBackColor = true;
            // 
            // DgVoodoo2OptionsButton
            // 
            this.DgVoodoo2OptionsButton.Location = new System.Drawing.Point(147, 19);
            this.DgVoodoo2OptionsButton.Name = "DgVoodoo2OptionsButton";
            this.DgVoodoo2OptionsButton.Size = new System.Drawing.Size(107, 41);
            this.DgVoodoo2OptionsButton.TabIndex = 8;
            this.DgVoodoo2OptionsButton.Text = "Options dgVoodoo2...";
            this.DgVoodoo2OptionsButton.UseVisualStyleBackColor = true;
            this.DgVoodoo2OptionsButton.Click += new System.EventHandler(this.DgVoodoo2OptionsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.HelpGameButton);
            this.Controls.Add(this.HelpEditorButton);
            this.Controls.Add(this.EditeurButton);
            this.Controls.Add(this.JouerButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lanceur Gruntz";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button JouerButton;
        private System.Windows.Forms.Button EditeurButton;
        private System.Windows.Forms.Button HelpEditorButton;
        private System.Windows.Forms.Button HelpGameButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DgVoodoo2OptionsButton;
        private System.Windows.Forms.Label DeactivateDgVoodoo2Label;
        private System.Windows.Forms.CheckBox DeactivateDgVoodoo2CheckBox;
    }
}

