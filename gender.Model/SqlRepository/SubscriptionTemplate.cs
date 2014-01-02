using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<SubscriptionTemplate> SubscriptionTemplates
        {
            get
            {
                return Db.SubscriptionTemplates;
            }
        }

        public bool CreateSubscriptionTemplate(SubscriptionTemplate instance)
        {
            if (instance.ID == 0)
            {
                Db.SubscriptionTemplates.InsertOnSubmit(instance);
                Db.SubscriptionTemplates.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateSubscriptionTemplate(SubscriptionTemplate instance)
        {
            var cache = Db.SubscriptionTemplates.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.IsActive = instance.IsActive;
				cache.Template = instance.Template;
                Db.SubscriptionTemplates.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveSubscriptionTemplate(int idSubscriptionTemplate)
        {
            SubscriptionTemplate instance = Db.SubscriptionTemplates.FirstOrDefault(p => p.ID == idSubscriptionTemplate);
            if (instance != null)
            {
                Db.SubscriptionTemplates.DeleteOnSubmit(instance);
                Db.SubscriptionTemplates.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}