using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public interface IModerable : IMaterial
    {
        DateTime AddedDate { get; }
        DateTime? ModeratedDate { get; }
    }
}
