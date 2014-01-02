using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkAccess> WebLinkAccesses
        {
            get
            {
                return Db.WebLinkAccesses;
            }
        }

        public bool CreateWebLinkAccess(WebLinkAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.WebLinkAccesses.InsertOnSubmit(instance);
                Db.WebLinkAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateWebLinkAccess(WebLinkAccess instance)
        {
            var cache = Db.WebLinkAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.WebLinkID = instance.WebLinkID;
				cache.UserID = instance.UserID;
                Db.WebLinkAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveWebLinkAccess(int idWebLinkAccess)
        {
            WebLinkAccess instance = Db.WebLinkAccesses.FirstOrDefault(p => p.ID == idWebLinkAccess);
            if (instance != null)
            {
                Db.WebLinkAccesses.DeleteOnSubmit(instance);
                Db.WebLinkAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}