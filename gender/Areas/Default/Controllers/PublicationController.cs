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
    public class PublicationController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthorList()
        {
            var list = Repository.Persons.Where(p => p.PublicationPersons.Any(r => r.Publication.ModeratedDate.HasValue)).OrderBy(p => p.LastName);

            return View(list);
        }

        public ActionResult YearList(int selectedYear = 0)
        {
            var list = Repository.Publications.Where(p => p.Year != null && p.ModeratedDate.HasValue).Select(p => p.Year.Value).Distinct().OrderByDescending(p => p);
            ViewBag.SelectedYear = selectedYear;
            return View(list);
        }

        public ActionResult Author(string url)
        {
            var person = Repository.Persons.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (person != null)
            {
                return View(person);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Organization(string url)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (organization != null)
            {
                return View(organization);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Year(int year)
        {
            var list = Repository.Publications.Where(p => p.Year == year);
            ViewBag.Year = year;
            return View(list);
        }

        public ActionResult MimeType(string mime)
        {
            var mimeType = Config.MimeTypes.FirstOrDefault(p => string.Compare(p.Name, mime, true) == 0);
            return View(mimeType);
        }

        public ActionResult Item(string url)
        {
            var item = Repository.Publications.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);

            if (publication != null)
            {
                PublicationSubject publicationSubject = null;
                if (idSubject.HasValue)
                {
                    publicationSubject = publication.PublicationSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextPublication = publication.PublicationSubjects.FirstOrDefault(p => p.ID > publicationSubject.ID);
                    if (nextPublication != null)
                    {
                        publicationSubject = nextPublication;
                    }
                    else
                    {
                        publicationSubject = publication.PublicationSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    publicationSubject = publication.PublicationSubjects.FirstOrDefault();
                }
                if (publicationSubject != null)
                {
                    return View(publicationSubject.Subject);
                }
            }
            return null;
        }

        public ActionResult Comments(int id)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);
            if (publication != null)
            {
                return View(publication);
            }
            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateComment(CommentView commentView)
        {
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
                {
                    var comment = (Comment)ModelMapper.Map(commentView, typeof(CommentView), typeof(Comment));
                    comment.UserID = CurrentUser.ID;
                    comment.ID = 0;
                    Repository.CreateComment(comment);
                    var publicationComment = new PublicationComment
                    {
                        PublicationID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreatePublicationComment(publicationComment);
                    Subscription.NewComment(Repository, publicationComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult CreateBook()
        {
            if (CurrentUser.CanCreatePublication())
            {
                var bookPublicationView = new BookPublicationView();
                return View("EditBook", bookPublicationView);
            }
            return RedirectToLoginPage;
        }

        [Authorize]
        public ActionResult CreateArticle()
        {
            if (CurrentUser.CanCreatePublication())
            {
                var articlePublicationView = new ArticlePublicationView();
                return View("EditArticle", articlePublicationView);
            }
            return RedirectToLoginPage;
        }

        [Authorize]
        public ActionResult CreateThesis()
        {
            if (CurrentUser.CanCreatePublication())
            {
                var thesisPublicationView = new ThesisPublicationView();
                return View("EditThesis", thesisPublicationView);
            }
            return RedirectToLoginPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);

            if (publication != null && CurrentUser.CanEdit(publication))
            {
                switch ((Publication.TypeEnum)publication.Type)
                {
                    case Publication.TypeEnum.Book:
                        var bookPublicationView = (BookPublicationView)ModelMapper.Map(publication, typeof(Publication), typeof(BookPublicationView));
                        return View("EditBook", bookPublicationView);
                    case Publication.TypeEnum.Article:
                        var articlePublicationView = (ArticlePublicationView)ModelMapper.Map(publication, typeof(Publication), typeof(ArticlePublicationView));
                        return View("EditArticle", articlePublicationView);
                    case Publication.TypeEnum.Thesis:
                        var thesisPublicationView = (ThesisPublicationView)ModelMapper.Map(publication, typeof(Publication), typeof(ThesisPublicationView));
                        return View("EditThesis", thesisPublicationView);
                }
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(PublicationView publicationView)
        {
            LinksVerification(publicationView.Links);

            if (ModelState.IsValid)
            {
                Publication publication = null;
                switch ((Publication.TypeEnum)publicationView.Type)
                {
                    case Publication.TypeEnum.Article:
                        publication = (Publication)ModelMapper.Map(publicationView, typeof(ArticlePublicationView), typeof(Publication));
                        break;
                    case Publication.TypeEnum.Book:
                        publication = (Publication)ModelMapper.Map(publicationView, typeof(BookPublicationView), typeof(Publication));
                        break;
                    case Publication.TypeEnum.Thesis:
                        publication = (Publication)ModelMapper.Map(publicationView, typeof(ThesisPublicationView), typeof(Publication));
                        break;
                }
                publication.UserID = CurrentUser.ID;
                var fillText = false;
                if (publication.ID == 0)
                {
                    Repository.CreatePublication(publication);
                    Repository.CreateUpdateRecord(new UpdateRecord()
                    {
                        ResourceID = publication.ID,
                        MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                        Type = (!string.IsNullOrWhiteSpace(publication.Content) 
                            || (publicationView.Files != null && publicationView.Files.Any()) 
                            || (publicationView.Links != null &&  publicationView.Links.Any()))
                            ? (int)UpdateRecord.TypeEnum.New : (int)UpdateRecord.TypeEnum.NewWithoutText,
                        AddedDate = DateTime.Now,
                        UserID = CurrentUser.ID
                    });
                }
                else
                {
                    Repository.UpdatePublication(publication, out fillText, CurrentUser.ID);
                }

                if (CurrentUser.Rating >= ModerateRating || CurrentUser.InRoles("admin,moderator") || CurrentUser.InvitedPrivileged)
                {
                    Repository.ModeratePublication(publication.ID);
                }
          

                var newSubjects = Repository.UpdatePublicationSubject(publication.ID, publicationView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && publicationView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, publication);
                }
                Repository.UpdatePublicationRegion(publication.ID, publicationView.RegionList);
                Repository.UpdatePublicationPerson(publication.ID, publicationView.PersonList);

                if (publicationView.ID != 0)
                {
                    var countLinks = publicationView.Links != null ? publicationView.Links.Count(p => p.Value.ID == 0) : 0;
                    if (countLinks > 0)
                    {
                        Repository.CreateUpdateRecord(new UpdateRecord()
                        {
                            ResourceID = publication.ID,
                            MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                            Type = countLinks == 1 ? (int)UpdateRecord.TypeEnum.NewLink : (int)UpdateRecord.TypeEnum.NewLinks,
                            AddedDate = DateTime.Now,
                            UserID = CurrentUser.ID
                        });
                    }
                }
                Repository.ClearPublicationLinks(publication.ID);
                if (publicationView.Links != null)
                {
                    foreach (var linkView in publicationView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);

                        var publicationLink = new PublicationLink()
                        {
                            PublicationID = publication.ID,
                            LinkID = link.ID
                        };

                        Repository.CreatePublicationLink(publicationLink);
                    }
                }
                if (publicationView is BookPublicationView)
                {
                    var bookPublicationView = (BookPublicationView)publicationView;
                    Repository.UpdatePublicationOrganization(publication.ID, bookPublicationView.OrganizationList);
                    if (bookPublicationView.ShopLinks != null)
                    {
                        foreach (var linkView in bookPublicationView.ShopLinks)
                        {
                            var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                            Repository.CreateLink(link);

                            var publicationLink = new PublicationLink()
                            {
                                PublicationID = publication.ID,
                                LinkID = link.ID,
                                IsShop = true
                            };

                            Repository.CreatePublicationLink(publicationLink);
                        }
                    }
                }
                var countFiles = publicationView.Files != null ? publicationView.Files.Count(p => p.Value.ID == 0) : 0;
                if (publicationView.ID != 0 && countFiles > 0)
                {
                    Repository.CreateUpdateRecord(new UpdateRecord()
                    {
                        ResourceID = publication.ID,
                        MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                        Type = countFiles == 1 ? (int)UpdateRecord.TypeEnum.NewFile : (int)UpdateRecord.TypeEnum.NewFiles,
                        AddedDate = DateTime.Now,
                        UserID = CurrentUser.ID
                    });
                }

                Repository.ClearPublicationFiles(publication.ID);
                if (publicationView.Files != null)
                {
                    foreach (var fileView in publicationView.Files)
                    {
                        var file = (Model.File)ModelMapper.Map(fileView.Value, typeof(FileView), typeof(Model.File));
                        Repository.CreateFile(file);

                        var publicationFile = new PublicationFile()
                        {
                            PublicationID = publication.ID,
                            FileID = file.ID
                        };
                        Repository.CreatePublicationFile(publicationFile);
                    }
                }

                if (publicationView.ID == 0)
                {
                    var authors = Repository.PublicationPersons.Where(p => p.PublicationID == publication.ID && p.PersonID != CurrentUser.Person.ID).Select(p => p.Person).ToList();
                    Subscription.AddMaterial(Repository, newSubjects, publication, authors);
                }
                else
                {
                    if (fillText)
                    {
                        var authors = Repository.PublicationPersons.Where(p => p.PublicationID == publication.ID).Select(p => p.Person).ToList();
                        Subscription.AddMaterialText(Repository, newSubjects, publication, authors);
                    }
                    if (countFiles > 0)
                    {
                        var authors = Repository.PublicationPersons.Where(p => p.PublicationID == publication.ID).Select(p => p.Person).ToList();
                        Subscription.AddMaterialFiles(Repository, newSubjects, publication, authors);
                    }
                }

                var newPublication = Repository.Publications.FirstOrDefault(p => p.ID == publication.ID);
                if (newPublication != null)
                {
                    return RedirectToAction("Item", new { url = newPublication.Url });
                }
                return RedirectToNotFoundPage;
            }

            switch ((Publication.TypeEnum)publicationView.Type)
            {
                case Publication.TypeEnum.Article:
                    return View("EditArticle", publicationView);
                case Publication.TypeEnum.Book:
                    return View("EditBook", publicationView);
                case Publication.TypeEnum.Thesis:
                    return View("EditThesis", publicationView);
            }
            return null;
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);
            if (publication != null && CurrentUser.CanDelete(publication))
            {
                Repository.RemovePublication(publication.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
