using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationRegion> PublicationRegions
        {
            get
            {
                return Db.PublicationRegions;
            }
        }

        public bool UpdatePublicationRegion(int idPublication, List<int> regions)
        {
            var publication = Db.Publications.FirstOrDefault(p => p.ID == idPublication);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (publication != null)
            {
                //remove others
                var listForDelete = publication.PublicationRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = publication.PublicationRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.PublicationRegions.DeleteAllOnSubmit(listForDelete);
                Db.PublicationRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var publicationRegion = new PublicationRegion
                        {
                            PublicationID = publication.ID,
                            RegionID = region.ID
                        };
                        Db.PublicationRegions.InsertOnSubmit(publicationRegion);
                        Db.PublicationRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }
}