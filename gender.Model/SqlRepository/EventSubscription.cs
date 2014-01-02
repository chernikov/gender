using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventSubscription> EventSubscriptions
        {
            get
            {
                return Db.EventSubscriptions;
            }
        }

        public bool CreateEventSubscription(EventSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.EventSubscriptions.InsertOnSubmit(instance);
                Db.EventSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateEventSubscription(EventSubscription instance)
        {
            var cache = Db.EventSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.EventID = instance.EventID;
				cache.UserID = instance.UserID;
                Db.EventSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveEventSubscription(int idEventSubscription)
        {
            EventSubscription instance = Db.EventSubscriptions.FirstOrDefault(p => p.ID == idEventSubscription);
            if (instance != null)
            {
                Db.EventSubscriptions.DeleteOnSubmit(instance);
                Db.EventSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}