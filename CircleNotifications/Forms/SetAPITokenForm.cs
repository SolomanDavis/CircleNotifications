using System;
using System.Windows.Forms;

namespace CircleNotifications.Forms {
    public partial class SetAPITokenForm : Form {
        private TrayAppContext appContext;

        public SetAPITokenForm() {
            InitializeComponent();
        }

        public SetAPITokenForm(TrayAppContext appContext) {
            this.appContext = appContext;
            InitializeComponent();
        }

        private void SetAPITokenButton_Click(object sender, EventArgs e) {
            if (SetAPITokenTextBox.Text == null || SetAPITokenTextBox.Text == "") return;

            appContext.SetCircleToken(SetAPITokenTextBox.Text);

            Properties.Settings.Default.CircleToken = SetAPITokenTextBox.Text;
            Properties.Settings.Default.Save();
            Console.WriteLine("Saved API Token: " + Properties.Settings.Default.CircleToken);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
