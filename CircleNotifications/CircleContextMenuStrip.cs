using CircleNotifications.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CircleNotifications {
    class CircleContextMenuStrip : ContextMenuStrip {
        public ToolStripSeparator toolStripSeparator = new ToolStripSeparator();

        private TrayAppContext appContext;

        private static string NUM_BUILDS_MENU_NAME = "NumBuildsMenu";
        
        public CircleContextMenuStrip(TrayAppContext appContext) {
            this.appContext = appContext;

            //MatchNumBuilds(appContext.NumBuilds, Properties.Settings.)

            Items.Add(toolStripSeparator);

            Items.Add("Refresh", null, new EventHandler(Refresh_OnClick));
            Items.Add("Set Circle API Token", null, SetAPIToken_OnClick);

            Items.Add(CreateNumBuildsDropDownMenu());

            Items.Add("Set Polling Interval", null, SetPollingInterval_OnClick);
            Items.Add("Set Project Details", null, SetProjectDetails_OnClick);
            Items.Add("Exit", null, new EventHandler(Exit_OnClick));

            ShowCheckMargin = false;
            ShowImageMargin = false;
        }

        public List<int> GetBuildNumsDisplayed() {
            int separatorIndex = Items.IndexOf(toolStripSeparator);
            List<int> displayedBuildNums = new List<int>();
            for (int i = 0; i < separatorIndex; ++i) {
                displayedBuildNums.Add(int.Parse(Items[i].Name));
            }
            return displayedBuildNums;
        }

        public List<ToolStripLabel> GetBuildLabels() {
            List<ToolStripLabel> labels = new List<ToolStripLabel>();
            for (int i = Items.IndexOf(toolStripSeparator) - 1; i >= 0; --i) {
                labels.Add((ToolStripLabel)Items[i]);
            }
            return labels;
        }

        // Generate new ToolStripLabel from build properties.
        public ToolStripLabel CreateToolStripLabelFromBuild(Build build) {
            ToolStripLabel buildLabel = new ToolStripLabel() {
                Name = String.Format("{0}", build.build_num),
                Text = String.Format("{0} - #{1}: {2}", build.branch, build.build_num,
                    (build.subject != null) ? ((build.subject.Length > 30) ? build.subject.Substring(0, 27) + "..." : build.subject) : "")
            };

            // Link Properties
            buildLabel.IsLink = true;
            buildLabel.LinkBehavior = LinkBehavior.AlwaysUnderline;
            buildLabel.Tag = build.build_url;
            buildLabel.Click += new EventHandler((source, e) => {
                ToolStripLabel label = (ToolStripLabel)source;
                System.Diagnostics.Process.Start((string)label.Tag); // Open label link
            });

            UpdateLabelIcon(build.status, buildLabel);

            return buildLabel;
        }

        public void UpdateBuildLabel(Build build) {
            ToolStripItem[] stripCollection = Items.Find(String.Format("{0}", build.build_num), false);
            if (stripCollection.Length <= 0) {
                Console.WriteLine("Could not find build label with build number {0}!", build.build_num);
                return;
            }

            ToolStripLabel buildLabel = (ToolStripLabel) stripCollection[0];
            UpdateLabelIcon(build.status, buildLabel);
        }

        public void UpdateLabelIcon(string status, ToolStripLabel buildLabel) {
            switch (status) {
                case "fixed":
                case "success": buildLabel.Image = Properties.Resources.favicon_green_png; break;
                case "scheduled":
                case "queued": buildLabel.Image = Properties.Resources.favicon_grey_png; break;
                case "running": buildLabel.Image = Properties.Resources.favicon_blue_png; break;
                case "failed": buildLabel.Image = Properties.Resources.favicon_red_png; break;
                case "not_run":
                case "canceled":
                default: buildLabel.Image = Properties.Resources.favicon_undefined_png; break;
            }
        }

        public void MatchNumBuilds(int numBuilds, int lastKnownBuildNum) {
            List<int> displayedBuildNums = GetBuildNumsDisplayed();

            int buildNum = 0;
            if (displayedBuildNums.Count == 0) { // Not populated at all so we use lastKnownBuildNum
                buildNum = lastKnownBuildNum;
            } else {
                buildNum = displayedBuildNums[displayedBuildNums.Count - 1] - 1; // Start populating at the oldest displayed build - 1.
            }

            if (displayedBuildNums.Count < numBuilds) {
                try { // Optimized to only make two HTTP requests rather than however many is needed to match.
                    Requests requests = new Requests(appContext);
                    int difference = numBuilds - displayedBuildNums.Count;
                    int mostRecentBuildNum = appContext.DeserializeBuilds(requests.RecentBuilds(1, 0))[0].build_num.Value;

                    List<Build> builds = appContext.DeserializeBuilds(requests.RecentBuilds(difference, mostRecentBuildNum - buildNum));
                    Console.WriteLine("Builds.Count: " + builds.Count);
                    foreach (Build build in builds) {
                        switch (build.status) {
                            case "running":
                            case "scheduled":
                            case "queued": appContext.RunningBuilds.Add(build.build_num.Value); break;
                        }

                        AddBuildAtSeparator(build);
                    }
                } catch (Exception e) {
                    Console.WriteLine("ERROR: Failed to Match number of builds\n");
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                    return;
                }
            } else if (displayedBuildNums.Count > numBuilds) {
                RemoveBuilds(displayedBuildNums.Count - numBuilds);
            }
        }

        public void AddBuildAtSeparator(Build build) {
            int separatorIndex = Items.IndexOf(toolStripSeparator);
            Items.Insert(separatorIndex, CreateToolStripLabelFromBuild(build));
        }

        public Dictionary<string, int> AddBuilds(Dictionary<string, int> metrics, List<Build> builds) {
            for (int i = builds.Count - 1; i >= 0; --i) { // Loop backwards so as to add most recent build last.
                Build build = builds[i];
                if (build == null) {
                    Console.WriteLine("ERROR: Build to add is null.");
                    return null;
                }

                ToolStripLabel buildLabel = CreateToolStripLabelFromBuild(build);

                if (!build.build_num.HasValue) {
                    Console.WriteLine("ERROR: Build to add has null build num.");
                    return null;
                }
                int buildNum = build.build_num.Value;

                Console.WriteLine("Build status: " + build.status);
                switch (build.status) { // TODO: Review all build status possibilities for better coverage.
                    case "fixed":
                    case "success":
                        metrics["successfulBuildNum"] = buildNum;
                        ++metrics["success"]; break;
                    case "running":
                        appContext.RunningBuilds.Add(buildNum); break;
                    case "failed":
                        metrics["failedBuildNum"] = buildNum;
                        ++metrics["failed"]; break;
                    case "queued":
                        appContext.RunningBuilds.Add(buildNum); break;
                    case "scheduled":
                        appContext.RunningBuilds.Add(buildNum); break;
                    case "not_run": ++metrics["not_run"]; break;
                    case "canceled": ++metrics["canceled"]; break;
                }

                Items.Insert(0, buildLabel);
            }

            return metrics;
        }

        // Refreshes and redraws currently running builds (ones that we need to care about no matter what
        // we are showing). Updates metrics with any changes.
        public Dictionary<string, int> RefreshRunningBuilds(Dictionary<string, int> metrics) {
            Requests requests = new Requests(appContext);
            SortedSet<int> newRunningBuilds = new SortedSet<int>(appContext.RunningBuilds); // Copy running builds.
            foreach (int buildNum in appContext.RunningBuilds) {
                Build build = appContext.DeserializeBuild(requests.SingleBuild(buildNum));
                if (build == null || build.status == null) {
                    Console.WriteLine("Stopping Refresh because of failed parsing of build JSON.");
                    return null;
                }

                Console.WriteLine("Updating build {0}", buildNum);
                UpdateBuildLabel(build);

                switch (build.status) {
                    case "fixed":
                    case "success":
                        ++metrics["success"];
                        metrics["successfulBuildNum"] = buildNum;
                        newRunningBuilds.Remove(buildNum);
                        break;
                    case "canceled":
                        ++metrics["canceled"];
                        newRunningBuilds.Remove(buildNum);
                        break;
                    case "failed":
                        ++metrics["failed"];
                        metrics["failedBuildNum"] = buildNum;
                        newRunningBuilds.Remove(buildNum);
                        break;
                    default: break;
                }
            }
            appContext.RunningBuilds = newRunningBuilds;
            return metrics;
        }

        public void RemoveBuilds(int numToRemove) {
            while (Items.IndexOf(toolStripSeparator) > 0 && numToRemove > 0) {
                Items.RemoveAt(Items.IndexOf(toolStripSeparator) - 1);
                --numToRemove;
            }
        }

        public void RemoveBuilds() {
            while (Items.IndexOf(toolStripSeparator) > 0) {
                Items.RemoveAt(Items.IndexOf(toolStripSeparator) - 1);
            }
        }

        public bool IsCorrectNumBuilds(int numBuilds) {
            return Items.IndexOf(toolStripSeparator) == numBuilds;
        }

        private ToolStripItem CreateNumBuildsDropDownMenu() {
            ToolStripMenuItem menu = new ToolStripMenuItem() {
                Name = NUM_BUILDS_MENU_NAME,
                Text = "Number of builds to show"
            };

            int[] options = { 5, 10, 20, 30 };

            foreach (int option in options) {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = String.Format("{0}", option);
                item.Checked = (Properties.Settings.Default.NumBuilds == int.Parse(item.Text)) ? true : false;
                item.Click += NumBuilds_OnClick;
                menu.DropDownItems.Add(item);
            }

            return menu;
        }


        // Events
        private void NumBuilds_OnClick(object source, EventArgs e) {
            Properties.Settings.Default.NumBuilds = int.Parse(((ToolStripItem)source).Text);
            Properties.Settings.Default.Save();
            appContext.NumBuilds = Properties.Settings.Default.NumBuilds;

            ToolStripMenuItem menu = (ToolStripMenuItem) Items.Find(NUM_BUILDS_MENU_NAME, false)[0];
            ToolStripMenuItem sourceMenu = (ToolStripMenuItem)source;

            foreach (ToolStripMenuItem item in menu.DropDownItems) {
                if (item.Text != sourceMenu.Text) item.Checked = false;
                else item.Checked = true;
            }

            appContext.RefreshBuilds();
        }

        private void Refresh_OnClick(object source, EventArgs e) {
            // Ask user to set API token if they haven't already
            if (Properties.Settings.Default.CircleToken == null) {
                SetAPITokenForm form = new SetAPITokenForm(appContext);
                form.Show();
            }

            // Clear all current build labels
            RemoveBuilds();
            appContext.RefreshBuilds();
        }

        private void SetAPIToken_OnClick(object source, EventArgs e) {
            SetAPITokenForm form = new SetAPITokenForm(appContext);
            form.Show();
        }

        private void SetPollingInterval_OnClick(object source, EventArgs e) {
            SetPollingIntervalForm form = new SetPollingIntervalForm(appContext);
            form.Show();
        }

        private void SetProjectDetails_OnClick(object source, EventArgs e) {
            SetProjectDetailsForm form = new SetProjectDetailsForm(appContext, false);
            form.Show();
        }

        private void Exit_OnClick(object source, EventArgs e) {
            appContext.ExitThread();
        }
    }
}
