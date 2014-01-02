using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;


namespace gender.Areas.Admin.Controllers
{
    public class ArticleController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Articles;
            var data = new PageableData<Article>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var articleView = new ArticleView();
            return View("Edit", articleView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var article = Repository.Articles.FirstOrDefault(p => p.ID == id);

            if (article != null)
            {
                var articleView = (ArticleView)ModelMapper.Map(article, typeof(Article), typeof(ArticleView));
                return View(articleView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(ArticleView articleView)
        {
            if (ModelState.IsValid)
            {
                var article = (Article)ModelMapper.Map(articleView, typeof(ArticleView), typeof(Article));
                if (article.ID == 0)
                {
                    Repository.CreateArticle(article, CurrentUser.ID);
                }
                else
                {
                    Repository.UpdateArticle(article);
                }

                Repository.UpdateArticleSubject(article.ID, articleView.SubjectList);

                return RedirectToAction("Index");
            }
            return View(articleView);
        }

        public ActionResult Delete(int id)
        {
            var article = Repository.Articles.FirstOrDefault(p => p.ID == id);
            if (article != null)
            {
                Repository.RemoveArticle(article.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.ArticleRecordRedirects.Where(p => p.ArticleID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(ArticleRecordRedirect articleRecordRedirect)
        {
            Repository.CreateArticleRecordRedirect(articleRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var articleRecordRedirect = Repository.ArticleRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (articleRecordRedirect != null) 
            {
                Repository.RemoveRecordRedirect(articleRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }
    }
}