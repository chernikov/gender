using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public interface ICommentable
    {
        IMaterial Material { get; }

        Comment Comment { get; }
    }
}
