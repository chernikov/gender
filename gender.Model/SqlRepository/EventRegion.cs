using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventRegion> EventRegions
        {
            get
            {
                return Db.EventRegions;
            }
        }

        public bool UpdateEventRegion(int idEvent, List<int> regions)
        {
            var @event = Db.Events.FirstOrDefault(p => p.ID == idEvent);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (@event != null)
            {
                //remove others
                var listForDelete = @event.EventRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = @event.EventRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                Db.EventRegions.DeleteAllOnSubmit(listForDelete);
                Db.EventRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var EventRegion = new EventRegion
                        {
                            EventID = @event.ID,
                            RegionID = region.ID
                        };
                        Db.EventRegions.InsertOnSubmit(EventRegion);
                        Db.EventRegions.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }

    }
}