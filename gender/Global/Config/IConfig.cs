using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using gender.Social.Twitter;
using gender.Social.Facebook;
using gender.Social.Google;
using gender.Social.Vkontakte;
using gender.Social.Yandex;
using gender.Social.Mailru;

namespace gender.Global.Config
{
    public interface IConfig
    {
        string ConnectionStrings(string connectionString);

        string CultureCode { get; }

        CultureInfo Culture { get; }

        bool DebugMode { get;  }

        string AdminEmail { get;  }

        MailSetting MailSetting { get; }

        bool EnableMail { get; }

        IQueryable<MimeType> MimeTypes {  get; }

        IQueryable<MailTemplate> MailTemplates { get; }

        IQueryable<IconSize> IconSizes {get;}

        ITwitterAppConfig TwitterAppConfig { get; }

        IFbAppConfig FacebookAppConfig { get; }

        IGoogleAppConfig GoogleAppConfig { get; }

        IVkAppConfig VkAppConfig { get; }

        IYandexAppConfig YandexAppConfig { get; }

        IMailruAppConfig MailruAppConfig { get; }
    }
}
