using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationRecordRedirect> PublicationRecordRedirects
        {
            get
            {
                return Db.PublicationRecordRedirects;
            }
        }

        public bool CreatePublicationRecordRedirect(PublicationRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationRecordRedirects.InsertOnSubmit(instance);
                Db.PublicationRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePublicationRecordRedirect(PublicationRecordRedirect instance)
        {
            var cache = Db.PublicationRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.PublicationID = instance.PublicationID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.PublicationRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePublicationRecordRedirect(int idPublicationRecordRedirect)
        {
            PublicationRecordRedirect instance = Db.PublicationRecordRedirects.FirstOrDefault(p => p.ID == idPublicationRecordRedirect);
            if (instance != null)
            {
                Db.PublicationRecordRedirects.DeleteOnSubmit(instance);
                Db.PublicationRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}