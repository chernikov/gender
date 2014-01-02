using gender.Global;
using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace gender.Areas.Admin.Controllers
{
    public class RegionController : AdminController
    {
        public ActionResult Index()
        {
            var list = Repository.Regions.Where(p => p.ParentID == null).OrderBy(p => p.OrderBy).ToList();
            return View(list);
        }

        public ActionResult SubRegion(int id)
        {
            var list = Repository.Regions.Where(p => p.ParentID == id).OrderBy(p => p.OrderBy).ToList();
            return View(list);
        }

        public ActionResult Create(int id = 0)
        {
            var regionView = new RegionView();
            if (id > 0)
            {
                regionView.ParentID = id;
            }
            return View("Edit", regionView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var region = Repository.Regions.FirstOrDefault(p => p.ID == id);

            if (region != null)
            {
                var regionView = (RegionView)ModelMapper.Map(region, typeof(Region), typeof(RegionView));
                return View(regionView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RegionView regionView)
        {
            if (ModelState.IsValid)
            {
                var region = (Region)ModelMapper.Map(regionView, typeof(RegionView), typeof(Region));

                if (region.ID == 0)
                {
                    Repository.CreateRegion(region);
                }
                else
                {
                    Repository.UpdateRegion(region);
                }
                return RedirectToAction("Index");
            }
            return View(regionView);
        }

        public ActionResult Delete(int id)
        {
            var region = Repository.Regions.FirstOrDefault(p => p.ID == id);
            if (region != null)
            {
                Repository.RemoveRegion(region.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AjaxRegionOrder(int id, int replaceTo)
        {
            if (Repository.MoveRegion(id, replaceTo))
            {
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public JsonResult AjaxRegionMove(int id, int moveTo)
        {
            if (Repository.ChangeParentRegion(id, moveTo))
            {
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult UpdateRegions()
        {
            var list = new List<string>();
            foreach (var region in Repository.Regions)
            {
                try
                {
                    var htmlName = HttpUtility.UrlEncode(region.Name);

                    region.Map = string.Format("<iframe width=\"100%\" height=\"350\" frameborder=\"0\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"https://maps.google.ru/maps?q={0}&amp;ie=UTF8&amp;hq=&amp;hnear={0}&amp;t=m&amp;output=embed\"></iframe>", htmlName);
                    htmlName = htmlName.Replace("+", "_");
                    region.Link = "http://ru.wikipedia.org/wiki/" + htmlName;
                    region.Description = Wikipedia.GetFirstParagraph(htmlName).StripTags();
                    Repository.UpdateRegion(region);
                }
                catch (Exception ex)
                {
                    list.Add(region.Name);
                }
            }

            return View(list);
            
        }

        public ActionResult UpdateHasEntry()
        {
            Repository.UpdateRegionsHasEntry();
            return Content("OK");
        }
    }
}
