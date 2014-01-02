using gender.Models.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class SearchController : DefaultController
    {
        public ActionResult Index(string searchString)
        {
            var searchResult = new SearchResult();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchResult.Fill(Repository, searchString);
            }
            return View(searchResult);
        }
    }
}
