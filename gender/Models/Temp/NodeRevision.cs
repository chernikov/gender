using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Models.Temp
{
    public class NodeRevision
    {
        public int Nid { get; set; }

        public int Vid { get; set; }

        public int Uid { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Teaser { get; set; }

        public string Log { get; set; }

        public DateTime Timestamp { get; set; }

        public int Format { get; set; }
    }
}