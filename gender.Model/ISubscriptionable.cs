using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public interface ISubscriptionable
    {
        int ID { get; }

        IMaterial Material { get; }

        User User { get; }
    }
}
