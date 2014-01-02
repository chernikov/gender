using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Tools;
using gender.Model;

namespace gender.Areas.Default.Controllers
{
    public class ArticleController : DefaultController
    {
        public ActionResult Index()
        {
            var list = Repository.Articles.ToList().OrderBy(p => p.Header.ClearHeader());
            return View(list);
        }


        public ActionResult Item(string url)
        {
            var item = Repository.Articles.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);

            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var article = Repository.Articles.FirstOrDefault(p => p.ID == id);

            if (article != null)
            {
                ArticleSubject articleSubject = null;
                if (idSubject.HasValue)
                {
                    articleSubject = article.ArticleSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextArticle = article.ArticleSubjects.FirstOrDefault(p => p.ID > articleSubject.ID);
                    if (nextArticle != null)
                    {
                        articleSubject = nextArticle;
                    }
                    else
                    {
                        articleSubject = article.ArticleSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    articleSubject = article.ArticleSubjects.FirstOrDefault();
                }
                if (articleSubject != null)
                {
                    return View(articleSubject.Subject);
                }
            }
            return null;
        }
    }
}
