using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;


namespace gender.Areas.Admin.Controllers
{ 
    public class RedirectController : AdminController
    {
		public ActionResult Index(int page = 1)
        {
			var list = Repository.Redirects;
			var data = new PageableData<Redirect>();
			data.Init(list, page, "Index");
			return View(data);
		}

		public ActionResult Create() 
		{
			var redirectView = new RedirectView();
			return View("Edit", redirectView);
		}

		[HttpGet]
		public ActionResult Edit(int id) 
		{
			var  redirect = Repository.Redirects.FirstOrDefault(p => p.ID == id); 

			if (redirect != null) {
				var redirectView = (RedirectView)ModelMapper.Map(redirect, typeof(Redirect), typeof(RedirectView));
				return View(redirectView);
			}
			return RedirectToNotFoundPage;
		}

		[HttpPost]
		public ActionResult Edit(RedirectView redirectView)
        {
            if (ModelState.IsValid)
            {
                var redirect = (Redirect)ModelMapper.Map(redirectView, typeof(RedirectView), typeof(Redirect));
                if (redirect.ID == 0)
                {
                    Repository.CreateRedirect(redirect);
                }
                else
                {
                    Repository.UpdateRedirect(redirect);
                }
                return RedirectToAction("Index");
            }
            return View(redirectView);
        }

        public ActionResult Delete(int id)
        {
            var redirect = Repository.Redirects.FirstOrDefault(p => p.ID == id);
            if (redirect != null)
            {
                    Repository.RemoveRedirect(redirect.ID);
            }
			return RedirectToAction("Index");
        }
	}
}