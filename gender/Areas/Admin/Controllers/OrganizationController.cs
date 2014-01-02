using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Global;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{
    public class OrganizationController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Organizations.OrderByDescending(p => p.ID);
            var data = new PageableData<Organization>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var organizationView = new OrganizationView();
            organizationView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
            return View("Edit", organizationView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => p.ID == id);

            if (organization != null)
            {
                var organizationView = (OrganizationView)ModelMapper.Map(organization, typeof(Organization), typeof(OrganizationView));
                return View(organizationView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(OrganizationView organizationView)
        {
            LinksVerification(organizationView.Links);
            if (ModelState.IsValid)
            {
                var organization = (Organization)ModelMapper.Map(organizationView, typeof(OrganizationView), typeof(Organization));
                organization.UserID = CurrentUser.ID;
                if (organization.ID == 0)
                {
                    organization.UserID = CurrentUser.ID;
                    Repository.CreateOrganization(organization);
                }
                else
                {
                    Repository.UpdateOrganization(organization);
                }
                var newSubjects = Repository.UpdateOrganizationSubject(organization.ID, organizationView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && organizationView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, organization);
                } 
                Repository.UpdateOrganizationRegion(organization.ID, organizationView.RegionList);
                Repository.ClearOrganizationContacts(organization.ID);
                if (organizationView.Contacts != null)
                {
                    foreach (var contactView in organizationView.Contacts)
                    {
                        var contact = (Contact)ModelMapper.Map(contactView.Value, typeof(ContactView), typeof(Contact));
                        Repository.CreateContact(contact);

                        var organizationContact = new OrganizationContact()
                        {
                            OrganizationID = organization.ID,
                            ContactID = contact.ID
                        };

                        Repository.CreateOrganizationContact(organizationContact);
                    }
                }
                Repository.ClearOrganizationLinks(organization.ID);
                if (organizationView.Links != null)
                {
                    foreach (var linkView in organizationView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);

                        var organizationLink = new OrganizationLink()
                        {
                            OrganizationID = organization.ID,
                            LinkID = link.ID
                        };

                        Repository.CreateOrganizationLink(organizationLink);
                    }
                }
                if (organizationView.ID == 0)
                {
                    Subscription.AddMaterial(Repository, newSubjects, organization, null);
                }
                return RedirectToAction("Index");
            }
            return View(organizationView);
        }

        public ActionResult Delete(int id)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => p.ID == id);
            if (organization != null)
            {
                Repository.RemoveOrganization(organization.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Access(int id)
        {
            var list = Repository.OrganizationAccesses.Where(p => p.OrganizationID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(OrganizationAccess organizationAccess)
        {
            if (organizationAccess.UserID != 0 && organizationAccess.OrganizationID != 0)
            {
                var exist = Repository.OrganizationAccesses.Any(p => p.OrganizationID == organizationAccess.OrganizationID && p.UserID == organizationAccess.UserID);

                if (!exist)
                {
                    Repository.CreateOrganizationAccess(organizationAccess);
                    Subscription.GiveRight(Repository, organizationAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.OrganizationAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemoveOrganizationAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Add(string name)
        {
            var organization = new Organization()
            {
                Name = name,
                Info = string.Empty,
                UserID = CurrentUser.ID
            };

            Repository.CreateOrganization(organization);

            return Json(new 
            {
                result = "ok",
                data = 
                new {
                    id = organization.ID,
                    name = organization.Name
                }
            });

        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.OrganizationRecordRedirects.Where(p => p.OrganizationID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(OrganizationRecordRedirect organizationRecordRedirect)
        {
            Repository.CreateOrganizationRecordRedirect(organizationRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var organizationRecordRedirect = Repository.OrganizationRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (organizationRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(organizationRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }
    }
}