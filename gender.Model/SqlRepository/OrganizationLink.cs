using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<OrganizationLink> OrganizationLinks
        {
            get
            {
                return Db.OrganizationLinks;
            }
        }

        public bool CreateOrganizationLink(OrganizationLink instance)
        {
            if (instance.ID == 0)
            {
                Db.OrganizationLinks.InsertOnSubmit(instance);
                Db.OrganizationLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearOrganizationLinks(int organizationID)
        {
            var listForDelete = Db.OrganizationLinks.Where(p => p.OrganizationID == organizationID);

            var linksForDelete = listForDelete.Select(p => p.Link);

            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.OrganizationLinks.DeleteAllOnSubmit(listForDelete);
            Db.OrganizationLinks.Context.SubmitChanges();
            return true;
        }

    }
}