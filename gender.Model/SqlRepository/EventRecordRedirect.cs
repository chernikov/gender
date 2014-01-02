using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventRecordRedirect> EventRecordRedirects
        {
            get
            {
                return Db.EventRecordRedirects;
            }
        }

        public bool CreateEventRecordRedirect(EventRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.EventRecordRedirects.InsertOnSubmit(instance);
                Db.EventRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateEventRecordRedirect(EventRecordRedirect instance)
        {
            var cache = Db.EventRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.EventID = instance.EventID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.EventRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveEventRecordRedirect(int idEventRecordRedirect)
        {
            EventRecordRedirect instance = Db.EventRecordRedirects.FirstOrDefault(p => p.ID == idEventRecordRedirect);
            if (instance != null)
            {
                Db.EventRecordRedirects.DeleteOnSubmit(instance);
                Db.EventRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}