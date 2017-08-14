using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CircleNotifications.Forms;
using Microsoft.Win32;

namespace CircleNotifications {
    public class TrayAppContext : ApplicationContext {
        Container component;

        // CircleCI User-Level API token.
        public string CircleToken { get; private set; }

        public int NumBuilds { get; set; }

        // Index 0 is most recent build.
        public SortedSet<int> RunningBuilds { get; set; }
        int lastKnownBuildNum;

        // Timer to poll for build updates.
        Timer timer = new Timer();
        bool refreshing = false;

        public TrayAppContext() {
            InitContext();
        }

        private void InitContext() {
            Properties.Settings.Default.Upgrade();

            CircleToken = Properties.Settings.Default.CircleToken;
            NumBuilds = Properties.Settings.Default.NumBuilds;
            lastKnownBuildNum = Properties.Settings.Default.LastKnownBuildNum;

            RunningBuilds = Properties.Settings.Default.RunningBuilds;
            if (RunningBuilds == null) RunningBuilds = new SortedSet<int>();

            // Make application run on startup. consider offering way to disable run on startup?
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (registryKey.GetValue("CircleNotifications") == null) {
                registryKey.SetValue("CircleNotifications", Application.ExecutablePath);
            }

            component = new Container();
            NotifyIconManager.InitNotifyIcon(this, component);

            timer.Tick += new EventHandler(OnTick);
        }

        // Refresh builds to display in the system tray.
        // Produces balloontip notifications if there are any newly successful or failed builds.
        public void RefreshBuilds() {
            Console.WriteLine("Refresh called from thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            if (!refreshing) refreshing = true;
            else return;
            Console.WriteLine("Begin refresh.");

            // Keep track of the number of builds so we can display in a notification.
            Dictionary<string, int> metrics = new Dictionary<string, int> {
                ["success"] = 0,
                ["failed"] = 0,
                // ["queued"] = 0, ["scheduled"] = 0, ["running"] = 0, Too much work and kind of annoying.
                ["not_run"] = 0,
                ["canceled"] = 0,
                ["successfulBuildNum"] = 0,
                ["failedBuildNum"] = 0
            };

            Requests requests = new Requests(this);
            NotifyIconManager nManager = new NotifyIconManager(this);
            CircleContextMenuStrip strip = nManager.GetCircleContextMenuStrip();

            try {
                // First refresh running builds.
                metrics = strip.RefreshRunningBuilds(metrics);
                if (metrics == null) throw new NullReferenceException("ERROR: metrics was null. A call to the strip failed.");

                // Then add any new builds to the strip, collecting metrics along the way.
                int numNewBuilds = GetNumNewBuilds();
                if (numNewBuilds > 0) {
                    List<Build> builds = DeserializeBuilds(requests.RecentBuilds(numNewBuilds, 0));
                    if (builds == null) throw new NullReferenceException("ERROR: Deserializing " + numNewBuilds + " builds failed.");

                    strip.RemoveBuilds(numNewBuilds);
                    metrics = strip.AddBuilds(metrics, builds);
                }
                if (metrics == null) throw new NullReferenceException("ERROR: metrics was null. A call to the strip failed.");

                // Ensure we are displaying the right number of builds.
                if (!strip.IsCorrectNumBuilds(NumBuilds)) strip.MatchNumBuilds(NumBuilds, lastKnownBuildNum);
            } catch (Exception e) {
                Console.WriteLine("ERROR: Got exception attempting to refresh builds\n" + e.StackTrace);
                refreshing = false; return;
            }

            // Change icon to reflect the status of the latest build regardles of whether we notified the user
            Build mostRecentBuild = DeserializeBuild(requests.SingleBuild(lastKnownBuildNum));
            nManager.UpdateTrayIcon(mostRecentBuild);

            // Notify user if we found any failed or successful builds.
            nManager.Notify(metrics);

            // Save updated values
            Properties.Settings.Default.LastKnownBuildNum = lastKnownBuildNum;
            Properties.Settings.Default.RunningBuilds = RunningBuilds;
            Properties.Settings.Default.DisplayedLabels = strip.GetBuildLabels();
            Properties.Settings.Default.Save();

            Console.WriteLine("Refresh complete");
            refreshing = false;
        }

        // Returns the number of new builds since the last time we checked for new builds and updates the cache.
        private int GetNumNewBuilds() {
            Requests requests = new Requests(this);
            string json = requests.RecentBuilds(1, 0);
            List<Build> builds = DeserializeBuilds(json);

            if (builds == null || builds.Count != 1) return 0;

            int mostRecentBuildNum, lastBuild = lastKnownBuildNum;
            if (builds[0].build_num.HasValue) {
                mostRecentBuildNum = builds[0].build_num.Value;
            } else {
                Console.WriteLine("ERROR: Failed to retrieve most recent build number. Try again.");
                return 0;
            }

            lastKnownBuildNum = mostRecentBuildNum;
            return mostRecentBuildNum - lastBuild;
        }

        
        // Deserialize JSON strings.
        public List<Build> DeserializeBuilds(string json) {
            try {
                JArray jsonBuilds = JArray.Parse(json);

                List<Build> builds = new List<Build>();
                foreach (JObject jsonBuild in jsonBuilds) {
                    Build build = JsonConvert.DeserializeObject<Build>(jsonBuild.ToString());
                    builds.Add(build);
                }

                return builds;
            } catch (Exception e) {
                Console.WriteLine("ERROR: Failed to deserialize build JSON\n" + e.StackTrace);
                return null;
            }
        }

        public Build DeserializeBuild(string json) {
            try {
                JObject jsonBuild = JObject.Parse(json);
                Build build = JsonConvert.DeserializeObject<Build>(jsonBuild.ToString());
                return build;
            } catch (Exception e) {
                Console.WriteLine("ERROR: Failed to deserialize build JSON\n" + e.StackTrace);
                return null;
            }
        }


        // Restart timer with new interval
        public void RestartTimer() {
            timer.Stop();
            timer.Interval = Properties.Settings.Default.PollingInterval;
            timer.Start();
        }

        // Poll for build updates when Timer raises event
        private void OnTick(object source, EventArgs e) {
            timer.Stop();

            Console.WriteLine("{0:HH:mm:ss.fff} - Checking for build updates", System.DateTime.Now);
            if (CircleToken == null) return;
            RefreshBuilds();

            timer.Interval = Properties.Settings.Default.PollingInterval;
            timer.Start();
        }
        
        // Reset relevant settings to default if CircleToken changes.
        public void SetCircleToken(string newCircleToken) {
            ClearContextOfBuilds();
            CircleToken = newCircleToken;
        }

        public void ClearContextOfBuilds() {
            lastKnownBuildNum = 0;
            RunningBuilds = new SortedSet<int>();
            Properties.Settings.Default.LastKnownBuildNum = lastKnownBuildNum;
            Properties.Settings.Default.RunningBuilds = RunningBuilds;
            Properties.Settings.Default.DisplayedLabels = new List<ToolStripLabel>();
            Properties.Settings.Default.Save();
        }


        // Teardown
        protected override void Dispose(bool disposing) {
            if (disposing && component != null) {
                component.Dispose();
            }

            if (disposing && timer != null) {
                timer.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void ExitThreadCore() {
            NotifyIconManager.TearDownNotifyIcon();
            base.ExitThreadCore();
        }
    }
}
