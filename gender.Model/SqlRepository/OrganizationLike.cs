using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<OrganizationLike> OrganizationLikes
        {
            get
            {
                return Db.OrganizationLikes;
            }
        }

        public bool CreateOrganizationLike(OrganizationLike instance)
        {
            if (instance.ID == 0)
            {
                Db.OrganizationLikes.InsertOnSubmit(instance);
                Db.OrganizationLikes.Context.SubmitChanges();
                RecalculateOrganizationLikes(instance.OrganizationID);
                return true;
            }

            return false;
        }

        public bool RemoveOrganizationLike(int idOrganizationLike)
        {
            var instance = Db.OrganizationLikes.FirstOrDefault(p => p.ID == idOrganizationLike);
            if (instance != null)
            {
                var organizationID = instance.OrganizationID;

                Db.OrganizationLikes.DeleteOnSubmit(instance);
                Db.OrganizationLikes.Context.SubmitChanges();
                RecalculateOrganizationLikes(organizationID);

                return true;
            }
            return false;
        }

        private void RecalculateOrganizationLikes(int organizationID)
        {
            var cache = Db.Organizations.FirstOrDefault(p => p.ID == organizationID);

            if (cache != null)
            {
                var list = Db.OrganizationLikes.Where(p => p.OrganizationID == organizationID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.OrganizationLikes.Context.SubmitChanges();
            }
        }
    }
}