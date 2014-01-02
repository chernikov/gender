using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationAccess> PublicationAccesses
        {
            get
            {
                return Db.PublicationAccesses;
            }
        }

        public bool CreatePublicationAccess(PublicationAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationAccesses.InsertOnSubmit(instance);
                Db.PublicationAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePublicationAccess(PublicationAccess instance)
        {
            var cache = Db.PublicationAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.PublicationID = instance.PublicationID;
				cache.UserID = instance.UserID;
                Db.PublicationAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePublicationAccess(int idPublicationAccess)
        {
            PublicationAccess instance = Db.PublicationAccesses.FirstOrDefault(p => p.ID == idPublicationAccess);
            if (instance != null)
            {
                Db.PublicationAccesses.DeleteOnSubmit(instance);
                Db.PublicationAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}