using gender.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class ContactController : DefaultController
    {
        //
        // GET: /Admin/Contact/

        public ActionResult AddContact()
        {
            return View("ContactItem", new KeyValuePair<string, ContactView>(
               Guid.NewGuid().ToString("N"),
               new ContactView()));
        }

    }
}
