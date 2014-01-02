using gender.Global;
using gender.Model;
using gender.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace gender.Areas.Admin.Controllers
{
    public class SubjectController : AdminController
    {
        public ActionResult Index()
        {
            var list = Repository.Subjects.Where(p => p.ParentID == null).OrderBy(p => p.OrderBy).ToList();
            return View(list);
        }

        public ActionResult SubSubject(int id)
        {
            var list = Repository.Subjects.Where(p => p.ParentID == id).OrderBy(p => p.OrderBy).ToList();
            return View(list);
        }

        public ActionResult Create(int id = 0)
        {
            var subjectView = new SubjectView();
            if (id > 0)
            {
                subjectView.ParentID = id;
            }
            return View("Edit", subjectView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);

            if (subject != null)
            {
                var subjectView = (SubjectView)ModelMapper.Map(subject, typeof(Subject), typeof(SubjectView));
                return View(subjectView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SubjectView subjectView)
        {
            if (ModelState.IsValid)
            {
                var subject = (Subject)ModelMapper.Map(subjectView, typeof(SubjectView), typeof(Subject));

                if (subject.ID == 0)
                {
                    Repository.CreateSubject(subject);
                }
                else
                {
                    Repository.UpdateSubject(subject);
                }
                return RedirectToAction("Index");
            }
            return View(subjectView);
        }

        public ActionResult Delete(int id)
        {
            var subject = Repository.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                Repository.RemoveSubject(subject.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AjaxSubjectOrder(int id, int replaceTo)
        {
            if (Repository.MoveSubject(id, replaceTo))
            {
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public JsonResult AjaxSubjectMove(int id, int moveTo)
        {
            if (Repository.ChangeParentSubject(id, moveTo))
            {
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }
    }
}
