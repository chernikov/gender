using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Admin.Controllers
{
    public class RecordRedirectController : AdminController
    {

        public ActionResult Index(int page = 1)
        {
            var list = Repository.RecordRedirects.OrderBy(p => p.Url);

            var data = new PageableData<RecordRedirect>();
            data.Init(list, page, "Index");

            return View(data);
        }

        public ActionResult Remove(int id)
        {
            Repository.RemoveRecordRedirect(id);
            return Json(new { result = "ok" });
        }

    }
}
