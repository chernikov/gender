using gender.Tools;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Document> Documents
        {
            get
            {
                return Db.Documents;
            }
        }

        public bool CreateDocument(Document instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Documents.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Documents.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;
                Db.Documents.InsertOnSubmit(instance);
                Db.Documents.Context.SubmitChanges();
                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.Document,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    AddedDate = DateTime.Now,
                    UserID = instance.UserID
                });
                return true;
            }

            return false;
        }

        public bool UpdateDocument(Document instance)
        {
            var cache = Db.Documents.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.EventID = instance.EventID;
				cache.Header = instance.Header;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Documents.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Documents.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
				cache.Teaser = instance.Teaser;
				cache.Content = instance.Content;
                cache.ChangedDate = DateTime.Now;
                Db.Documents.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveDocument(int idDocument)
        {
            var instance = Db.Documents.FirstOrDefault(p => p.ID == idDocument);
            if (instance != null)
            {
                Db.Documents.DeleteOnSubmit(instance);
                Db.Documents.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModerateDocument(int idDocument)
        {
            var instance = Db.Documents.FirstOrDefault(p => p.ID == idDocument);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.Documents.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}