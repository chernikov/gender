using gender.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class UserEmailController : DefaultController
    {
        public ActionResult AddUserEmail()
        {
            return View("UserEmailItem", new KeyValuePair<string, UserEmailView>(
               Guid.NewGuid().ToString("N"),
               new UserEmailView()));
        }

    }
}
