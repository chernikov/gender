using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;


namespace gender.Areas.Admin.Controllers
{ 
    public class BlogController : AdminController
    {
		public ActionResult Index(int page = 1)
        {
			var list = Repository.Blogs.OrderByDescending(p => p.LastUpdate);
			var data = new PageableData<Blog>();
			data.Init(list, page, "Index");
			return View(data);
		}
	}
}