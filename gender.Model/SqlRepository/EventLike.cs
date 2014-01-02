using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventLike> EventLikes
        {
            get
            {
                return Db.EventLikes;
            }
        }

        public bool CreateEventLike(EventLike instance)
        {
            if (instance.ID == 0)
            {
                Db.EventLikes.InsertOnSubmit(instance);
                Db.EventLikes.Context.SubmitChanges();
                RecalculateEventLikes(instance.EventID);
                return true;
            }

            return false;
        }

        public bool RemoveEventLike(int idEventLike)
        {
            var instance = Db.EventLikes.FirstOrDefault(p => p.ID == idEventLike);
            if (instance != null)
            {
                var eventID = instance.EventID;

                Db.EventLikes.DeleteOnSubmit(instance);
                Db.EventLikes.Context.SubmitChanges();
                RecalculateEventLikes(eventID);

                return true;
            }
            return false;
        }

        private void RecalculateEventLikes(int eventID)
        {
            var cache = Db.Events.FirstOrDefault(p => p.ID == eventID);

            if (cache != null)
            {
                var list = Db.EventLikes.Where(p => p.EventID == eventID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.EventLikes.Context.SubmitChanges();
            }
        }
    }
}