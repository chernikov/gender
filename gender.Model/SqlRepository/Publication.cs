using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {

        public IQueryable<Publication> Publications
        {
            get
            {
                return Db.Publications;
            }
        }

        public bool CreatePublication(Publication instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;
                
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Publications.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Persons.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;
                instance.Content = instance.Content ?? "";
                instance.Teaser = instance.Teaser ?? "";
                Db.Publications.InsertOnSubmit(instance);
                Db.Publications.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePublication(Publication instance, out bool fillText, int? UserID = null)
        {
            fillText = false;
            var cache = Db.Publications.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.ParentID = instance.ParentID;
                cache.Cover = instance.Cover;
                cache.Header = instance.Header;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Publications.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Publications.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
                cache.Bibliographic = instance.Bibliographic;
                cache.Year = instance.Year;
                cache.Type = instance.Type;
                cache.Teaser = instance.Teaser ?? "";
                if (string.IsNullOrWhiteSpace(cache.Content) && !string.IsNullOrWhiteSpace(instance.Content))
                {
                    fillText = true;
                    CreateUpdateRecord(new UpdateRecord()
                    {
                        ResourceID = cache.ID,
                        MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                        Type = (int)UpdateRecord.TypeEnum.NewText,
                        AddedDate = DateTime.Now,
                        UserID = UserID
                    });
                }
                cache.Content = instance.Content ?? "";
                cache.ChangedDate = DateTime.Now;
                Db.Publications.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool RemovePublication(int idPublication)
        {
            Publication instance = Db.Publications.FirstOrDefault(p => p.ID == idPublication);
            if (instance != null)
            {
                if (instance.Publications.Any())
                {
                    foreach (var publication in instance.Publications.ToList())
                    {
                        publication.ParentID = null;
                    }
                }
                Db.Publications.Context.SubmitChanges();
                Db.Publications.DeleteOnSubmit(instance);
                Db.Publications.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModeratePublication(int idPublication)
        {
            var instance = Db.Publications.FirstOrDefault(p => p.ID == idPublication);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.Publications.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}