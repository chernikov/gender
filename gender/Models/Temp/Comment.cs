using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Models.Temp
{
    public class Comment
    {
        public int Cid { get; set; }

        public int Pid { get; set; }

        public int Nid { get; set; }

        public int Uid { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public string HostName { get; set; }

        public DateTime Timestamp { get; set; }

        public int Status { get; set; }

        public int Format { get; set; }

        public string Thread { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string HomePage { get; set; }

        public int? RealID { get; set; }
    }
}