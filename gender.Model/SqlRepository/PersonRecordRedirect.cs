using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PersonRecordRedirect> PersonRecordRedirects
        {
            get
            {
                return Db.PersonRecordRedirects;
            }
        }

        public bool CreatePersonRecordRedirect(PersonRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.PersonRecordRedirects.InsertOnSubmit(instance);
                Db.PersonRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePersonRecordRedirect(PersonRecordRedirect instance)
        {
            var cache = Db.PersonRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.PersonID = instance.PersonID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.PersonRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePersonRecordRedirect(int idPersonRecordRedirect)
        {
            PersonRecordRedirect instance = Db.PersonRecordRedirects.FirstOrDefault(p => p.ID == idPersonRecordRedirect);
            if (instance != null)
            {
                Db.PersonRecordRedirects.DeleteOnSubmit(instance);
                Db.PersonRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}