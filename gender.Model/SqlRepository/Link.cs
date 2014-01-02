using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Link> Links
        {
            get
            {
                return Db.Links;
            }
        }

        public bool CreateLink(Link instance)
        {
            if (instance.ID == 0)
            {
                Db.Links.InsertOnSubmit(instance);
                Db.Links.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateLink(Link instance)
        {
            var cache = Db.Links.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Url = instance.Url;
				cache.Icon = instance.Icon;
				cache.Title = instance.Title;
                Db.Links.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveLink(int idLink)
        {
            Link instance = Db.Links.FirstOrDefault(p => p.ID == idLink);
            if (instance != null)
            {
                Db.Links.DeleteOnSubmit(instance);
                Db.Links.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}