using System.Globalization;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;
using gender.Global.Config.OAuth;

namespace gender.Global.Config
{
    public class Config : IConfig
    {
        public string ConnectionStrings(string connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }

        public string CultureCode
        {
            get
            {
                var culture = ConfigurationManager.AppSettings["Culture"];
                if (culture != null)
                {
                    return culture;
                }
                return "en";
            }
        }

        public CultureInfo Culture
        {
            get
            {
                return new CultureInfo(CultureCode);
            }
        }
        public bool DebugMode
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["DebugMode"]);
            }
        }

        public string AdminEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminEmail"];
            }
        }

        public bool EnableMail
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["EnableMail"]);
            }
        }

        public IQueryable<MimeType> MimeTypes
        {
            get
            {
                MimeTypesConfigSection configInfo = (MimeTypesConfigSection)ConfigurationManager.GetSection("mimeConfig");
                return configInfo.mimeTypes.OfType<MimeType>().AsQueryable<MimeType>();
            }
        }

        public MailSetting MailSetting
        {
            get
            {
                return (MailSetting)ConfigurationManager.GetSection("mailConfig");
            }
        }


        public IQueryable<MailTemplate> MailTemplates
        {
            get
            {
                MailTemplateConfig configInfo = (MailTemplateConfig)ConfigurationManager.GetSection("mailTemplatesConfig");
                return configInfo.mailTemplates.OfType<MailTemplate>().AsQueryable<MailTemplate>();
            }
        }

        public IQueryable<IconSize> IconSizes
        {
            get
            {
                IconSizesConfigSection configInfo = (IconSizesConfigSection)ConfigurationManager.GetSection("iconConfig");
                if (configInfo != null)
                {
                    return configInfo.iconSizes.OfType<IconSize>().AsQueryable<IconSize>();
                }
                return null;
            }
        }

        public Social.Vkontakte.IVkAppConfig VkAppConfig
        {
            get
            {
                return (VkontakteAppConfig)ConfigurationManager.GetSection("vkAppConfig");
            }
        }

        public Social.Twitter.ITwitterAppConfig TwitterAppConfig
        {
            get
            {
                return (TwitterAppConfig)ConfigurationManager.GetSection("twitterAppConfig");
            }
        }

        public Social.Facebook.IFbAppConfig FacebookAppConfig
        {
            get
            {
                return (FacebookAppConfig)ConfigurationManager.GetSection("facebookAppConfig");
            }
        }

        public Social.Google.IGoogleAppConfig GoogleAppConfig
        {
            get
            {
                return (GoogleAppConfig)ConfigurationManager.GetSection("googleAppConfig");
            }
        }

        public Social.Yandex.IYandexAppConfig YandexAppConfig
        {
            get
            {
                return (YandexAppConfig)ConfigurationManager.GetSection("yandexAppConfig");
            }
        }

        public Social.Mailru.IMailruAppConfig MailruAppConfig
        {
            get
            {
                return (MailruAppConfig)ConfigurationManager.GetSection("mailruAppConfig");
            }
        }

    }
}