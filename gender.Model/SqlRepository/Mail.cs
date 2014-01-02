using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<Mail> Mails
        {
            get
            {
                return Db.Mails;
            }
        }

        public bool SaveMail(Mail instance)
        {
           
            instance.ProcessedDate = DateTime.Now;
            instance.Subject = "";
            instance.Body = "";
            Db.Mails.InsertOnSubmit(instance);
            Db.Mails.Context.SubmitChanges();
            return true;
        }

        public bool PushMail(Mail instance)
        {
            instance.AddedDate = DateTime.Now;
            Db.Mails.InsertOnSubmit(instance);
            Db.Mails.Context.SubmitChanges();
            return true;
        }

        public Mail PopMail()
        {
            var instance = Db.Mails.FirstOrDefault(p => !p.ProcessedDate.HasValue);
            if (instance != null)
            {
                instance.ProcessedDate = DateTime.Now;
                Db.Mails.Context.SubmitChanges();
                return instance;
            }
            return null;
        }

        public Mail PopMail(int id)
        {
            var instance = Db.Mails.FirstOrDefault(p => p.ID == id);
            if (instance != null)
            {
                instance.ProcessedDate = DateTime.Now;
                Db.Mails.Context.SubmitChanges();
                return instance;
            }
            return null;
        }

        public void ClearMailBody(int id)
        {
            var instance = Db.Mails.FirstOrDefault(p => p.ID == id);
            if (instance != null)
            {
                instance.Body = string.Empty;
                Db.Mails.Context.SubmitChanges();
            }
        }
    }
}