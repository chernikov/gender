using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class RegionController : DefaultController
    {
        public ActionResult Index()
        {
            var list = Repository.Regions.Where(p => p.ParentID == null).ToList();

            return View(list);
        }

        //список всех каталогов 
        public ActionResult Item(string path = null)
        {
            if (path != null)
            {
                var arr = path.Split('/');
                Region region = null;
                foreach (var urlInner in arr)
                {
                    var lastCatalog = region;
                    if (region != null)
                    {
                        region = Repository.Regions.FirstOrDefault(p => string.Compare(p.Url, urlInner, StringComparison.OrdinalIgnoreCase) == 0 && p.ParentID == region.ID);
                    }
                    else
                    {
                        region = Repository.Regions.FirstOrDefault(p => string.Compare(p.Url, urlInner, StringComparison.OrdinalIgnoreCase) == 0 && p.ParentID == null);
                    }

                    //такого каталога нет
                    if (region == null)
                    {
                        return RedirectToNotFoundPage;
                    }
                }
                if (region != null)
                {
                    return View(region);
                }
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Other(int id)
        {
            var region = Repository.Regions.FirstOrDefault(p => p.ID == id);

            if (region.ID == 1 || region.ParentID == 1 || (region.Parent != null && region.Parent.ParentID == 1))
            {
                return View("World");
            }
            return View("Russia");
        }

        public ActionResult List(int id, int type)
        {
            ViewBag.Type = type;
            var region =Repository.Regions.FirstOrDefault(p => p.ID == id);
            return View(region);
        }
    }
}
