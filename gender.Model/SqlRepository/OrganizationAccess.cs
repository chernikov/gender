using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<OrganizationAccess> OrganizationAccesses
        {
            get
            {
                return Db.OrganizationAccesses;
            }
        }

        public bool CreateOrganizationAccess(OrganizationAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.OrganizationAccesses.InsertOnSubmit(instance);
                Db.OrganizationAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateOrganizationAccess(OrganizationAccess instance)
        {
            var cache = Db.OrganizationAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.OrganizationID = instance.OrganizationID;
				cache.UserID = instance.UserID;
                Db.OrganizationAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveOrganizationAccess(int idOrganizationAccess)
        {
            OrganizationAccess instance = Db.OrganizationAccesses.FirstOrDefault(p => p.ID == idOrganizationAccess);
            if (instance != null)
            {
                Db.OrganizationAccesses.DeleteOnSubmit(instance);
                Db.OrganizationAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}