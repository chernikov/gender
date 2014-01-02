using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public interface IAccess
    {
        IMaterial Material { get; }

        User User { get; }
    }
}
