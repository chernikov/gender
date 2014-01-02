using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogParser> BlogParsers
        {
            get
            {
                return Db.BlogParsers;
            }
        }

        public bool CreateBlogParser(BlogParser instance)
        {
            if (instance.ID == 0)
            {
                Db.BlogParsers.InsertOnSubmit(instance);
                Db.BlogParsers.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateBlogParser(BlogParser instance)
        {
            var cache = Db.BlogParsers.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.BlogID = instance.BlogID;
				cache.Link = instance.Link;
				cache.Type = instance.Type;
                Db.BlogParsers.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveBlogParser(int idBlogParser)
        {
            BlogParser instance = Db.BlogParsers.FirstOrDefault(p => p.ID == idBlogParser);
            if (instance != null)
            {
                Db.BlogParsers.DeleteOnSubmit(instance);
                Db.BlogParsers.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}