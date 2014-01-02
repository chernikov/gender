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
    public class ImageController : DefaultController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Images.Where(p => p.ModeratedDate.HasValue).OrderBy(p => p.Header);

            var data = new PageableData<Image>();
            data.Init(list, page, "Index", itemPerPage: 20);

            return View(data);
        }

        public ActionResult Item(string url)
        {
            var item = Repository.Images.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);

            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);

            if (image != null)
            {
                ImageSubject imageSubject = null;
                if (idSubject.HasValue)
                {
                    imageSubject = image.ImageSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextImage = image.ImageSubjects.FirstOrDefault(p => p.ID > imageSubject.ID);
                    if (nextImage != null)
                    {
                        imageSubject = nextImage;
                    }
                    else
                    {
                        imageSubject = image.ImageSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    imageSubject = image.ImageSubjects.FirstOrDefault();
                }
                if (imageSubject != null)
                {
                    return View(imageSubject.Subject);
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

        public ActionResult Comments(int id)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);
            if (image != null)
            {
                return View(image);
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
                    var imageComment = new ImageComment
                    {
                        ImageID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateImageComment(imageComment);
                    Subscription.NewComment(Repository, imageComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateImage())
            {
                var imageView = new ImageView();
                imageView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
                return View("Edit", imageView);
            }

            return RedirectToLoginPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);

            if (image != null && CurrentUser.CanEdit(image))
            {
                var imageView = (ImageView)ModelMapper.Map(image, typeof(Image), typeof(ImageView));
                return View(imageView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
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
                if (CurrentUser.Rating >= ModerateRating || CurrentUser.InRoles("admin,moderator") || CurrentUser.InvitedPrivileged)
                {
                    Repository.ModerateImage(image.ID);
                }
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

                var newImage = Repository.Images.FirstOrDefault(p => p.ID == image.ID);
                if (newImage != null)
                {
                    return RedirectToAction("Item", new { url = newImage.Url });
                }
                return RedirectToAction("Index");
            }
            return View(imageView);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);
            if (image != null && CurrentUser.CanDelete(image))
            {
                Repository.RemoveImage(image.ID);
            }
            return RedirectToAction("Index");
        }
    }
}
