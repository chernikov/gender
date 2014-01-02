using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Net;
using gender.Global.Config;
using System.Web.Mvc;
using Ninject;

namespace gender.Tools.Mail
{
    public class MailSender : IMailSender
    {
        [Inject]
        public IConfig Config {get; set; }
       
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void SendMail(string email, string subject, string body, MailAddress mailAddress = null)
        {
            try
            {
                logger.Error("Email : "  + email);
                logger.Error("Subject : " + subject);
                logger.Error("Body : " + body);
                if (Config.EnableMail)
                {
                    if (mailAddress == null)
                    {
                        mailAddress = new MailAddress(Config.MailSetting.SmtpReply, Config.MailSetting.SmtpUser);
                    }
                    MailMessage message = new MailMessage(
                        mailAddress,
                        new MailAddress(email))
                    {
                        Subject = subject,
                        BodyEncoding = Encoding.UTF8,
                        Body = body,
                        IsBodyHtml = true,
                        SubjectEncoding = Encoding.UTF8
                    };
                    SmtpClient client = new SmtpClient
                    {
                        Host = Config.MailSetting.SmtpServer,
                        Port = Config.MailSetting.SmtpPort,
                        UseDefaultCredentials = false,
                        EnableSsl = Config.MailSetting.EnableSsl,
                        Credentials =
                            new NetworkCredential(Config.MailSetting.SmtpUserName,
                                                  Config.MailSetting.SmtpPassword),
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    };
                    client.Send(message);
                }
                else
                {
                    logger.Debug("Email : {0} {1} \t Subject: {2} {3} Body: {4}", email, Environment.NewLine, subject, Environment.NewLine, body);
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Mail send exception {0}", ex.Message));
            }
        }
    }
}