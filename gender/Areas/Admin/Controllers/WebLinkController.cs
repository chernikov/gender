using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Tools;
using System.IO;


namespace gender.Areas.Admin.Controllers
{
    public class WebLinkController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.WebLinks.OrderByDescending(p => p.ID);
            var data = new PageableData<WebLink>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var weblinkView = new WebLinkView();
            return View("Edit", weblinkView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var weblink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);

            if (weblink != null)
            {
                var weblinkView = (WebLinkView)ModelMapper.Map(weblink, typeof(WebLink), typeof(WebLinkView));
                return View(weblinkView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
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
                Repository.ModerateWebLink(weblink.ID);
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
                return RedirectToAction("Index");
            }
            return View(weblinkView);
        }

        public ActionResult Delete(int id)
        {
            var weblink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);
            if (weblink != null)
            {
                Repository.RemoveWebLink(weblink.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetScreenshot(string url)
        {

            var ms = WebScreenshoter.GetScreenshot(url);
            if (ms != null)
            {
                var path = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".png";
                using (var fs = new FileStream(Server.MapPath(path), FileMode.Create))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return Json(new { result = "ok", path }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Access(int id)
        {
            var list = Repository.WebLinkAccesses.Where(p => p.WebLinkID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(WebLinkAccess webLinkAccess)
        {
            if (webLinkAccess.UserID != 0 && webLinkAccess.WebLinkID != 0)
            {
                var exist = Repository.WebLinkAccesses.Any(p => p.WebLinkID == webLinkAccess.WebLinkID && p.UserID == webLinkAccess.UserID);

                if (!exist)
                {
                    Repository.CreateWebLinkAccess(webLinkAccess);
                    Subscription.GiveRight(Repository, webLinkAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.WebLinkAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemoveWebLinkAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.WebLinkRecordRedirects.Where(p => p.WebLinkID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(WebLinkRecordRedirect webLinkRecordRedirect)
        {
            Repository.CreateWebLinkRecordRedirect(webLinkRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var webLinkRecordRedirect = Repository.WebLinkRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (webLinkRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(webLinkRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModerateWebLink(id);
            return RedirectBack;
        }
    }
}