using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CircleNotifications {
    class Requests {
        public const string BASE_PATH = "https://circleci.com/api/v1.1/";

        private TrayAppContext appContext;

        string username, vcsType, projectName;

        public Requests(TrayAppContext appContext) {
            this.appContext = appContext;
            username = Properties.Settings.Default.Username;
            vcsType = Properties.Settings.Default.VCSType;
            projectName = Properties.Settings.Default.ProjectName;
        }

        // Hit Endpoints
        public string Me() {
            UriBuilder uriBuilder = NewUriBuilder();

            uriBuilder.Path += "me";

            return Get(uriBuilder.Uri);
        }

        public string Projects() {
            UriBuilder uriBuilder = NewUriBuilder();

            uriBuilder.Path += "projects";

            return Get(uriBuilder.Uri);
        }

        public string RecentBuilds(int limit, int offset) {
            UriBuilder uriBuilder = NewUriBuilder();

            uriBuilder.Path += String.Format("project/{0}/{1}/{2}", vcsType, username, projectName);
            uriBuilder = addQuery(uriBuilder, "limit", (limit > appContext.NumBuilds) ? appContext.NumBuilds : limit);
            uriBuilder = addQuery(uriBuilder, "offset", offset);

            return Get(uriBuilder.Uri);
        }

        public string RecentBuilds(int limit, int offset, string filter) {
            UriBuilder uriBuilder = NewUriBuilder();

            uriBuilder.Path += String.Format("project/{0}/{1}/{2}", vcsType, username, projectName);
            uriBuilder = addQuery(uriBuilder, "limit", (limit > appContext.NumBuilds) ? appContext.NumBuilds : limit);
            uriBuilder = addQuery(uriBuilder, "offset", offset);
            uriBuilder = addQuery(uriBuilder, "filter", filter);

            return Get(uriBuilder.Uri);
        }

        public string SingleBuild(int buildNum) {
            UriBuilder uriBuilder = NewUriBuilder();

            uriBuilder.Path += String.Format("project/{0}/{1}/{2}/{3}", vcsType, username, projectName, buildNum);

            return Get(uriBuilder.Uri);
        }


        private string Get(Uri uri) {
            string jsonResponse = null;

            if (uri == null) return jsonResponse;

            try {
                HttpWebRequest httpRequest = (HttpWebRequest) WebRequest.Create(uri.ToString());
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                HttpWebResponse httpResponse = (HttpWebResponse) httpRequest.GetResponse();
                StreamReader stream = new StreamReader(httpResponse.GetResponseStream());

                jsonResponse = stream.ReadToEnd();
            } catch (Exception e) {
                Console.WriteLine("ERROR: Failed to get response from " + uri.ToString());
                Console.WriteLine(e.StackTrace);
            }

            return jsonResponse;
        }


        private UriBuilder NewUriBuilder() {
            UriBuilder uriBuilder = new UriBuilder(BASE_PATH);

            if (appContext.CircleToken == null) {
                throw new ArgumentNullException("ERROR: No CircleToken set!");
            }

            uriBuilder.Query = String.Format("circle-token={0}", appContext.CircleToken);
            return uriBuilder;
        }

        private UriBuilder addQuery(UriBuilder uriBuilder, string queryName, int query) {
            uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + String.Format("{0}={1}", queryName, query);
            return uriBuilder;
        }

        private UriBuilder addQuery(UriBuilder uriBuilder, string queryName, string query) {
            uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + String.Format("{0}={1}", queryName, query);
            return uriBuilder;
        }

    }
}
