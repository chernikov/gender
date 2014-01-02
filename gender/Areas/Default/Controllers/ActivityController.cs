using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class ActivityController : DefaultController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.UpdateRecords.OrderByDescending(p => p.AddedDate).ToList();

            var data = new PageableData<UpdateRecord>();
            data.Init(list.AsQueryable(), page, "Index", itemPerPage : 100);

            foreach (var update in data.List)
            {
                ProcessUpdateRecord(update);
            }
            return View(data);
        }

        public ActionResult Author(string url)
        {
            var person = Repository.Persons.FirstOrDefault(p => p.Url == url);

            if (person != null && person.SiteUser != null)
            {
                return View(person);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AuthorContent(int id)
        {
            var list = Repository.UpdateRecords.Where(p => p.UserID == id).OrderByDescending(p => p.AddedDate).ToList();
            foreach (var update in list)
            {
                ProcessUpdateRecord(update);
            }
            return View(list);
        }

        private void ProcessUpdateRecord(UpdateRecord update)
        {
            switch ((UpdateRecord.MaterialTypeEnum)update.MaterialType)
            {
                case UpdateRecord.MaterialTypeEnum.Article:
                    {
                        var item = Repository.Articles.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Article", new { url = item.Url });
                            update.NameLink = item.Header;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.Document:
                    {
                        var item = Repository.Documents.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Document", new { url = item.Url });
                            update.NameLink = item.Header;
                            update.Moderated = item.ModeratedDate.HasValue;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.Event:
                    {
                        var item = Repository.Events.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Event", new { url = item.Url });
                            update.NameLink = item.Header;
                            update.Moderated = item.ModeratedDate.HasValue;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.Image:
                    {
                        var item = Repository.Images.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Image", new { url = item.Url });
                            update.NameLink = item.Header;
                            update.Moderated = item.ModeratedDate.HasValue;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.Organization:
                    {
                        var item = Repository.Organizations.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Organization", new { url = item.Url });
                            update.NameLink = item.Name;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.Publication:
                    {
                        var item = Repository.Publications.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Publication", new { url = item.Url });
                            update.NameLink = item.Header;
                            update.Moderated = item.ModeratedDate.HasValue;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.StudyMaterial:
                    {
                        var item = Repository.StudyMaterials.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "StudyMaterial", new { url = item.Url });
                            update.NameLink = item.Name;
                            update.Moderated = item.ModeratedDate.HasValue;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.Person:
                    {
                        var item = Repository.Users.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "Person", new { url = item.Person.Url });
                            update.NameLink = item.Person.FullName;
                            update.Moderated = item.Person.ModeratedDate.HasValue;
                        }
                    }
                    break;
                case UpdateRecord.MaterialTypeEnum.WebLink:
                    {
                        var item = Repository.WebLinks.FirstOrDefault(p => p.ID == update.ResourceID);
                        if (item != null)
                        {
                            update.Link = Url.Action("Item", "WebLink", new { url = item.Url });
                            update.NameLink = item.Name;
                            update.Moderated = item.ModeratedDate.HasValue;
                        }
                    }
                    break;
            }
        }
    }
}
