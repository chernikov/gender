using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PersonRegion> PersonRegions
        {
            get
            {
                return Db.PersonRegions;
            }
        }

        public bool UpdatePersonRegion(int idPerson, List<int> regions)
        {
            var person = Db.Persons.FirstOrDefault(p => p.ID == idPerson);
            if (regions == null)
            {
                regions = new List<int>();
            }

            if (person != null)
            {
                //remove others
                var listForDelete = person.PersonRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = person.PersonRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.PersonRegions.DeleteAllOnSubmit(listForDelete);
                Db.PersonRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var personRegion = new PersonRegion
                        {
                            PersonID = person.ID,
                            RegionID = region.ID
                        };
                        Db.PersonRegions.InsertOnSubmit(personRegion);
                        Db.PersonRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }
}