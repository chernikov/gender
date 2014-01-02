using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class StringController : DefaultController
    {
        //
        // GET: /Default/String/

        public ActionResult Index()
        {
            var sb = new StringBuilder();

            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
            {
                sb.AppendLine(connectionString.ConnectionString);
            }
            return Content(sb.ToString());
        }

    }
}
