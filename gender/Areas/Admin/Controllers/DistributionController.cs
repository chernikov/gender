using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;


namespace gender.Areas.Admin.Controllers
{
    public class DistributionController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            IQueryable<Distribution> list = null;
            list = Repository.Distributions.OrderByDescending(p => p.ID);
            var data = new PageableData<Distribution>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var distributionView = new DistributionView();
            return View("Edit", distributionView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var distribution = Repository.Distributions.FirstOrDefault(p => p.ID == id);

            if (distribution != null)
            {
                var distributionView = (DistributionView)ModelMapper.Map(distribution, typeof(Distribution), typeof(DistributionView));
                return View(distributionView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(DistributionView distributionView)
        {
            if (ModelState.IsValid)
            {
                var distribution = (Distribution)ModelMapper.Map(distributionView, typeof(DistributionView), typeof(Distribution));

                if (distribution.ID == 0)
                {

                    Repository.CreateDistribution(distribution);
                }
                else
                {
                    Repository.UpdateDistribution(distribution);
                }
                return RedirectToAction("Index");
            }
            return View(distributionView);
        }

        public ActionResult Delete(int id)
        {
            var distribution = Repository.Distributions.FirstOrDefault(p => p.ID == id);
            if (distribution != null)
            {
                Repository.RemoveDistribution(distribution.ID);
                return RedirectToAction("Index");
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Start(int id)
        {
            var distribution = Repository.Distributions.FirstOrDefault(p => p.ID == id);
            if (distribution != null && !distribution.IsStart)
            {
                foreach (var user in Repository.Users.Where(p => p.UserEmails.Any()))
                {
                    var mail = new Mail()
                    {
                        UserID = user.ID,
                        Email = user.PrimaryEmail.Email,
                        DistributionID = distribution.ID
                    };

                    mail.Subject = distribution.Subject;
                    mail.Subject = mail.Subject.Replace("%username%", user.Person.FullName);
                    mail.Body = distribution.Body.Replace("%username%", user.Person.FullName);
                    mail.Body = mail.Body.Replace("<img src=\"/Content", "<img src=\"" + HostName + "/Content");
                    mail.Body = mail.Body.Replace("<a href=\"/", "<a href=\"" + HostName + "/");

                    Repository.PushMail(mail);
                }
                Repository.StartDistribution(distribution.ID);
                return RedirectToAction("Index");
            }
            return RedirectToNotFoundPage;
        }
    }
}