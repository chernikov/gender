using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostSubscription> BlogPostSubscriptions
        {
            get
            {
                return Db.BlogPostSubscriptions;
            }
        }

        public bool CreateBlogPostSubscription(BlogPostSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.BlogPostSubscriptions.InsertOnSubmit(instance);
                Db.BlogPostSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBlogPostSubscription(BlogPostSubscription instance)
        {
            var cache = Db.BlogPostSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.BlogPostID = instance.BlogPostID;
				cache.UserID = instance.UserID;
                Db.BlogPostSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBlogPostSubscription(int idBlogPostSubscription)
        {
            BlogPostSubscription instance = Db.BlogPostSubscriptions.FirstOrDefault(p => p.ID == idBlogPostSubscription);
            if (instance != null)
            {
                Db.BlogPostSubscriptions.DeleteOnSubmit(instance);
                Db.BlogPostSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}