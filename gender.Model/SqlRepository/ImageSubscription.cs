using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageSubscription> ImageSubscriptions
        {
            get
            {
                return Db.ImageSubscriptions;
            }
        }

        public bool CreateImageSubscription(ImageSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.ImageSubscriptions.InsertOnSubmit(instance);
                Db.ImageSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateImageSubscription(ImageSubscription instance)
        {
            var cache = Db.ImageSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ImageID = instance.ImageID;
				cache.UserID = instance.UserID;
                Db.ImageSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveImageSubscription(int idImageSubscription)
        {
            ImageSubscription instance = Db.ImageSubscriptions.FirstOrDefault(p => p.ID == idImageSubscription);
            if (instance != null)
            {
                Db.ImageSubscriptions.DeleteOnSubmit(instance);
                Db.ImageSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}