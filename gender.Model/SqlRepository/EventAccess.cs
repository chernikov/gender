using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventAccess> EventAccesses
        {
            get
            {
                return Db.EventAccesses;
            }
        }

        public bool CreateEventAccess(EventAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.EventAccesses.InsertOnSubmit(instance);
                Db.EventAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateEventAccess(EventAccess instance)
        {
            var cache = Db.EventAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.EventID = instance.EventID;
				cache.UserID = instance.UserID;
                Db.EventAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveEventAccess(int idEventAccess)
        {
            EventAccess instance = Db.EventAccesses.FirstOrDefault(p => p.ID == idEventAccess);
            if (instance != null)
            {
                Db.EventAccesses.DeleteOnSubmit(instance);
                Db.EventAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}