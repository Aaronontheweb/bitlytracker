using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitlyTracker.Models
{
    public class BitlyUserClicksResponse
    {
        public int status_code { get; set; }
        public string status_txt { get; set; }

        public Data data { get; set; }

        public class Data
        {
            public int Days { get; set; }
            public int total_clicks { get; set; }
            public IList<Click> clicks { get; set; }

            public Data()
            {
                clicks = new List<Click>();
            }
        }

        public class Click
        {
            public int clicks { get; set; }
            public int day_start { get; set; }
        }
    }

    public class MissingBitlyUserClicksResponse : BitlyUserClicksResponse { }
}
