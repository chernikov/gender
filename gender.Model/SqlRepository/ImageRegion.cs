using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageRegion> ImageRegions
        {
            get
            {
                return Db.ImageRegions;
            }
        }

       public bool UpdateImageRegion(int idImage, List<int> regions)
        {
            var image = Db.Images.FirstOrDefault(p => p.ID == idImage);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (image != null)
            {
                //remove others
                var listForDelete = image.ImageRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = image.ImageRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.ImageRegions.DeleteAllOnSubmit(listForDelete);
                Db.ImageRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var imageRegion = new ImageRegion
                        {
                            ImageID = image.ID,
                            RegionID = region.ID
                        };
                        Db.ImageRegions.InsertOnSubmit(imageRegion);
                        Db.ImageRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }

}