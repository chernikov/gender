using gender.Global.Config;
using gender.Model;
using gender.Tools.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender
{
    public class MailProcessor
    {
        public static bool SendNextMail(IRepository Repository, MailSender mailSender)
        {
            var mail = Repository.PopMail();

            if (mail != null)
            {
                mailSender.SendMail(mail.Email, mail.Subject, mail.Body);
                Repository.ClearMailBody(mail.ID);
                return true;
            }
            return false;
        }
    }
}