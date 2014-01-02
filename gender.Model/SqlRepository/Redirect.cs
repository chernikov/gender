using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Redirect> Redirects
        {
            get
            {
                return Db.Redirects;
            }
        }

        public bool CreateRedirect(Redirect instance)
        {
            if (instance.ID == 0)
            {
                Db.Redirects.InsertOnSubmit(instance);
                Db.Redirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateRedirect(Redirect instance)
        {
            var cache = Db.Redirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.OldLink = instance.OldLink;
				cache.NewLink = instance.NewLink;
                cache.IsForum = instance.IsForum;
                Db.Redirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRedirect(int idRedirect)
        {
            Redirect instance = Db.Redirects.FirstOrDefault(p => p.ID == idRedirect);
            if (instance != null)
            {
                Db.Redirects.DeleteOnSubmit(instance);
                Db.Redirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}