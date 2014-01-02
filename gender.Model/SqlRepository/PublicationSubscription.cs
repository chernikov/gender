using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationSubscription> PublicationSubscriptions
        {
            get
            {
                return Db.PublicationSubscriptions;
            }
        }

        public bool CreatePublicationSubscription(PublicationSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationSubscriptions.InsertOnSubmit(instance);
                Db.PublicationSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePublicationSubscription(PublicationSubscription instance)
        {
            var cache = Db.PublicationSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.PublicationID = instance.PublicationID;
				cache.UserID = instance.UserID;
                Db.PublicationSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePublicationSubscription(int idPublicationSubscription)
        {
            PublicationSubscription instance = Db.PublicationSubscriptions.FirstOrDefault(p => p.ID == idPublicationSubscription);
            if (instance != null)
            {
                Db.PublicationSubscriptions.DeleteOnSubmit(instance);
                Db.PublicationSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}