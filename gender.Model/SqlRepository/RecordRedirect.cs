using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<RecordRedirect> RecordRedirects
        {
            get
            {
                return Db.RecordRedirects;
            }
        }

        public bool CreateRecordRedirect(RecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.RecordRedirects.InsertOnSubmit(instance);
                Db.RecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateRecordRedirect(RecordRedirect instance)
        {
            var cache = Db.RecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Url = instance.Url;
				cache.IsForum = instance.IsForum;
                Db.RecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRecordRedirect(int idRecordRedirect)
        {
            RecordRedirect instance = Db.RecordRedirects.FirstOrDefault(p => p.ID == idRecordRedirect);
            if (instance != null)
            {
                Db.RecordRedirects.DeleteOnSubmit(instance);
                Db.RecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}