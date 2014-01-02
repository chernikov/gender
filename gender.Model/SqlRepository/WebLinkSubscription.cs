using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkSubscription> WebLinkSubscriptions
        {
            get
            {
                return Db.WebLinkSubscriptions;
            }
        }

        public bool CreateWebLinkSubscription(WebLinkSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.WebLinkSubscriptions.InsertOnSubmit(instance);
                Db.WebLinkSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateWebLinkSubscription(WebLinkSubscription instance)
        {
            var cache = Db.WebLinkSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.WebLinkID = instance.WebLinkID;
				cache.UserID = instance.UserID;
                Db.WebLinkSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveWebLinkSubscription(int idWebLinkSubscription)
        {
            WebLinkSubscription instance = Db.WebLinkSubscriptions.FirstOrDefault(p => p.ID == idWebLinkSubscription);
            if (instance != null)
            {
                Db.WebLinkSubscriptions.DeleteOnSubmit(instance);
                Db.WebLinkSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}