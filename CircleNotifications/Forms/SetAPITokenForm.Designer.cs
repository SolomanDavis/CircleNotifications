namespace CircleNotifications.Forms {
    partial class SetAPITokenForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SetAPITokenLabel = new System.Windows.Forms.Label();
            this.SetAPITokenTextBox = new System.Windows.Forms.TextBox();
            this.SetAPIToken_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SetAPITokenLabel
            // 
            this.SetAPITokenLabel.AutoSize = true;
            this.SetAPITokenLabel.CausesValidation = false;
            this.SetAPITokenLabel.Enabled = false;
            this.SetAPITokenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetAPITokenLabel.Location = new System.Drawing.Point(40, 28);
            this.SetAPITokenLabel.Name = "SetAPITokenLabel";
            this.SetAPITokenLabel.Size = new System.Drawing.Size(251, 20);
            this.SetAPITokenLabel.TabIndex = 0;
            this.SetAPITokenLabel.Text = "Set CircleCI User-Level API Token";
            this.SetAPITokenLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SetAPITokenTextBox
            // 
            this.SetAPITokenTextBox.Location = new System.Drawing.Point(13, 63);
            this.SetAPITokenTextBox.Name = "SetAPITokenTextBox";
            this.SetAPITokenTextBox.Size = new System.Drawing.Size(307, 22);
            this.SetAPITokenTextBox.TabIndex = 1;
            // 
            // SetAPIToken_Button
            // 
            this.SetAPIToken_Button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SetAPIToken_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetAPIToken_Button.Location = new System.Drawing.Point(126, 98);
            this.SetAPIToken_Button.Name = "SetAPIToken_Button";
            this.SetAPIToken_Button.Size = new System.Drawing.Size(75, 23);
            this.SetAPIToken_Button.TabIndex = 2;
            this.SetAPIToken_Button.Text = "Submit";
            this.SetAPIToken_Button.UseVisualStyleBackColor = false;
            this.SetAPIToken_Button.Click += new System.EventHandler(this.SetAPITokenButton_Click);
            // 
            // SetAPITokenForm
            // 
            this.AcceptButton = this.SetAPIToken_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(332, 140);
            this.Controls.Add(this.SetAPIToken_Button);
            this.Controls.Add(this.SetAPITokenTextBox);
            this.Controls.Add(this.SetAPITokenLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "SetAPITokenForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Set API Token";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox SetAPITokenTextBox;
        private System.Windows.Forms.Button SetAPIToken_Button;
        protected System.Windows.Forms.Label SetAPITokenLabel;
    }
}