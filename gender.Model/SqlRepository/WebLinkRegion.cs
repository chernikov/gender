using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkRegion> WebLinkRegions
        {
            get
            {
                return Db.WebLinkRegions;
            }
        }

        public bool UpdateWebLinkRegion(int idWebLink, List<int> regions)
        {
            var webLink = Db.WebLinks.FirstOrDefault(p => p.ID == idWebLink);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (webLink != null)
            {
                //remove others
                var listForDelete = webLink.WebLinkRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = webLink.WebLinkRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.WebLinkRegions.DeleteAllOnSubmit(listForDelete);
                Db.WebLinkRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var WebLinkRegion = new WebLinkRegion
                        {
                            WebLinkID = webLink.ID,
                            RegionID = region.ID
                        };
                        Db.WebLinkRegions.InsertOnSubmit(WebLinkRegion);
                        Db.WebLinkRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }
}