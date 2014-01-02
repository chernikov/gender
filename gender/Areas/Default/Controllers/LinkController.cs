using gender.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Tools;

namespace gender.Areas.Default.Controllers
{
    public class LinkController : DefaultController
    {
        public ActionResult AddLink(int type = 1)
        {
            if (type == 1)
            {
                return View("LinkItem", new KeyValuePair<string, LinkView>(
                   Guid.NewGuid().ToString("N"),
                   new LinkView()));
            }
            return View("ShopLinkItem", new KeyValuePair<string, LinkView>(
            Guid.NewGuid().ToString("N"),
            new LinkView()));
        }

        public ActionResult ProcessUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {

                var filePath = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";

                url.GetFavicon(Server.MapPath(filePath));

                var title = url.GetWebPageTitle();

                return Json(new { result = "ok", data = new { filePath, title } }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = "error" });
        }

        public ActionResult ProcessSimpleUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var filePath = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
                
                url.GetFavicon(Server.MapPath(filePath));
                var uri = new Uri(url);
                var result = uri.Authority;
                result = result.Substring(0, result.LastIndexOf(".")).ToUpper() + result.Substring(result.LastIndexOf("."));

                return Json(new { result = "ok", data = new { filePath, result } }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "error" });
        }
    }
}
