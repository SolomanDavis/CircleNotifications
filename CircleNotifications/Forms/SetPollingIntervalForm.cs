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
    public partial class SetPollingIntervalForm : Form {
        TrayAppContext appContext;

        public SetPollingIntervalForm() {
            InitializeComponent();
        }

        public SetPollingIntervalForm(TrayAppContext appContext) {
            this.appContext = appContext;
            InitializeComponent();
        }

        private void SetPollingIntervalButton_Click(object sender, EventArgs e) {
            try {
                string intervalStr = SetPollingIntervalTextBox.Text;
                if (intervalStr == null || intervalStr == "") { return; }
                int interval = int.Parse(intervalStr);

                Properties.Settings.Default.PollingInterval = interval;
                Properties.Settings.Default.Save();
                Console.WriteLine("Saved polling interval: " + interval);

                appContext.RestartTimer();

                DialogResult = DialogResult.OK;
                Close();
            } catch (FormatException exc) {
                Console.WriteLine("ERROR: Given interval was of the wrong format.");
                Console.WriteLine(exc.Message + "\n" + exc.StackTrace);
            }
        }
    }
}
