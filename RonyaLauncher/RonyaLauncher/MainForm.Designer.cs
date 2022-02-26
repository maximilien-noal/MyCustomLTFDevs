namespace RonyaLauncher
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
            this.pictureBoxRonya = new System.Windows.Forms.PictureBox();
            this.DeactivateDdrawCompatCheckBox = new System.Windows.Forms.CheckBox();
            this.ddrawCompatLabel = new System.Windows.Forms.Label();
            this.JouerButton = new System.Windows.Forms.Button();
            this.LisezMoiButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRonya)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRonya
            // 
            this.pictureBoxRonya.Image = global::RonyaLauncher.Properties.Resources.ronya;
            this.pictureBoxRonya.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxRonya.Name = "pictureBoxRonya";
            this.pictureBoxRonya.Size = new System.Drawing.Size(256, 172);
            this.pictureBoxRonya.TabIndex = 0;
            this.pictureBoxRonya.TabStop = false;
            // 
            // DeactivateDdrawCompatCheckBox
            // 
            this.DeactivateDdrawCompatCheckBox.AutoSize = true;
            this.DeactivateDdrawCompatCheckBox.Location = new System.Drawing.Point(66, 190);
            this.DeactivateDdrawCompatCheckBox.Name = "DeactivateDdrawCompatCheckBox";
            this.DeactivateDdrawCompatCheckBox.Size = new System.Drawing.Size(145, 17);
            this.DeactivateDdrawCompatCheckBox.TabIndex = 1;
            this.DeactivateDdrawCompatCheckBox.Text = "Désactiver ddrawCompat";
            this.DeactivateDdrawCompatCheckBox.UseVisualStyleBackColor = true;
            // 
            // ddrawCompatLabel
            // 
            this.ddrawCompatLabel.AutoSize = true;
            this.ddrawCompatLabel.Location = new System.Drawing.Point(12, 210);
            this.ddrawCompatLabel.Name = "ddrawCompatLabel";
            this.ddrawCompatLabel.Size = new System.Drawing.Size(248, 13);
            this.ddrawCompatLabel.TabIndex = 2;
            this.ddrawCompatLabel.Text = "(déconseillé : uniquement utile en cas de problème)";
            // 
            // JouerButton
            // 
            this.JouerButton.Location = new System.Drawing.Point(100, 235);
            this.JouerButton.Name = "JouerButton";
            this.JouerButton.Size = new System.Drawing.Size(75, 23);
            this.JouerButton.TabIndex = 3;
            this.JouerButton.Text = "Jouer";
            this.JouerButton.UseVisualStyleBackColor = true;
            this.JouerButton.Click += new System.EventHandler(this.JouerButton_Click);
            // 
            // LisezMoiButton
            // 
            this.LisezMoiButton.Location = new System.Drawing.Point(100, 266);
            this.LisezMoiButton.Name = "LisezMoiButton";
            this.LisezMoiButton.Size = new System.Drawing.Size(75, 23);
            this.LisezMoiButton.TabIndex = 4;
            this.LisezMoiButton.Text = "Lisez-Moi";
            this.LisezMoiButton.UseVisualStyleBackColor = true;
            this.LisezMoiButton.Click += new System.EventHandler(this.LisezMoiButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 301);
            this.Controls.Add(this.LisezMoiButton);
            this.Controls.Add(this.JouerButton);
            this.Controls.Add(this.ddrawCompatLabel);
            this.Controls.Add(this.DeactivateDdrawCompatCheckBox);
            this.Controls.Add(this.pictureBoxRonya);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lanceur Ronya";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRonya)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRonya;
        private System.Windows.Forms.CheckBox DeactivateDdrawCompatCheckBox;
        private System.Windows.Forms.Label ddrawCompatLabel;
        private System.Windows.Forms.Button JouerButton;
        private System.Windows.Forms.Button LisezMoiButton;
    }
}

