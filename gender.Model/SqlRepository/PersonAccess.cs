using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PersonAccess> PersonAccesses
        {
            get
            {
                return Db.PersonAccesses;
            }
        }

        public bool CreatePersonAccess(PersonAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.PersonAccesses.InsertOnSubmit(instance);
                Db.PersonAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePersonAccess(PersonAccess instance)
        {
            var cache = Db.PersonAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.PersonID = instance.PersonID;
				cache.UserID = instance.UserID;
                Db.PersonAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePersonAccess(int idPersonAccess)
        {
            PersonAccess instance = Db.PersonAccesses.FirstOrDefault(p => p.ID == idPersonAccess);
            if (instance != null)
            {
                Db.PersonAccesses.DeleteOnSubmit(instance);
                Db.PersonAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}