using System;
using System.Collections.Generic;
using System.Net;

namespace BitlyTracker.Models
{
    public class BitlyRealtimeLinksResponse
    {
        public int status_code { get; set; }
        public string status_txt { get; set; }

        public Data data { get; set; }

        public class Data
        {
            public IList<Link> realtime_links { get; set; }

            public Data()
            {
                realtime_links = new List<Link>();
            }
        }

        public class Link
        {
            public int clicks { get; set; }
            public string user_hash { get; set; }
            public string bitlyUrl { get { return string.Format("http://bit.ly{0}", user_hash); } }
        }
    }
}
