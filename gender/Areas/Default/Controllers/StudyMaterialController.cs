using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;

namespace gender.Areas.Default.Controllers
{
    public class StudyMaterialController : DefaultController
    {

        public ActionResult Index(int page = 1)
        {
            var list = Repository.StudyMaterials.Where(p => p.ModeratedDate.HasValue).OrderBy(p => p.ID);
            var data = new PageableData<StudyMaterial>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Item(string url)
        {
            var item = Repository.StudyMaterials.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            return View(item);
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var studyMaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);

            if (studyMaterial != null)
            {
                StudyMaterialSubject studyMaterialSubject = null;
                if (idSubject.HasValue)
                {
                    studyMaterialSubject = studyMaterial.StudyMaterialSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextStudyMaterial = studyMaterial.StudyMaterialSubjects.FirstOrDefault(p => p.ID > studyMaterialSubject.ID);
                    if (nextStudyMaterial != null)
                    {
                        studyMaterialSubject = nextStudyMaterial;
                    }
                    else
                    {
                        studyMaterialSubject = studyMaterial.StudyMaterialSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    studyMaterialSubject = studyMaterial.StudyMaterialSubjects.FirstOrDefault();
                }
                if (studyMaterialSubject != null)
                {
                    return View(studyMaterialSubject.Subject);
                }
            }
            return null;
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

        public ActionResult Comments(int id)
        {
            var studyMaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);
            if (studyMaterial != null)
            {
                return View(studyMaterial);
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
                    var studyMaterialComment = new StudyMaterialComment
                    {
                        StudyMaterialID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateStudyMaterialComment(studyMaterialComment);
                    Subscription.NewComment(Repository, studyMaterialComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateStudyMaterial())
            {
                var studymaterialView = new StudyMaterialView();
                return View("Edit", studymaterialView);
            }

            return RedirectToNotFoundPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var studymaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);

            if (studymaterial != null && CurrentUser.CanEdit(studymaterial))
            {
                var studymaterialView = (StudyMaterialView)ModelMapper.Map(studymaterial, typeof(StudyMaterial), typeof(StudyMaterialView));
                return View(studymaterialView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(StudyMaterialView studymaterialView)
        {
            LinksVerification(studymaterialView.Links);
            if (ModelState.IsValid)
            {
                var studymaterial = (StudyMaterial)ModelMapper.Map(studymaterialView, typeof(StudyMaterialView), typeof(StudyMaterial));
                if (studymaterial.ID == 0)
                {
                    studymaterial.UserID = CurrentUser.ID;
                    Repository.CreateStudyMaterial(studymaterial);
                }
                else
                {
                    Repository.UpdateStudyMaterial(studymaterial);
                }
                if (CurrentUser.Rating >= ModerateRating || CurrentUser.InRoles("admin,moderator") || CurrentUser.InvitedPrivileged)
                {
                    Repository.ModerateStudyMaterial(studymaterial.ID);
                }
                var newSubjects = Repository.UpdateStudyMaterialSubject(studymaterial.ID, studymaterialView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && studymaterialView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, studymaterial);
                }
                Repository.UpdateStudyMaterialRegion(studymaterial.ID, studymaterialView.RegionList);
                Repository.UpdateStudyMaterialOrganization(studymaterial.ID, studymaterialView.OrganizationList);
                Repository.UpdateStudyMaterialPerson(studymaterial.ID, studymaterialView.PersonList);
                Repository.ClearStudyMaterialLinks(studymaterial.ID);
                if (studymaterialView.Links != null)
                {
                    foreach (var linkView in studymaterialView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);

                        var studyMaterialLink = new StudyMaterialLink()
                        {
                            StudyMaterialID = studymaterial.ID,
                            LinkID = link.ID
                        };

                        Repository.CreateStudyMaterialLink(studyMaterialLink);
                    }
                }
                Repository.ClearStudyMaterialFiles(studymaterial.ID);
                if (studymaterialView.Files != null)
                {
                    foreach (var fileView in studymaterialView.Files)
                    {
                        var file = (Model.File)ModelMapper.Map(fileView.Value, typeof(FileView), typeof(Model.File));
                        Repository.CreateFile(file);

                        var studyMaterialFile = new StudyMaterialFile()
                        {
                            StudyMaterialID = studymaterial.ID,
                            FileID = file.ID
                        };
                        Repository.CreateStudyMaterialFile(studyMaterialFile);
                    }
                }
                if (studymaterialView.ID == 0)
                {
                    var authors = Repository.StudyMaterialPersons.Where(p => p.StudyMaterialID == studymaterial.ID && p.PersonID != CurrentUser.Person.ID).Select(p => p.Person).ToList();
                    Subscription.AddMaterial(Repository, newSubjects, studymaterial, authors);
                }

                var newStudyMaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == studymaterial.ID);
                if (newStudyMaterial != null)
                {
                    return RedirectToAction("Item", new { url = newStudyMaterial.Url });
                }
                return RedirectToNotFoundPage;
            }
            return View(studymaterialView);
        }

        

        [Authorize]
        public ActionResult Delete(int id)
        {
            var studymaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);
            if (studymaterial != null && CurrentUser.CanDelete(studymaterial))
            {
                Repository.RemoveStudyMaterial(studymaterial.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
