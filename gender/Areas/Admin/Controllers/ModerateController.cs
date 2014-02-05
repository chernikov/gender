using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Admin.Controllers
{
    public class ModerateController : AdminController
    {
        public ActionResult Index()
        {
            var list = new List<IModerable>();

            list.AddRange(Repository.Documents.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.Events.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.Images.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.Organizations.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.Persons.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.Publications.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.StudyMaterials.Where(p => !p.ModeratedDate.HasValue).ToList());
            list.AddRange(Repository.WebLinks.Where(p => !p.ModeratedDate.HasValue).ToList());

            return View(list.OrderByDescending(p => p.AddedDate).ToList());
        }

        public ActionResult Count()
        {
            var count = 0;
            count += Repository.Documents.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.Events.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.Images.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.Organizations.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.Persons.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.Publications.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.StudyMaterials.Count(p => !p.ModeratedDate.HasValue);
            count += Repository.WebLinks.Count(p => !p.ModeratedDate.HasValue);


            return View("Count", count);
        }

    }
}
