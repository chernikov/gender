using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class OrganizationController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int type)
        {
            IQueryable<Organization> list = null;
            switch ((Organization.Type)type)
            {
                case Organization.Type.World :
                    list = Repository.Organizations.Where(p => p.OrganizationRegions.Any(r => r.Region.ID == 1));
                    break;
                case Organization.Type.Russia:
                    list = Repository.Organizations.Where(p => p.OrganizationRegions.Any(r => r.Region.ID == 2 || r.Region.ParentID == 2));
                    break;
                case Organization.Type.Other:
                    list = Repository.Organizations.Where(p => p.OrganizationRegions.Any(r => r.Region.ParentID == 1 && r.Region.ID != 2 && r.Region.ParentID != 2));
                    break;
            }

            if (list != null)
            {
                /*list = list.Where(p => p.OrganizationSubjects.Any() && (p.EventOrganizations.Any() || p.BlogPostOrganizations.Any() || p.StudyMaterialOrganizations.Any() || p.DocumentOrganizations.Any() || p.PublicationOrganizations.Any()));*/
                if (list.Any()) 
                {
                    list = list.OrderBy(p => p.Name);
                    return View(list.ToList());
                }
            }
            
            return null;
        }

        public ActionResult Item(string url)
        {
            var item = Repository.Organizations.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);

            if (item != null) 
            {
                return View(item);
            }

            return RedirectToNotFoundPage;
        }

        public ActionResult Setting(string url)
        {
             var item = Repository.Organizations.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
             return RedirectToAction("Edit", new { id = item.ID });
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateOrganization())
            {
                var organizationView = new OrganizationView();
                organizationView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
                return View("Edit", organizationView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => p.ID == id);
            if (organization != null && CurrentUser.CanEdit(organization))
            {
                var organizationView = (OrganizationView)ModelMapper.Map(organization, typeof(Organization), typeof(OrganizationView));
                return View(organizationView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
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

                var newOrganization = Repository.Organizations.FirstOrDefault(p => p.ID == organization.ID);
                if (newOrganization != null)
                {
                    return RedirectToAction("Item", new { url = newOrganization.Url });
                }
                return RedirectToAction("Index");
            }
            return View(organizationView);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => p.ID == id);
            if (organization != null && CurrentUser.CanDelete(organization))
            {
                Repository.RemoveOrganization(organization.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
