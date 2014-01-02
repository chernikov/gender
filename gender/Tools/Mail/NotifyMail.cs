using System;
using System.Linq;
using System.Web.Mvc;
using gender.Global.Config;

namespace gender.Tools.Mail
{
    public static class NotifyMail
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static IConfig _config;

        public static IConfig Config
        {
            get
            {
                if (_config == null)
                {
                    _config = (DependencyResolver.Current).GetService<IConfig>();

                }
                return _config;
            }
        }

        private static IMailSender _mailSender;

        public static IMailSender MailSender
        {
            get
            {
                if (_mailSender == null)
                {
                    _mailSender = (DependencyResolver.Current).GetService<IMailSender>();

                }
                return _mailSender;
            }
        }

        public static void SendNotify(string templateName, string email,
            Func<string, string> subject,
            Func<string, string> body)
        {
            var template = Config.MailTemplates.FirstOrDefault(p => string.Compare(p.Name, templateName, true) == 0);
            if (template == null)
            {
                logger.Error("Can't find template (" + templateName + ")");
            }
            else
            {
                MailSender.SendMail(email,
                    subject.Invoke(template.Subject),
                    body.Invoke(template.Template));
            }
        }
    }
}