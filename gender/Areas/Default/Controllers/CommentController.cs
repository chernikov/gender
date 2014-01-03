using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class CommentController : DefaultController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Comments.OrderByDescending(p => p.AddedDate);

            var data = new PageableData<Comment>();
            data.Init(list, page, "Index", itemPerPage : 100);

            data.List.ForEach(p => p.Init());
            return View(data);
        }

        public ActionResult ToggleSubscriptionEvent(int id)
        {
            if (CurrentUser != null)
            {
                var eventSubscription = Repository.EventSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.EventID == id);

                if (eventSubscription != null)
                {
                    Repository.RemoveEventSubscription(eventSubscription.ID);
                }
                else
                {
                    eventSubscription = new EventSubscription()
                    {
                        UserID = CurrentUser.ID,
                        EventID = id
                    };
                    Repository.CreateEventSubscription(eventSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ToggleSubscriptionDocument(int id)
        {
            if (CurrentUser != null)
            {
                var documentSubscription = Repository.DocumentSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.DocumentID == id);

                if (documentSubscription != null)
                {
                    Repository.RemoveDocumentSubscription(documentSubscription.ID);
                }
                else
                {
                    documentSubscription = new DocumentSubscription()
                    {
                        UserID = CurrentUser.ID,
                        DocumentID = id
                    };
                    Repository.CreateDocumentSubscription(documentSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ToggleSubscriptionImage(int id)
        {
            if (CurrentUser != null)
            {
                var imageSubscription = Repository.ImageSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.ImageID == id);

                if (imageSubscription != null)
                {
                    Repository.RemoveImageSubscription(imageSubscription.ID);
                }
                else
                {
                    imageSubscription = new ImageSubscription()
                    {
                        UserID = CurrentUser.ID,
                        ImageID = id
                    };
                    Repository.CreateImageSubscription(imageSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ToggleSubscriptionBlogPost(int id)
        {
            if (CurrentUser != null)
            {
                var blogPostSubscription = Repository.BlogPostSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.BlogPostID == id);

                if (blogPostSubscription != null)
                {
                    Repository.RemoveBlogPostSubscription(blogPostSubscription.ID);
                }
                else
                {
                    blogPostSubscription = new BlogPostSubscription()
                    {
                        UserID = CurrentUser.ID,
                        BlogPostID = id
                    };
                    Repository.CreateBlogPostSubscription(blogPostSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ToggleSubscriptionPublication(int id)
        {
            if (CurrentUser != null)
            {
                var publicationSubscription = Repository.PublicationSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.PublicationID == id);

                if (publicationSubscription != null)
                {
                    Repository.RemovePublicationSubscription(publicationSubscription.ID);
                }
                else
                {
                    publicationSubscription = new PublicationSubscription()
                    {
                        UserID = CurrentUser.ID,
                        PublicationID = id
                    };
                    Repository.CreatePublicationSubscription(publicationSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ToggleSubscriptionStudyMaterial(int id)
        {
            if (CurrentUser != null)
            {
                var studyMaterialSubscription = Repository.StudyMaterialSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.StudyMaterialID == id);

                if (studyMaterialSubscription != null)
                {
                    Repository.RemoveStudyMaterialSubscription(studyMaterialSubscription.ID);
                }
                else
                {
                    studyMaterialSubscription = new StudyMaterialSubscription()
                    {
                        UserID = CurrentUser.ID,
                        StudyMaterialID = id
                    };
                    Repository.CreateStudyMaterialSubscription(studyMaterialSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ToggleSubscriptionWebLink(int id)
        {
            if (CurrentUser != null)
            {
                var webLinkSubscription = Repository.WebLinkSubscriptions.FirstOrDefault(p => p.UserID == CurrentUser.ID && p.WebLinkID == id);

                if (webLinkSubscription != null)
                {
                    Repository.RemoveWebLinkSubscription(webLinkSubscription.ID);
                }
                else
                {
                    webLinkSubscription = new WebLinkSubscription()
                    {
                        UserID = CurrentUser.ID,
                        WebLinkID = id
                    };
                    Repository.CreateWebLinkSubscription(webLinkSubscription);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles="admin")]
        public ActionResult RemoveComment(int id)
        {
            if (Repository.RemoveComment(id))
            {
                return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
            };

            return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
        }
    }
}
