using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostRecordRedirect> BlogPostRecordRedirects
        {
            get
            {
                return Db.BlogPostRecordRedirects;
            }
        }

        public bool CreateBlogPostRecordRedirect(BlogPostRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.BlogPostRecordRedirects.InsertOnSubmit(instance);
                Db.BlogPostRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBlogPostRecordRedirect(BlogPostRecordRedirect instance)
        {
            var cache = Db.BlogPostRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.BlogPostID = instance.BlogPostID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.BlogPostRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBlogPostRecordRedirect(int idBlogPostRecordRedirect)
        {
            BlogPostRecordRedirect instance = Db.BlogPostRecordRedirects.FirstOrDefault(p => p.ID == idBlogPostRecordRedirect);
            if (instance != null)
            {
                Db.BlogPostRecordRedirects.DeleteOnSubmit(instance);
                Db.BlogPostRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}