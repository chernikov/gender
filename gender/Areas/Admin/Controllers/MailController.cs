using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Admin.Controllers
{
    public class MailController : AdminController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Test()
        {
            var mailAddress = new MailAddress(Config.MailSetting.SmtpReply, Config.MailSetting.SmtpUser);

            MailMessage message = new MailMessage(
                       mailAddress,
                       new MailAddress("chernikov@gmail.com"))
            {
                Subject = "Test subject",
                BodyEncoding = Encoding.UTF8,
                Body = "Test Body",
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

            return Content("OK");
        }


    }
}
