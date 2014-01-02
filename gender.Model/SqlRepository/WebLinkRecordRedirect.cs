using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkRecordRedirect> WebLinkRecordRedirects
        {
            get
            {
                return Db.WebLinkRecordRedirects;
            }
        }

        public bool CreateWebLinkRecordRedirect(WebLinkRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.WebLinkRecordRedirects.InsertOnSubmit(instance);
                Db.WebLinkRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateWebLinkRecordRedirect(WebLinkRecordRedirect instance)
        {
            var cache = Db.WebLinkRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.WebLinkID = instance.WebLinkID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.WebLinkRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveWebLinkRecordRedirect(int idWebLinkRecordRedirect)
        {
            WebLinkRecordRedirect instance = Db.WebLinkRecordRedirects.FirstOrDefault(p => p.ID == idWebLinkRecordRedirect);
            if (instance != null)
            {
                Db.WebLinkRecordRedirects.DeleteOnSubmit(instance);
                Db.WebLinkRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}