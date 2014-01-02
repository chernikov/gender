using gender.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class SelectController : DefaultController
    {
        public ActionResult SelectEvent(string term)
        {
            var list = Repository.Events;

            var searchList = SearchEngine.Get(term, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Header
                }),
                term = term
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectRegion(string term)
        {
            var list = Repository.Regions;

            var searchList = SearchEngine.Get(term, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectOrganization(string term)
        {
            var list = Repository.Organizations;

            var searchList = SearchEngine.Get(term, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectPerson(string term)
        {
            var list = Repository.Persons;
            var searchList = SearchEngine.Get(term, list);
            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = string.Format("{0} {1} {2}", p.LastName, p.FirstName, p.Patronymic)
                })
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SelectUser(string term)
        {
            var list = Repository.Persons.Where(p => p.UserID.HasValue);
            var searchList = SearchEngine.Get(term, list);
            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.UserID,
                    name = string.Format("{0}: {1} {2}", p.ID, p.FirstName, p.LastName)
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectPublication(string term)
        {
            var list = Repository.Publications;

            var searchList = SearchEngine.Get(term, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Header
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectSubject(string term)
        {
            var list = Repository.Subjects;

            var searchList = SearchEngine.Get(term, list);

            return Json(new
            {
                result = "ok",
                data = searchList.Select(p => new
                {
                    id = p.ID,
                    name = p.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
