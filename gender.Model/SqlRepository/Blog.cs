using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Blog> Blogs
        {
            get
            {
                return Db.Blogs;
            }
        }

        public bool CreateBlog(Blog instance)
        {
            if (instance.ID == 0)
            {
                instance.LastUpdate = DateTime.Now;
                Db.Blogs.InsertOnSubmit(instance);
                Db.Blogs.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBlog(Blog instance)
        {
            var cache = Db.Blogs.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.LastUpdate = instance.LastUpdate;
                Db.Blogs.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBlog(int idBlog)
        {
            Blog instance = Db.Blogs.FirstOrDefault(p => p.ID == idBlog);
            if (instance != null)
            {
                Db.Blogs.DeleteOnSubmit(instance);
                Db.Blogs.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}