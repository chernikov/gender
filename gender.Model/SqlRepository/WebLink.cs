using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLink> WebLinks
        {
            get
            {
                return Db.WebLinks;
            }
        }

        public bool CreateWebLink(WebLink instance, int? idUser = null)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;

                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.WebLinks.Any(p => string.Compare(p.SiteUrl, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.WebLinks.Any(p => string.Compare(p.SiteUrl, url, true) == 0);
                    i++;
                }
                instance.SiteUrl = url;
                Db.WebLinks.InsertOnSubmit(instance);
                Db.WebLinks.Context.SubmitChanges();

                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.WebLink,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    AddedDate = DateTime.Now,
                    UserID = idUser
                });
                return true;
            }

            return false;
        }

        public bool UpdateWebLink(WebLink instance)
        {
            var cache = Db.WebLinks.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Name = instance.Name;
                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.WebLinks.Any(p => string.Compare(p.SiteUrl, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.WebLinks.Any(p => string.Compare(p.SiteUrl, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.SiteUrl = url;
				cache.Screenshot = instance.Screenshot;
				cache.Url = instance.Url;
				cache.ReservedUrl = instance.ReservedUrl;
                cache.Description = instance.Description;
                cache.RSS = instance.RSS;
                cache.ChangedDate = DateTime.Now;
                Db.WebLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveWebLink(int idWebLink)
        {
            WebLink instance = Db.WebLinks.FirstOrDefault(p => p.ID == idWebLink);
            if (instance != null)
            {
                Db.WebLinks.DeleteOnSubmit(instance);
                Db.WebLinks.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModerateWebLink(int idWebLink)
        {
            var instance = Db.WebLinks.FirstOrDefault(p => p.ID == idWebLink);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.WebLinks.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}