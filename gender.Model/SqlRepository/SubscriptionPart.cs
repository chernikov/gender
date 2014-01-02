using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<SubscriptionPart> SubscriptionParts
        {
            get
            {
                return Db.SubscriptionParts;
            }
        }

        public bool CreateSubscriptionPart(SubscriptionPart instance)
        {
            if (instance.ID == 0)
            {
                Db.SubscriptionParts.InsertOnSubmit(instance);
                Db.SubscriptionParts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateSubscriptionPart(SubscriptionPart instance)
        {
            var cache = Db.SubscriptionParts.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.AddedDate = instance.AddedDate;
				cache.Text = instance.Text;
				cache.UpdateType = instance.UpdateType;
				cache.IsProcessed = instance.IsProcessed;
                Db.SubscriptionParts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveSubscriptionPart(int idSubscriptionPart)
        {
            SubscriptionPart instance = Db.SubscriptionParts.FirstOrDefault(p => p.ID == idSubscriptionPart);
            if (instance != null)
            {
                Db.SubscriptionParts.DeleteOnSubmit(instance);
                Db.SubscriptionParts.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public void SetProcessSubscriptionPart(IList<SubscriptionPart> list)
        {
            foreach (var item in list)
            {
                var instance = Db.SubscriptionParts.FirstOrDefault(p => p.ID == item.ID);
                if (instance != null)
                {
                    instance.IsProcessed = true;
                    instance.Text = "";
                    Db.SubscriptionParts.Context.SubmitChanges();
                }
            }
        }
    }
}