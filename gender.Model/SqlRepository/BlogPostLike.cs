using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostLike> BlogPostLikes
        {
            get
            {
                return Db.BlogPostLikes;
            }
        }

        public bool CreateBlogPostLike(BlogPostLike instance)
        {
            if (instance.ID == 0)
            {
                Db.BlogPostLikes.InsertOnSubmit(instance);
                Db.BlogPostLikes.Context.SubmitChanges();
                RecalculateBlogPostLikes(instance.BlogPostID);
                return true;
            }

            return false;
        }

        

        public bool RemoveBlogPostLike(int idBlogPostLike)
        {
            BlogPostLike instance = Db.BlogPostLikes.FirstOrDefault(p => p.ID == idBlogPostLike);
            if (instance != null)
            {
                var blogPostId = instance.BlogPostID;
                Db.BlogPostLikes.DeleteOnSubmit(instance);
                Db.BlogPostLikes.Context.SubmitChanges();
                RecalculateBlogPostLikes(blogPostId);
                return true;
            }
            return false;
        }

        private void RecalculateBlogPostLikes(int blogPostId)
        {
            var cache = Db.BlogPosts.FirstOrDefault(p => p.ID == blogPostId);

            if (cache != null)
            {
                var list = Db.BlogPostLikes.Where(p => p.BlogPostID == blogPostId);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.BlogPostLikes.Context.SubmitChanges();
            }
        }
    }
}