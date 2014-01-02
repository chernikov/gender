using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Event> Events
        {
            get
            {
                return Db.Events;
            }
        }

        public bool CreateEvent(Event instance, int? idUser)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;

                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Events.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Events.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;
                Db.Events.InsertOnSubmit(instance);
                Db.Events.Context.SubmitChanges();
                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.Event,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    AddedDate = DateTime.Now,
                    UserID = idUser
                });
                return true;
            }

            return false;
        }

        public bool UpdateEvent(Event instance)
        {
            var cache = Db.Events.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Image = instance.Image;
				cache.Header = instance.Header;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Events.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Events.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
                cache.Teaser = instance.Teaser;
                cache.EventDate = instance.EventDate;
                cache.Year = instance.Year;
                cache.ChangedDate = DateTime.Now;
                Db.Events.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveEvent(int idEvent)
        {
            Event instance = Db.Events.FirstOrDefault(p => p.ID == idEvent);
            if (instance != null)
            {
                foreach (var document in instance.Documents.ToList())
                {
                    document.Event = null;
                    Db.Events.Context.SubmitChanges();
                }
                Db.Events.DeleteOnSubmit(instance);
                Db.Events.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModerateEvent(int idEvent)
        {
            var instance = Db.Events.FirstOrDefault(p => p.ID == idEvent);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.Events.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}