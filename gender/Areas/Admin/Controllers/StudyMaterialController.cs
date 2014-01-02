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
    public class StudyMaterialController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.StudyMaterials.OrderByDescending(p => p.ID);
            var data = new PageableData<StudyMaterial>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var studymaterialView = new StudyMaterialView();
            return View("Edit", studymaterialView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var studymaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);

            if (studymaterial != null)
            {
                var studymaterialView = (StudyMaterialView)ModelMapper.Map(studymaterial, typeof(StudyMaterial), typeof(StudyMaterialView));
                return View(studymaterialView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
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
                Repository.ModerateStudyMaterial(studymaterial.ID);
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
                return RedirectToAction("Index");
            }
            return View(studymaterialView);
        }

        public ActionResult Delete(int id)
        {
            var studymaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);
            if (studymaterial != null)
            {
                Repository.RemoveStudyMaterial(studymaterial.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Access(int id)
        {
            var list = Repository.StudyMaterialAccesses.Where(p => p.StudyMaterialID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(StudyMaterialAccess studyMaterialAccess)
        {
            if (studyMaterialAccess.UserID != 0 && studyMaterialAccess.StudyMaterialID != 0)
            {
                var exist = Repository.StudyMaterialAccesses.Any(p => p.StudyMaterialID == studyMaterialAccess.StudyMaterialID && p.UserID == studyMaterialAccess.UserID);

                if (!exist)
                {
                    Repository.CreateStudyMaterialAccess(studyMaterialAccess);
                    Subscription.GiveRight(Repository, studyMaterialAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.StudyMaterialAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemoveStudyMaterialAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.StudyMaterialRecordRedirects.Where(p => p.StudyMaterialID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(StudyMaterialRecordRedirect studyMaterialRecordRedirect)
        {
            Repository.CreateStudyMaterialRecordRedirect(studyMaterialRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var studyMaterialRecordRedirect = Repository.StudyMaterialRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (studyMaterialRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(studyMaterialRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModerateStudyMaterial(id);
            return RedirectBack;
        }
    }
}