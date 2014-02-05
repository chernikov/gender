using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public interface IMaterial
    {
        int ID { get; }

        string Url { get; }

        string TypeUrl { get; }

        string ClassName { get; }

        string Name { get; }

        string MaterialType { get; }

        IList<User> CommentSubscribers { get; }

        string DefaultUrl { get; }

    }
}
