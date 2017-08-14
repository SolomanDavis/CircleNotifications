using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleNotifications {
    public class PreviousSuccessfulBuild {
        public int? build_num { get; set; }
        public string status { get; set; }
        public int? build_time_millis { get; set; }
    }
}
