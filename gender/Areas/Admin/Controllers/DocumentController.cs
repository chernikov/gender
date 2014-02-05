using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{ 
    public class DocumentController : AdminController
    {
		public ActionResult Index(int page = 1)
        {
			var list = Repository.Documents;
			var data = new PageableData<Document>();
			data.Init(list, page, "Index");
			return View(data);
		}

		public ActionResult Create() 
		{
			var documentView = new DocumentView();
			return View("Edit", documentView);
		}

		[HttpGet]
		public ActionResult Edit(int id) 
		{
			var  document = Repository.Documents.FirstOrDefault(p => p.ID == id); 

			if (document != null) 
            {
				var documentView = (DocumentView)ModelMapper.Map(document, typeof(Document), typeof(DocumentView));
				return View(documentView);
			}
			return RedirectToNotFoundPage;
		}

		[HttpPost]
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
                var newSubjects = Repository.UpdateDocumentSubject(document.ID, documentView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && documentView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, document);
                }
                Repository.UpdateDocumentRegion(document.ID, documentView.RegionList);
                Repository.UpdateDocumentOrganization(document.ID, documentView.OrganizationList);
                Repository.ModerateDocument(document.ID);
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
                return RedirectToAction("Index");
            }
            return View(documentView);
        }

        public ActionResult Delete(int id)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);
            if (document != null)
            {
                Repository.RemoveDocument(document.ID);
            }
            return RedirectBack;
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModerateDocument(id);
            return RedirectBack;
        }

        public ActionResult Access(int id)
        {
            var list = Repository.DocumentAccesses.Where(p => p.DocumentID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(DocumentAccess documentAccess)
        {
            if (documentAccess.UserID != 0 && documentAccess.DocumentID != 0)
            {
                var exist = Repository.DocumentAccesses.Any(p => p.DocumentID == documentAccess.DocumentID && p.UserID == documentAccess.UserID);

                if (!exist)
                {
                    Repository.CreateDocumentAccess(documentAccess);
                    Subscription.GiveRight(Repository, documentAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.DocumentAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemoveDocumentAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.DocumentRecordRedirects.Where(p => p.DocumentID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(DocumentRecordRedirect documentRecordRedirect)
        {
            Repository.CreateDocumentRecordRedirect(documentRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var documentRecordRedirect = Repository.DocumentRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (documentRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(documentRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }
	}
}