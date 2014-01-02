using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostComment> BlogPostComments
        {
            get
            {
                return Db.BlogPostComments;
            }
        }

        public bool CreateBlogPostComment(BlogPostComment instance)
        {
            if (instance.ID == 0)
            {
                Db.BlogPostComments.InsertOnSubmit(instance);
                Db.BlogPostComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBlogPostComment(BlogPostComment instance)
        {
            var cache = Db.BlogPostComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.BlogPostID = instance.BlogPostID;
				cache.CommentID = instance.CommentID;
                Db.BlogPostComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBlogPostComment(int idBlogPostComment)
        {
            BlogPostComment instance = Db.BlogPostComments.FirstOrDefault(p => p.ID == idBlogPostComment);
            if (instance != null)
            {
                Db.BlogPostComments.DeleteOnSubmit(instance);
                Db.BlogPostComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}