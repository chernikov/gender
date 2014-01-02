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
    public class DocumentController : DefaultController
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
                case Organization.Type.World:
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
                list = list.Where(p => p.DocumentOrganizations.Any());
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
            var item = Repository.Documents.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);

            if (document != null)
            {
                DocumentSubject documentSubject = null;
                if (idSubject.HasValue)
                {
                    documentSubject = document.DocumentSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextDocument = document.DocumentSubjects.FirstOrDefault(p => p.ID > documentSubject.ID);
                    if (nextDocument != null)
                    {
                        documentSubject = nextDocument;
                    }
                    else
                    {
                        documentSubject = document.DocumentSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    documentSubject = document.DocumentSubjects.FirstOrDefault();
                }
                if (documentSubject != null)
                {
                    return View(documentSubject.Subject);
                }
            }
            return null;
        }

        public ActionResult Comments(int id)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);
            if (document != null)
            {
                return View(document);
            }
            return null;
        }

        [HttpPost]
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
                    var documentComment = new DocumentComment
                    {
                        DocumentID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateDocumentComment(documentComment);
                    Subscription.NewComment(Repository, documentComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateDocument())
            {
                var documentView = new DocumentView();
                return View("Edit", documentView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);

            if (document != null && CurrentUser.CanEdit(document))
            {
                var documentView = (DocumentView)ModelMapper.Map(document, typeof(Document), typeof(DocumentView));
                return View(documentView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(DocumentView documentView)
        {
            LinksVerification(documentView.Links);

            if (ModelState.IsValid)
            {
                var document = (Document)ModelMapper.Map(documentView, typeof(DocumentView), typeof(Document));
                if (document.ID == 0)
                {
                    document.UserID = CurrentUser.ID;
                    Repository.CreateDocument(document);
                }
                else
                {
                    Repository.UpdateDocument(document);
                }

                if (CurrentUser.Rating >= ModerateRating || CurrentUser.InRoles("admin,moderator") || CurrentUser.InvitedPrivileged)
                {
                    Repository.ModerateDocument(document.ID);
                }

                var newSubjects = Repository.UpdateDocumentSubject(document.ID, documentView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && documentView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, document);
                }
                Repository.UpdateDocumentRegion(document.ID, documentView.RegionList);
                Repository.UpdateDocumentOrganization(document.ID, documentView.OrganizationList);
              
                Repository.ClearDocumentLinks(document.ID);
                if (documentView.Links != null)
                {
                    foreach (var linkView in documentView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);

                        var documentLink = new DocumentLink()
                        {
                            DocumentID = document.ID,
                            LinkID = link.ID
                        };

                        Repository.CreateDocumentLink(documentLink);
                    }
                }
                Repository.ClearDocumentFiles(document.ID);
                if (documentView.Files != null)
                {
                    foreach (var fileView in documentView.Files)
                    {
                        var file = (Model.File)ModelMapper.Map(fileView.Value, typeof(FileView), typeof(Model.File));
                        Repository.CreateFile(file);

                        var documentFile = new DocumentFile()
                        {
                            DocumentID = document.ID,
                            FileID = file.ID
                        };
                        Repository.CreateDocumentFile(documentFile);
                    }
                }
                if (documentView.ID == 0)
                {
                    Subscription.AddMaterial(Repository, newSubjects, document, null);
                }

                var newDocument = Repository.Documents.FirstOrDefault(p => p.ID == document.ID);
                if (newDocument != null)
                {
                    return RedirectToAction("Item", new { url = newDocument.Url });
                }
                return RedirectToNotFoundPage;
            }
            return View(documentView);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);

            if (document != null && CurrentUser.CanDelete(document))
            {
                Repository.RemoveDocument(document.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
