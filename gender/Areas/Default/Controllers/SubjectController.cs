using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class SubjectController : DefaultController
    {
        public ActionResult Index()
        {
            var list = Repository.Subjects.Where(p => p.ParentID == null).OrderBy(p => p.OrderBy).ToList();
            return View(list);
        }

        //список всех каталогов 
        public ActionResult Item(string path = null)
        {
            if (path != null)
            {
                var arr = path.Split('/');
                Subject subject = null;
                foreach (var urlInner in arr)
                {
                    var lastCatalog = subject;
                    if (subject != null)
                    {
                        subject = Repository.Subjects.FirstOrDefault(p => string.Compare(p.Url, urlInner, StringComparison.OrdinalIgnoreCase) == 0 && p.ParentID == subject.ID);
                    }
                    else
                    {
                        subject = Repository.Subjects.FirstOrDefault(p => string.Compare(p.Url, urlInner, StringComparison.OrdinalIgnoreCase) == 0 && p.ParentID == null);
                    }

                    //такого каталога нет
                    if (subject == null)
                    {
                        return RedirectToNotFoundPage;
                    }
                }
                if (subject != null)
                {
                    return View(subject);
                }
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult PublicationList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.PublicationSubjects.Where(p => p.Publication.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Publication);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult DocumentList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.DocumentSubjects.Where(p => p.Document.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Document);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult ArticleList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.ArticleSubjects.OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Article);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult BlogPostList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.BlogPostSubjects.OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.BlogPost);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult StudyMaterialList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.StudyMaterialSubjects.Where(p => p.StudyMaterial.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.StudyMaterial);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult PersonList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.PersonSubjects.Where(p => p.Person.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Person);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult OrganizationList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.OrganizationSubjects.OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Organization);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult EventList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.EventSubjects.Where(p => p.Event.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Event);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult WebLinkList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.WebLinkSubjects.Where(p => p.WebLink.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.WebLink);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult ImageList(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                ViewBag.Subject = subject;
                var list = subject.ImageSubjects.Where(p => p.Image.ModeratedDate.HasValue).OrderBy(p => Guid.NewGuid()).Take(5).Select(p => p.Image);
                return View(list.ToList());
            }
            return null;
        }

        public ActionResult ToggleSubscription(int id)
        {
            if (CurrentUser != null)
            {
                var subjectSubscription = Repository.SubjectSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.SubjectID == id);

                if (subjectSubscription != null)
                {
                    Repository.RemoveSubjectSubscription(subjectSubscription.ID);
                }
                else
                {
                    subjectSubscription = new SubjectSubscription()
                    {
                        UserID = CurrentUser.ID,
                        SubjectID = id
                    };
                    Repository.CreateSubjectSubscription(subjectSubscription);
                }
            }
            return Json(new {result = "ok"}, JsonRequestBehavior.AllowGet);
        }
    }
}
