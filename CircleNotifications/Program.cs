using CircleNotifications.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleNotifications {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            TrayAppContext appContext = new TrayAppContext();

            // Retrieve CircleToken on startup
            if (Properties.Settings.Default.CircleToken == "") {
                SetAPITokenForm form = new SetAPITokenForm(appContext);
                if (form.ShowDialog() != DialogResult.OK) {
                    appContext.Dispose(); return;
                }
            }

            // Set required user parameters on startup
            if (Properties.Settings.Default.Username == "" ||
                Properties.Settings.Default.ProjectName == "" ||
                Properties.Settings.Default.VCSType == "") {
                SetProjectDetailsForm form = new SetProjectDetailsForm(appContext, true);
                if (form.ShowDialog() != DialogResult.OK) {
                    appContext.Dispose(); return;
                }
            }

            // Populate tray icon if we have everything we need to do so.
            appContext.RefreshBuilds();
            appContext.RestartTimer();

            Application.Run(appContext);
        }
    }
}
