using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleNotifications {
    public class Node {
        public string public_ip_addr { get; set; }
        public int? port { get; set; }
        public string username { get; set; }
        public string image_id { get; set; }
        public object ssh_enabled { get; set; }
    }
}
