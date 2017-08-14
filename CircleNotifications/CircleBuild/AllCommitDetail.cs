using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleNotifications {
    public class AllCommitDetail {
        public object committer_date { get; set; }
        public string body { get; set; }
        public string branch { get; set; }
        public string author_date { get; set; }
        public object committer_email { get; set; }
        public string commit { get; set; }
        public object committer_login { get; set; }
        public object committer_name { get; set; }
        public string subject { get; set; }
        public string commit_url { get; set; }
        public string author_login { get; set; }
        public string author_name { get; set; }
        public string author_email { get; set; }
    }
}
