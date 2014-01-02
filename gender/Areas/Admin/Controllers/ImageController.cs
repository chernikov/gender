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
    public class ImageController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Images.OrderByDescending(p => p.ID);
            var data = new PageableData<Image>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var imageView = new ImageView();
            imageView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
            return View("Edit", imageView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);

            if (image != null)
            {
                var imageView = (ImageView)ModelMapper.Map(image, typeof(Image), typeof(ImageView));
                return View(imageView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(ImageView imageView)
        {
            LinksVerification(imageView.Links);
            if (ModelState.IsValid)
            {
                var image = (Image)ModelMapper.Map(imageView, typeof(ImageView), typeof(Image));
                if (image.ID == 0)
                {
                    Repository.CreateImage(image, CurrentUser.ID);
                }
                else
                {
                    Repository.UpdateImage(image);
                }
                Repository.ModerateImage(image.ID);
                var newSubjects = Repository.UpdateImageSubject(image.ID, imageView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && imageView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, image);
                }
                Repository.UpdateImageRegion(image.ID, imageView.RegionList);
                Repository.UpdateImagePerson(image.ID, imageView.PersonList);
                Repository.ClearImageLinks(image.ID);
                if (imageView.Links != null)
                {
                    foreach (var linkView in imageView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);

                        var imageLink = new ImageLink()
                        {
                            ImageID = image.ID,
                            LinkID = link.ID
                        };

                        Repository.CreateImageLink(imageLink);
                    }
                }
                if (imageView.ID == 0)
                {
                    var authors = Repository.ImagePersons.Where(p => p.ImageID == image.ID && p.PersonID != CurrentUser.Person.ID).Select(p => p.Person).ToList();
                    Subscription.AddMaterial(Repository, newSubjects, image, authors);
                }
                return RedirectToAction("Index");
            }
            return View(imageView);
        }

        public ActionResult Delete(int id)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);
            if (image != null)
            {
                Repository.RemoveImage(image.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Access(int id)
        {
            var list = Repository.ImageAccesses.Where(p => p.ImageID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(ImageAccess imageAccess)
        {
            if (imageAccess.UserID != 0 && imageAccess.ImageID != 0)
            {
                var exist = Repository.ImageAccesses.Any(p => p.ImageID == imageAccess.ImageID && p.UserID == imageAccess.UserID);

                if (!exist)
                {
                    Repository.CreateImageAccess(imageAccess);
                    Subscription.GiveRight(Repository, imageAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.ImageAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemoveImageAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.ImageRecordRedirects.Where(p => p.ImageID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(ImageRecordRedirect imageRecordRedirect)
        {
            Repository.CreateImageRecordRedirect(imageRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var imageRecordRedirect = Repository.ImageRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (imageRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(imageRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModerateImage(id);
            return RedirectBack;
        }

    }
}