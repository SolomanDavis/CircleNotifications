using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleNotifications {
    class NotifyIconManager {
        TrayAppContext appContext;
        private static NotifyIcon notifyIcon;

        public static string DEFAULT_TOOLTIP_TEXT = "Right-click to see your latest builds.";

        public NotifyIconManager(TrayAppContext appContext) {
            this.appContext = appContext;
        }

        // Initializes and displays the NotifyIcon.
        public static void InitNotifyIcon(TrayAppContext appContext, Container components) {
            ContextMenuStrip strip = new CircleContextMenuStrip(appContext);

            notifyIcon = new NotifyIcon(components) {
                ContextMenuStrip = strip,
                Icon = Properties.Resources.favicon_icon,
                Text = DEFAULT_TOOLTIP_TEXT,
                Visible = true
            };
        }

        public CircleContextMenuStrip GetCircleContextMenuStrip() {
            return (CircleContextMenuStrip) notifyIcon.ContextMenuStrip;
        }

        public void UpdateTrayIcon(Build build) {
            if (appContext.RunningBuilds.Count > 0) { // Running build takes precedence for system tray icon.
                notifyIcon.Text = NotifyIconManager.DEFAULT_TOOLTIP_TEXT;
                notifyIcon.Icon = Properties.Resources.favicon_blue_icon;
            } else if (build != null) {
                switch (build.status) {
                    case "success":
                        notifyIcon.Text = String.Format("Build #{0} was successful.", build.build_num);
                        notifyIcon.Icon = Properties.Resources.favicon_green_icon;
                        break;
                    case "scheduled":
                    case "queued":
                        notifyIcon.Text = String.Format("Build #{0} is queued or scheduled.", build.build_num);
                        notifyIcon.Icon = Properties.Resources.favicon_grey_icon;
                        break;
                    case "running":
                        notifyIcon.Text = String.Format("Build #{0} is running.", build.build_num);
                        notifyIcon.Icon = Properties.Resources.favicon_blue_icon;
                        break;
                    case "canceled":
                        notifyIcon.Text = String.Format("Build #{0} was canceled.", build.build_num);
                        notifyIcon.Icon = Properties.Resources.favicon_undefined_icon;
                        break;
                    case "failed":
                        notifyIcon.Text = String.Format("Build #{0} has failed.", build.build_num);
                        notifyIcon.Icon = Properties.Resources.favicon_red_icon;
                        break;
                    default:
                        notifyIcon.Text = DEFAULT_TOOLTIP_TEXT;
                        notifyIcon.Icon = Properties.Resources.favicon_icon;
                        break;
                }
            }
        }

        // Creates and displays balloon tip notification depending on given metrics.
        public void Notify(Dictionary<string, int> metrics) {
            if (metrics["failed"] > 0 || metrics["success"] > 0) {
                if (metrics["failed"] > 0) {
                    if (metrics["failed"] == 1) {
                        notifyIcon.BalloonTipTitle = String.Format("Oh no! build #{0} has failed", metrics["failedBuildNum"]);
                    } else {
                        notifyIcon.BalloonTipTitle = String.Format("Oh no! {0} builds have failed", metrics["failed"]);
                    }
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                } else if (metrics["success"] > 0) {
                    if (metrics["success"] == 1) {
                        notifyIcon.BalloonTipTitle = String.Format("Build #{0} has completed successfully", metrics["successfulBuildNum"]);
                    } else {
                        notifyIcon.BalloonTipTitle = String.Format("{0} builds have completed successfully", metrics["success"]);
                    }
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                }
                notifyIcon.BalloonTipText = String.Format(
                    "You have {0} failed builds and {1} successful builds" +
                    "Right-click the CircleNotifications tray icon for more information",
                    metrics["failed"], metrics["success"]
                );

                notifyIcon.ShowBalloonTip(5000);
            }
        }

        // Tears down the NotifyIcon.
        public static void TearDownNotifyIcon() {
            notifyIcon.Visible = false;
            notifyIcon = null;
        }
    }
}
