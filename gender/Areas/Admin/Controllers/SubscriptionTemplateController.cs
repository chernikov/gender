using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;


namespace gender.Areas.Admin.Controllers
{ 
    public class SubscriptionTemplateController : AdminController
    {
		public ActionResult Index()
        {
			var list = Repository.SubscriptionTemplates.ToList();
			return View(list);
		}

		[HttpGet]
		public ActionResult Edit(int id) 
		{
			var  subscriptiontemplate = Repository.SubscriptionTemplates.FirstOrDefault(p => p.ID == id); 

			if (subscriptiontemplate != null) 
            {
				var subscriptiontemplateView = (SubscriptionTemplateView)ModelMapper.Map(subscriptiontemplate, typeof(SubscriptionTemplate), typeof(SubscriptionTemplateView));
				return View(subscriptiontemplateView);
			}
			return RedirectToNotFoundPage;
		}

		[HttpPost]
		public ActionResult Edit(SubscriptionTemplateView subscriptiontemplateView)
        {
            var dbTemplate = Repository.SubscriptionTemplates.FirstOrDefault(p => p.ID == subscriptiontemplateView.ID);
            if (dbTemplate != null)
            {
                try
                {
                    var stringArr = new string[dbTemplate.CountParameters];
                    for (int i = 0; i < stringArr.Count(); i++)
                    {
                        stringArr[i] = "PARAM";
                    }
                    var result = string.Format(subscriptiontemplateView.Template, stringArr);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Template", "ћаксимальное количество параметров должно быть " + dbTemplate.CountParameters);
                }
            }
            if (ModelState.IsValid)
            {
                var subscriptiontemplate = (SubscriptionTemplate)ModelMapper.Map(subscriptiontemplateView, typeof(SubscriptionTemplateView), typeof(SubscriptionTemplate));
                if (subscriptiontemplate.ID == 0)
                {
                    Repository.CreateSubscriptionTemplate(subscriptiontemplate);
                }
                else
                {
                    Repository.UpdateSubscriptionTemplate(subscriptiontemplate);
                }
                return RedirectToAction("Index");
            }
            return View(subscriptiontemplateView);
        }

        public ActionResult Delete(int id)
        {
            var subscriptiontemplate = Repository.SubscriptionTemplates.FirstOrDefault(p => p.ID == id);
            if (subscriptiontemplate != null)
            {
                    Repository.RemoveSubscriptionTemplate(subscriptiontemplate.ID);
            }
			return RedirectToAction("Index");
        }
	}
}