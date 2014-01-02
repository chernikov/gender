using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<OrganizationRegion> OrganizationRegions
        {
            get
            {
                return Db.OrganizationRegions;
            }
        }

        public bool UpdateOrganizationRegion(int idOrganization, List<int> regions)
        {
            var organization = Db.Organizations.FirstOrDefault(p => p.ID == idOrganization);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (organization != null)
            {
                //remove others
                var listForDelete = organization.OrganizationRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = organization.OrganizationRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.OrganizationRegions.DeleteAllOnSubmit(listForDelete);
                Db.OrganizationRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var OrganizationRegion = new OrganizationRegion
                        {
                            OrganizationID = organization.ID,
                            RegionID = region.ID
                        };
                        Db.OrganizationRegions.InsertOnSubmit(OrganizationRegion);
                        Db.OrganizationRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }
}