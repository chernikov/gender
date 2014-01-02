using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ArticleRecordRedirect> ArticleRecordRedirects
        {
            get
            {
                return Db.ArticleRecordRedirects;
            }
        }

        public bool CreateArticleRecordRedirect(ArticleRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.ArticleRecordRedirects.InsertOnSubmit(instance);
                Db.ArticleRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateArticleRecordRedirect(ArticleRecordRedirect instance)
        {
            var cache = Db.ArticleRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ArticleID = instance.ArticleID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.ArticleRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveArticleRecordRedirect(int idArticleRecordRedirect)
        {
            ArticleRecordRedirect instance = Db.ArticleRecordRedirects.FirstOrDefault(p => p.ID == idArticleRecordRedirect);
            if (instance != null)
            {
                Db.ArticleRecordRedirects.DeleteOnSubmit(instance);
                Db.ArticleRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}