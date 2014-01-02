using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPost> BlogPosts
        {
            get
            {
                return Db.BlogPosts;
            }
        }

       

        public bool CreateBlogPost(BlogPost instance, DateTime? AddedDate)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = AddedDate ?? DateTime.Now;
                instance.ChangedDate = DateTime.Now;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.BlogPosts.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.BlogPosts.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;
                Db.BlogPosts.InsertOnSubmit(instance);
                Db.BlogPosts.Context.SubmitChanges();
                instance.Blog.LastUpdate = DateTime.Now;
                Db.BlogPosts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBlogPost(BlogPost instance)
        {
            var cache = Db.BlogPosts.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Header = instance.Header;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.BlogPosts.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.BlogPosts.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                instance.Url = url;
				cache.Content = instance.Content;
				cache.Source = instance.Source;
                instance.ChangedDate = DateTime.Now;
                Db.BlogPosts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBlogPost(int idBlogPost)
        {
            BlogPost instance = Db.BlogPosts.FirstOrDefault(p => p.ID == idBlogPost);
            if (instance != null)
            {
                Db.BlogPosts.DeleteOnSubmit(instance);
                Db.BlogPosts.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}