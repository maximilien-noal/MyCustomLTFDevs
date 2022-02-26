namespace MiniGolfDeluxeLauncher
{
    partial class RootForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RootForm));
            this.JouerButton = new System.Windows.Forms.Button();
            this.radioButtonMP3 = new System.Windows.Forms.RadioButton();
            this.radioButtonMIDI = new System.Windows.Forms.RadioButton();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // JouerButton
            // 
            this.JouerButton.Location = new System.Drawing.Point(55, 12);
            this.JouerButton.Name = "JouerButton";
            this.JouerButton.Size = new System.Drawing.Size(75, 23);
            this.JouerButton.TabIndex = 0;
            this.JouerButton.Text = "Jouer";
            this.JouerButton.UseVisualStyleBackColor = true;
            this.JouerButton.Click += new System.EventHandler(this.JouerButton_Click);
            // 
            // radioButtonMP3
            // 
            this.radioButtonMP3.AutoSize = true;
            this.radioButtonMP3.Location = new System.Drawing.Point(6, 23);
            this.radioButtonMP3.Name = "radioButtonMP3";
            this.radioButtonMP3.Size = new System.Drawing.Size(95, 17);
            this.radioButtonMP3.TabIndex = 1;
            this.radioButtonMP3.TabStop = true;
            this.radioButtonMP3.Text = "Musiques MP3";
            this.radioButtonMP3.UseVisualStyleBackColor = true;
            // 
            // radioButtonMIDI
            // 
            this.radioButtonMIDI.AutoSize = true;
            this.radioButtonMIDI.Location = new System.Drawing.Point(6, 57);
            this.radioButtonMIDI.Name = "radioButtonMIDI";
            this.radioButtonMIDI.Size = new System.Drawing.Size(96, 17);
            this.radioButtonMIDI.TabIndex = 2;
            this.radioButtonMIDI.TabStop = true;
            this.radioButtonMIDI.Text = "Musiques MIDI";
            this.radioButtonMIDI.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.radioButtonMP3);
            this.groupBoxOptions.Controls.Add(this.radioButtonMIDI);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 49);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(160, 100);
            this.groupBoxOptions.TabIndex = 3;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Préférences";
            // 
            // RootForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 161);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.JouerButton);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(200, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "RootForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3-D MiniGolf Deluxe";
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button JouerButton;
        private System.Windows.Forms.RadioButton radioButtonMP3;
        private System.Windows.Forms.RadioButton radioButtonMIDI;
        private System.Windows.Forms.GroupBox groupBoxOptions;
    }
}

