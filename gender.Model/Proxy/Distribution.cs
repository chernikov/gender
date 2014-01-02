using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Distribution
    {
        public bool AnyMails
        {
            get
            {
                return Mails.Any();
            }
        }

        public int CountDeliveredMails
        {
            get
            {
                return Mails.Count(p => p.ProcessedDate.HasValue);
            }
        }

        public int CountMails
        {
            get
            {
                return Mails.Count();
            }
        }
	}
}