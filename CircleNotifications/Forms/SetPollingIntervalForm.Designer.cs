namespace CircleNotifications.Forms {
    partial class SetPollingIntervalForm {
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
            this.SetPollingIntervalButton = new System.Windows.Forms.Button();
            this.SetPollingIntervalTextBox = new System.Windows.Forms.TextBox();
            this.SetPollingIntervalLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SetPollingIntervalButton
            // 
            this.SetPollingIntervalButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SetPollingIntervalButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetPollingIntervalButton.Location = new System.Drawing.Point(103, 103);
            this.SetPollingIntervalButton.Name = "SetPollingIntervalButton";
            this.SetPollingIntervalButton.Size = new System.Drawing.Size(75, 23);
            this.SetPollingIntervalButton.TabIndex = 5;
            this.SetPollingIntervalButton.Text = "Submit";
            this.SetPollingIntervalButton.UseVisualStyleBackColor = false;
            this.SetPollingIntervalButton.Click += new System.EventHandler(this.SetPollingIntervalButton_Click);
            // 
            // SetPollingIntervalTextBox
            // 
            this.SetPollingIntervalTextBox.Location = new System.Drawing.Point(92, 68);
            this.SetPollingIntervalTextBox.Name = "SetPollingIntervalTextBox";
            this.SetPollingIntervalTextBox.Size = new System.Drawing.Size(96, 20);
            this.SetPollingIntervalTextBox.TabIndex = 4;
            // 
            // SetPollingIntervalLabel
            // 
            this.SetPollingIntervalLabel.AutoSize = true;
            this.SetPollingIntervalLabel.CausesValidation = false;
            this.SetPollingIntervalLabel.Enabled = false;
            this.SetPollingIntervalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetPollingIntervalLabel.Location = new System.Drawing.Point(45, 30);
            this.SetPollingIntervalLabel.Name = "SetPollingIntervalLabel";
            this.SetPollingIntervalLabel.Size = new System.Drawing.Size(191, 20);
            this.SetPollingIntervalLabel.TabIndex = 3;
            this.SetPollingIntervalLabel.Text = "Set Polling Interval (in ms)";
            this.SetPollingIntervalLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SetPollingIntervalForm
            // 
            this.AcceptButton = this.SetPollingIntervalButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(286, 147);
            this.Controls.Add(this.SetPollingIntervalButton);
            this.Controls.Add(this.SetPollingIntervalTextBox);
            this.Controls.Add(this.SetPollingIntervalLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SetPollingIntervalForm";
            this.ShowInTaskbar = false;
            this.Text = "Set Polling Interval";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetPollingIntervalButton;
        private System.Windows.Forms.TextBox SetPollingIntervalTextBox;
        protected System.Windows.Forms.Label SetPollingIntervalLabel;
    }
}