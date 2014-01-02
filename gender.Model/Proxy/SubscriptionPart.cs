using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class SubscriptionPart
    {
        public enum UpdateTypeEnum
        {
            Update = 0x01, 
            Comment = 0x02,
            Immediate = 0x03
        }
	}
}