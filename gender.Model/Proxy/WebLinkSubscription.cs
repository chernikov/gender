using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class WebLinkSubscription : ISubscriptionable
    {
        public IMaterial Material
        {
            get { return WebLink; }
        }
    }
}