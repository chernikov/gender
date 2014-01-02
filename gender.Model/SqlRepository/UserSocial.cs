using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UserSocial> UserSocials
        {
            get
            {
                return Db.UserSocials;
            }
        }

        public bool CreateUserSocial(UserSocial instance)
        {
            if (instance.ID == 0)
            {
                instance.Link = instance.Link ?? string.Empty;
                Db.UserSocials.InsertOnSubmit(instance);
                Db.UserSocials.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUserSocial(UserSocial instance)
        {
            var cache = Db.UserSocials.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.Provider = instance.Provider;
				cache.Link = instance.Link;
				cache.Identified = instance.Identified;
				cache.UserInfo = instance.UserInfo;
				cache.JsonResource = instance.JsonResource;
				cache.ExpiredBy = instance.ExpiredBy;
                Db.UserSocials.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUserSocial(int idUserSocial)
        {
            UserSocial instance = Db.UserSocials.FirstOrDefault(p => p.ID == idUserSocial);
            if (instance != null)
            {
                Db.UserSocials.DeleteOnSubmit(instance);
                Db.UserSocials.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}