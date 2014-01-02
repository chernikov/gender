using gender.Social.Mailru;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace gender.Global.Config.OAuth
{
    public class MailruAppConfig : ConfigurationSection, IMailruAppConfig
    {
        [ConfigurationProperty("AppId", IsRequired = true)]
        public string AppId
        {
            get
            {
                return this["AppId"] as string;
            }

            set
            {
                this["AppId"] = value;
            }
        }

        [ConfigurationProperty("AppPrivate", IsRequired = true)]
        public string AppPrivate
        {
            get
            {
                return this["AppPrivate"] as string;
            }

            set
            {
                this["AppPrivate"] = value;
            }
        }

        [ConfigurationProperty("AppSecret", IsRequired = true)]
        public string AppSecret
        {
            get
            {
                return this["AppSecret"] as string;
            }

            set
            {
                this["AppSecret"] = value;
            }
        }
    }
}