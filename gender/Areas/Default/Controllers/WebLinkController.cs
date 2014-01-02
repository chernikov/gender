using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class WebLinkController : DefaultController
    {

        public ActionResult Index(int page = 1)
        {
            var list = Repository.WebLinks.Where(p => p.ModeratedDate.HasValue).OrderBy(p => p.Name);
            var data = new PageableData<WebLink>();
            data.Init(list, page, "Index", itemPerPage: 20);
            return View(data);
        }

        public ActionResult Item(string url)
        {
            var item = Repository.WebLinks.FirstOrDefault(p => string.Compare(p.SiteUrl, url, true) == 0);

            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var webLink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);

            if (webLink != null)
            {
                WebLinkSubject webLinkSubject = null;
                if (idSubject.HasValue)
                {
                    webLinkSubject = webLink.WebLinkSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextWebLink = webLink.WebLinkSubjects.FirstOrDefault(p => p.ID > webLinkSubject.ID);
                    if (nextWebLink != null)
                    {
                        webLinkSubject = nextWebLink;
                    }
                    else
                    {
                        webLinkSubject = webLink.WebLinkSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    webLinkSubject = webLink.WebLinkSubjects.FirstOrDefault();
                }
                if (webLinkSubject != null)
                {
                    return View(webLinkSubject.Subject);
                }
            }
            return null;
        }

        public ActionResult Comments(int id)
        {
            var webLink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);
            if (webLink != null)
            {
                return View(webLink);
            }
            return null;
        }

        [HttpPost]
        public ActionResult CreateComment(CommentView commentView)
        {
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
                {
                    var comment = (Comment)ModelMapper.Map(commentView, typeof(CommentView), typeof(Comment));
                    comment.UserID = CurrentUser.ID;
                    comment.ID = 0;
                    Repository.CreateComment(comment);
                    var webLinkComment = new WebLinkComment
                    {
                        WebLinkID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateWebLinkComment(webLinkComment);
                    Subscription.NewComment(Repository, webLinkComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateWebLink())
            {
                var weblinkView = new WebLinkView();
                return View("Edit", weblinkView);
            }

            return RedirectToLoginPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var weblink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);

            if (weblink != null && CurrentUser.CanEdit(weblink))
            {
                var weblinkView = (WebLinkView)ModelMapper.Map(weblink, typeof(WebLink), typeof(WebLinkView));
                return View(weblinkView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(WebLinkView weblinkView)
        {
            if (ModelState.IsValid)
            {
                var weblink = (WebLink)ModelMapper.Map(weblinkView, typeof(WebLinkView), typeof(WebLink));
                if (weblink.ID == 0)
                {
                    Repository.CreateWebLink(weblink, CurrentUser.ID);
                }
                else
                {
                    Repository.UpdateWebLink(weblink);
                }
                if (CurrentUser.Rating >= ModerateRating || CurrentUser.InRoles("admin,moderator") || CurrentUser.InvitedPrivileged)
                {
                    Repository.ModerateWebLink(weblink.ID);
                }
                var newSubjects = Repository.UpdateWebLinkSubject(weblink.ID, weblinkView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && weblinkView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, weblink);
                }
                Repository.UpdateWebLinkRegion(weblink.ID, weblinkView.RegionList);
                if (weblinkView.ID == 0)
                {
                    Subscription.AddMaterial(Repository, newSubjects, weblink, null);
                }

                var newWebLink = Repository.WebLinks.FirstOrDefault(p => p.ID == weblink.ID);
                if (newWebLink != null)
                {
                    return RedirectToAction("Item", new { url = newWebLink.SiteUrl });
                }
                return RedirectToNotFoundPage;
            }
            return View(weblinkView);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var weblink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);
            if (weblink != null && CurrentUser.CanDelete(weblink))
            {
                Repository.RemoveWebLink(weblink.ID);
            }
            return RedirectToAction("Index");
        }

    }
}
