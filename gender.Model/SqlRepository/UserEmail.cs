using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UserEmail> UserEmails
        {
            get
            {
                return Db.UserEmails;
            }
        }

        public bool CreateUserEmail(UserEmail instance)
        {
            if (instance.ID == 0)
            {
                instance.ActivateLink = StringExtension.GenerateNewFile();
                Db.UserEmails.InsertOnSubmit(instance);
                Db.UserEmails.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateUserEmail(UserEmail instance)
        {
            var cache = Db.UserEmails.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
                cache.IsPrimary = instance.IsPrimary;
                cache.Sended = instance.Sended;
                if (string.Compare(instance.Email, cache.Email, true) != 0)
                {
                    cache.Email = instance.Email;
                    cache.ActivateLink = StringExtension.GenerateNewFile(); 
                    cache.ActivateDate = null;
                    cache.Sended = false;
                }
                Db.UserEmails.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUserEmail(int idUserEmail)
        {
            UserEmail instance = Db.UserEmails.FirstOrDefault(p => p.ID == idUserEmail);
            if (instance != null)
            {
                Db.UserEmails.DeleteOnSubmit(instance);
                Db.UserEmails.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}