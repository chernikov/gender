using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class PageController : DefaultController
    {
        public ActionResult Item(string url)
        {
            var page = Repository.Pages.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);

            if (page != null)
            {
                return View(page);
            }
            return RedirectToNotFoundPage;
        }

    }
}
