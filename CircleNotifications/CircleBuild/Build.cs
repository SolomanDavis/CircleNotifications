using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CircleNotifications {

    public class Build {
        public string compare { get; set; }
        public PreviousSuccessfulBuild previous_successful_build { get; set; }
        public object build_parameters { get; set; }
        public Boolean? oss { get; set; }
        public Boolean? all_commit_details_truncated { get; set; }
        public object committer_date { get; set; }
        public string body { get; set; }
        public string usage_queued_at { get; set; }
        public object fail_reason { get; set; }
        public object retry_of { get; set; }
        public string reponame { get; set; }
        public List<object> ssh_users { get; set; }
        public string build_url { get; set; }
        public int? parallel { get; set; }
        public object failed { get; set; }
        public string branch { get; set; }
        public string username { get; set; }
        public string author_date { get; set; }
        public string why { get; set; }
        public User user { get; set; }
        public string vcs_revision { get; set; }
        public object vcs_tag { get; set; }
        public int? build_num { get; set; }
        public Boolean? infrastructure_fail { get; set; }
        public object committer_email { get; set; }
        public Boolean? has_artifacts { get; set; }
        public Previous previous { get; set; }
        public string status { get; set; }
        public object committer_name { get; set; }
        public object retries { get; set; }
        public string subject { get; set; }
        public string vcs_type { get; set; }
        public Boolean? timedout { get; set; }
        public object dont_build { get; set; }
        public string lifecycle { get; set; }
        public Boolean? no_dependency_cache { get; set; }
        public string stop_time { get; set; }
        public Boolean? ssh_disabled { get; set; }
        public int? build_time_millis { get; set; }
        public object picard { get; set; }
        public CircleYml circle_yml { get; set; }
        public List<Message> messages { get; set; }
        public Boolean? is_first_green_build { get; set; }
        public object job_name { get; set; }
        public string start_time { get; set; }
        public object canceler { get; set; }
        public List<AllCommitDetail> all_commit_details { get; set; }
        public string platform { get; set; }
        public string outcome { get; set; }
        public string vcs_url { get; set; }
        public string author_name { get; set; }
        public List<Node> node { get; set; }
        public string queued_at { get; set; }
        public Boolean? canceled { get; set; }
        public string author_email { get; set; }
    }
}
