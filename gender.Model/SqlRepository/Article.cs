using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Article> Articles
        {
            get
            {
                return Db.Articles;
            }
        }

        public bool CreateArticle(Article instance, int? idUser = null)
        {
            if (instance.ID == 0)
            {
                Db.Articles.InsertOnSubmit(instance);
                Db.Articles.Context.SubmitChanges();

                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.Article,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    AddedDate = DateTime.Now,
                    UserID = idUser
                });
                return true;
            }
            return false;
        }

        public bool UpdateArticle(Article instance)
        {
            var cache = Db.Articles.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Url = instance.Url;
				cache.Header = instance.Header;
				cache.Text = instance.Text;
                Db.Articles.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveArticle(int idArticle)
        {
            Article instance = Db.Articles.FirstOrDefault(p => p.ID == idArticle);
            if (instance != null)
            {
                Db.Articles.DeleteOnSubmit(instance);
                Db.Articles.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}