using gender.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class ErrorController : BaseController
    {
        #region Http404

        public ActionResult Http404()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("Http404");
        }

        #endregion


    }
}
