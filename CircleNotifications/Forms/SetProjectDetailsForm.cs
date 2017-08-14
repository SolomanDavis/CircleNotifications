using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleNotifications.Forms {
    public partial class SetProjectDetailsForm : Form {
        TrayAppContext appContext;
        bool enforceSubmission;

        public SetProjectDetailsForm() {
            InitializeComponent();
        }

        public SetProjectDetailsForm(TrayAppContext appContext, bool enforceSubmission) {
            this.appContext = appContext;
            this.enforceSubmission = enforceSubmission;
            InitializeComponent();
        }

        private void SetProjectDetailsFormButton_Click(object sender, EventArgs e) {
            if (enforceSubmission) {
                if (UsernameTextbox.Text != null && UsernameTextbox.Text != "") {
                    Properties.Settings.Default.Username = UsernameTextbox.Text;
                } else {
                    MessageBox.Show("Please submit a (project-wide) username");
                    return;
                }

                if ((VCSTypeTextbox.Text != null && VCSTypeTextbox.Text != "") &&
                    (VCSTypeTextbox.Text == "bitbucket" || VCSTypeTextbox.Text == "github")) {
                    Properties.Settings.Default.VCSType = VCSTypeTextbox.Text;
                } else {
                    MessageBox.Show("Please submit a valid VCS type (one of 'bitbucket' or 'github').");
                    return;
                }

                if (ProjectNameTextbox.Text != null && ProjectNameTextbox.Text != "") {
                    Properties.Settings.Default.ProjectName = ProjectNameTextbox.Text;
                } else {
                    MessageBox.Show("Please submit a project (repository) name.");
                    return;
                }
            } else {
                if (UsernameTextbox.Text != "") Properties.Settings.Default.Username = UsernameTextbox.Text;
                if (VCSTypeTextbox.Text != "") Properties.Settings.Default.VCSType = VCSTypeTextbox.Text;
                if (ProjectNameTextbox.Text != "") Properties.Settings.Default.ProjectName = ProjectNameTextbox.Text;
            }

            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
