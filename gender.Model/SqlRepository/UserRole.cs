using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UserRole> UserRoles
        {
            get
            {
                return Db.UserRoles;
            }
        }

        public bool CreateUserRole(UserRole instance)
        {
            if (instance.ID == 0)
            {
                Db.UserRoles.InsertOnSubmit(instance);
                Db.UserRoles.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUserRole(UserRole instance)
        {
            var cache = Db.UserRoles.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.RoleID = instance.RoleID;
				cache.UserID = instance.UserID;
                Db.UserRoles.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUserRole(int idUserRole)
        {
            UserRole instance = Db.UserRoles.FirstOrDefault(p => p.ID == idUserRole);
            if (instance != null)
            {
                Db.UserRoles.DeleteOnSubmit(instance);
                Db.UserRoles.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}