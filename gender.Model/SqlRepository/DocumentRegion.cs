using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentRegion> DocumentRegions
        {
            get
            {
                return Db.DocumentRegions;
            }
        }

        public bool UpdateDocumentRegion(int idDocument, List<int> regions)
        {
            var document = Db.Documents.FirstOrDefault(p => p.ID == idDocument);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (document != null)
            {
                //remove others
                var listForDelete = document.DocumentRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = document.DocumentRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.DocumentRegions.DeleteAllOnSubmit(listForDelete);
                Db.DocumentRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var DocumentRegion = new DocumentRegion
                        {
                            DocumentID = document.ID,
                            RegionID = region.ID
                        };
                        Db.DocumentRegions.InsertOnSubmit(DocumentRegion);
                        Db.DocumentRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }
}