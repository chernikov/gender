using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using gender.Model;

namespace gender.Global.Auth
{
    public interface IUserable : IIdentity
    {
        User User { get; }
    }
}
