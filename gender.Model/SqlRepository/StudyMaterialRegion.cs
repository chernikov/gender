using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialRegion> StudyMaterialRegions
        {
            get
            {
                return Db.StudyMaterialRegions;
            }
        }

        public bool UpdateStudyMaterialRegion(int idStudyMaterial, List<int> regions)
        {
            var studyMaterial = Db.StudyMaterials.FirstOrDefault(p => p.ID == idStudyMaterial);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (studyMaterial != null)
            {
                //remove others
                var listForDelete = studyMaterial.StudyMaterialRegions.Where(p => !regions.Contains(p.RegionID));
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                var existList = studyMaterial.StudyMaterialRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                Db.StudyMaterialRegions.DeleteAllOnSubmit(listForDelete);
                Db.StudyMaterialRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var StudyMaterialRegion = new StudyMaterialRegion
                        {
                            StudyMaterialID = studyMaterial.ID,
                            RegionID = region.ID
                        };
                        Db.StudyMaterialRegions.InsertOnSubmit(StudyMaterialRegion);
                        Db.StudyMaterialRegions.Context.SubmitChanges();
                    }
                }

                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }

    }
}