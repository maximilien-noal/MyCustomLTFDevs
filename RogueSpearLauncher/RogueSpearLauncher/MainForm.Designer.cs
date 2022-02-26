namespace RogueSpearLauncher
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
            this.DeactivateDdrawCompatCheckbox = new System.Windows.Forms.CheckBox();
            this.DeactivateDdrawCompatLabel = new System.Windows.Forms.Label();
            this.EditorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JouerButton
            // 
            this.JouerButton.Location = new System.Drawing.Point(97, 12);
            this.JouerButton.Name = "JouerButton";
            this.JouerButton.Size = new System.Drawing.Size(75, 23);
            this.JouerButton.TabIndex = 0;
            this.JouerButton.Text = "Jouer";
            this.JouerButton.UseVisualStyleBackColor = true;
            this.JouerButton.Click += new System.EventHandler(this.JouerButton_Click);
            // 
            // DeactivateDdrawCompatCheckbox
            // 
            this.DeactivateDdrawCompatCheckbox.AutoSize = true;
            this.DeactivateDdrawCompatCheckbox.Location = new System.Drawing.Point(65, 81);
            this.DeactivateDdrawCompatCheckbox.Name = "DeactivateDdrawCompatCheckbox";
            this.DeactivateDdrawCompatCheckbox.Size = new System.Drawing.Size(145, 17);
            this.DeactivateDdrawCompatCheckbox.TabIndex = 2;
            this.DeactivateDdrawCompatCheckbox.Text = "Désactiver ddrawCompat";
            this.DeactivateDdrawCompatCheckbox.UseVisualStyleBackColor = true;
            this.DeactivateDdrawCompatCheckbox.CheckedChanged += new System.EventHandler(this.DeactivateDdrawCompatCheckbox_CheckedChanged);
            // 
            // DeactivateDdrawCompatLabel
            // 
            this.DeactivateDdrawCompatLabel.AutoSize = true;
            this.DeactivateDdrawCompatLabel.Location = new System.Drawing.Point(12, 101);
            this.DeactivateDdrawCompatLabel.Name = "DeactivateDdrawCompatLabel";
            this.DeactivateDdrawCompatLabel.Size = new System.Drawing.Size(248, 13);
            this.DeactivateDdrawCompatLabel.TabIndex = 3;
            this.DeactivateDdrawCompatLabel.Text = "(déconseillé : uniquement utile en cas de problème)";
            // 
            // EditorButton
            // 
            this.EditorButton.Location = new System.Drawing.Point(80, 41);
            this.EditorButton.Name = "EditorButton";
            this.EditorButton.Size = new System.Drawing.Size(113, 23);
            this.EditorButton.TabIndex = 4;
            this.EditorButton.Text = "Éditeur de missions";
            this.EditorButton.UseVisualStyleBackColor = true;
            this.EditorButton.Click += new System.EventHandler(this.EditorButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.EditorButton);
            this.Controls.Add(this.DeactivateDdrawCompatLabel);
            this.Controls.Add(this.DeactivateDdrawCompatCheckbox);
            this.Controls.Add(this.JouerButton);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lanceur Rogue Spear";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button JouerButton;
        private System.Windows.Forms.CheckBox DeactivateDdrawCompatCheckbox;
        private System.Windows.Forms.Label DeactivateDdrawCompatLabel;
        private System.Windows.Forms.Button EditorButton;
    }
}

