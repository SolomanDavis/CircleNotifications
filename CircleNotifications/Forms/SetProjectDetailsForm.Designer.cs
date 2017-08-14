namespace CircleNotifications.Forms {
    partial class SetProjectDetailsForm {
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
            this.SetProjectDetailsFormButton = new System.Windows.Forms.Button();
            this.UsernameTextbox = new System.Windows.Forms.TextBox();
            this.SetProjectDetailsFormLabel = new System.Windows.Forms.Label();
            this.VCSTypeTextbox = new System.Windows.Forms.TextBox();
            this.ProjectNameTextbox = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.VCSTypeLabel = new System.Windows.Forms.Label();
            this.ProjectNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SetProjectDetailsFormButton
            // 
            this.SetProjectDetailsFormButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SetProjectDetailsFormButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetProjectDetailsFormButton.Location = new System.Drawing.Point(324, 178);
            this.SetProjectDetailsFormButton.Name = "SetProjectDetailsFormButton";
            this.SetProjectDetailsFormButton.Size = new System.Drawing.Size(75, 23);
            this.SetProjectDetailsFormButton.TabIndex = 4;
            this.SetProjectDetailsFormButton.Text = "Submit";
            this.SetProjectDetailsFormButton.UseVisualStyleBackColor = false;
            this.SetProjectDetailsFormButton.Click += new System.EventHandler(this.SetProjectDetailsFormButton_Click);
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Location = new System.Drawing.Point(224, 69);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(175, 20);
            this.UsernameTextbox.TabIndex = 5;
            // 
            // SetProjectDetailsFormLabel
            // 
            this.SetProjectDetailsFormLabel.AutoSize = true;
            this.SetProjectDetailsFormLabel.CausesValidation = false;
            this.SetProjectDetailsFormLabel.Enabled = false;
            this.SetProjectDetailsFormLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetProjectDetailsFormLabel.Location = new System.Drawing.Point(156, 28);
            this.SetProjectDetailsFormLabel.Name = "SetProjectDetailsFormLabel";
            this.SetProjectDetailsFormLabel.Size = new System.Drawing.Size(140, 20);
            this.SetProjectDetailsFormLabel.TabIndex = 3;
            this.SetProjectDetailsFormLabel.Text = "Set Project Details";
            this.SetProjectDetailsFormLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VCSTypeTextbox
            // 
            this.VCSTypeTextbox.Location = new System.Drawing.Point(224, 104);
            this.VCSTypeTextbox.Name = "VCSTypeTextbox";
            this.VCSTypeTextbox.Size = new System.Drawing.Size(175, 20);
            this.VCSTypeTextbox.TabIndex = 6;
            // 
            // ProjectNameTextbox
            // 
            this.ProjectNameTextbox.Location = new System.Drawing.Point(224, 142);
            this.ProjectNameTextbox.Name = "ProjectNameTextbox";
            this.ProjectNameTextbox.Size = new System.Drawing.Size(175, 20);
            this.ProjectNameTextbox.TabIndex = 7;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.CausesValidation = false;
            this.UsernameLabel.Enabled = false;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.Location = new System.Drawing.Point(40, 67);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(140, 20);
            this.UsernameLabel.TabIndex = 8;
            this.UsernameLabel.Text = "Project Username:";
            this.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VCSTypeLabel
            // 
            this.VCSTypeLabel.AutoSize = true;
            this.VCSTypeLabel.CausesValidation = false;
            this.VCSTypeLabel.Enabled = false;
            this.VCSTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VCSTypeLabel.Location = new System.Drawing.Point(7, 104);
            this.VCSTypeLabel.Name = "VCSTypeLabel";
            this.VCSTypeLabel.Size = new System.Drawing.Size(211, 20);
            this.VCSTypeLabel.TabIndex = 9;
            this.VCSTypeLabel.Text = "VCS Type (bitbucket/github):";
            this.VCSTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProjectNameLabel
            // 
            this.ProjectNameLabel.AutoSize = true;
            this.ProjectNameLabel.CausesValidation = false;
            this.ProjectNameLabel.Enabled = false;
            this.ProjectNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectNameLabel.Location = new System.Drawing.Point(53, 142);
            this.ProjectNameLabel.Name = "ProjectNameLabel";
            this.ProjectNameLabel.Size = new System.Drawing.Size(108, 20);
            this.ProjectNameLabel.TabIndex = 10;
            this.ProjectNameLabel.Text = "Project Name:";
            this.ProjectNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SetProjectDetailsForm
            // 
            this.AcceptButton = this.SetProjectDetailsFormButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(434, 219);
            this.Controls.Add(this.ProjectNameLabel);
            this.Controls.Add(this.VCSTypeLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.UsernameTextbox);
            this.Controls.Add(this.VCSTypeTextbox);
            this.Controls.Add(this.ProjectNameTextbox);
            this.Controls.Add(this.SetProjectDetailsFormButton);
            this.Controls.Add(this.SetProjectDetailsFormLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SetProjectDetailsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Project Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetProjectDetailsFormButton;
        private System.Windows.Forms.TextBox UsernameTextbox;
        protected System.Windows.Forms.Label SetProjectDetailsFormLabel;
        private System.Windows.Forms.TextBox VCSTypeTextbox;
        private System.Windows.Forms.TextBox ProjectNameTextbox;
        protected System.Windows.Forms.Label UsernameLabel;
        protected System.Windows.Forms.Label VCSTypeLabel;
        protected System.Windows.Forms.Label ProjectNameLabel;
    }
}