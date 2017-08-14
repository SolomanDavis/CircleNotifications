using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleNotifications {
    public class User {
        public Boolean? is_user { get; set; }
        public string login { get; set; }
        public string avatar_url { get; set; }
        public string name { get; set; }
        public string vcs_type { get; set; }
        public string id { get; set; }
    }
}
