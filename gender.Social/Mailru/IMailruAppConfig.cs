using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gender.Social.Mailru
{
    public interface IMailruAppConfig
    {
        string AppId { get; }
        string AppPrivate { get; }
        string AppSecret { get; }
    }
}
