using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class LikeController : DefaultController
    {
        #region Comment
        [HttpGet]
        public ActionResult CommentLike(int id)
        {
            var comment = Repository.Comments.FirstOrDefault(p => p.ID == id);
            return View(comment);
        }

        [HttpPost]
        public ActionResult CommentLike(int id, bool value)
        {
            var comment = Repository.Comments.FirstOrDefault(p => p.ID == id);

            if (comment != null)
            {
                var existLike = comment.CommentLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(comment);
                    }
                    else
                    {
                        Repository.RemoveCommentLike(existLike.ID);
                    }
                }
                else
                {
                    var commentLike = new CommentLike()
                    {
                        CommentID = comment.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateCommentLike(commentLike);
                }
                var newComment = Repository.Comments.FirstOrDefault(p => p.ID == id);
                return View(newComment);
            }
            return null;
        }
        #endregion 

        #region BlogPostLike

        [HttpGet]
        public ActionResult BlogPostLike(int id)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            return View(blogPost);
        }

        [HttpPost]
        public ActionResult BlogPostLike(int id, bool value)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);

            if (blogPost != null)
            {
                var existLike = blogPost.BlogPostLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(blogPost);
                    }
                    else
                    {
                        Repository.RemoveBlogPostLike(existLike.ID);
                    }
                }
                else
                {
                    var blogPostLike = new BlogPostLike()
                    {
                        BlogPostID = blogPost.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateBlogPostLike(blogPostLike);
                }
                var newBlogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
                return View(newBlogPost);
            }
            return null;
        }


        [HttpGet]
        public ActionResult BlogPostShortLike(int id)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            return View(blogPost);
        }

        [HttpPost]
        public ActionResult BlogPostShortLike(int id, bool value)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);

            if (blogPost != null)
            {
                var existLike = blogPost.BlogPostLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(blogPost);
                    }
                    else
                    {
                        Repository.RemoveBlogPostLike(existLike.ID);
                    }
                }
                else
                {
                    var blogPostLike = new BlogPostLike()
                    {
                        BlogPostID = blogPost.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateBlogPostLike(blogPostLike);
                }
                var newBlogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
                return View(newBlogPost);
            }
            return null;
        }
        #endregion 

        #region Document

        [HttpGet]
        public ActionResult DocumentLike(int id)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);
            return View(document);
        }

        [HttpPost]
        public ActionResult DocumentLike(int id, bool value)
        {
            var document = Repository.Documents.FirstOrDefault(p => p.ID == id);

            if (document != null)
            {
                var existLike = document.DocumentLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(document);
                    }
                    else
                    {
                        Repository.RemoveDocumentLike(existLike.ID);
                    }
                }
                else
                {
                    var documentLike = new DocumentLike()
                    {
                        DocumentID = document.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateDocumentLike(documentLike);
                }
                var newDocument = Repository.Documents.FirstOrDefault(p => p.ID == id);
                return View(newDocument);
            }
            return null;
        }

        #endregion 

        #region Event

        [HttpGet]
        public ActionResult EventLike(int id)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);
            return View(@event);
        }

        [HttpPost]
        public ActionResult EventLike(int id, bool value)
        {
            var @event = Repository.Events.FirstOrDefault(p => p.ID == id);

            if (@event != null)
            {
                var existLike = @event.EventLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(@event);
                    }
                    else
                    {
                        Repository.RemoveEventLike(existLike.ID);
                    }
                }
                else
                {
                    var eventLike = new EventLike()
                    {
                        EventID = @event.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateEventLike(eventLike);
                }
                var newEvent = Repository.Events.FirstOrDefault(p => p.ID == id);
                return View(newEvent);
            }
            return null;
        }

        #endregion 

        #region Image

        [HttpGet]
        public ActionResult ImageLike(int id)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);
            return View(image);
        }

        [HttpPost]
        public ActionResult ImageLike(int id, bool value)
        {
            var image = Repository.Images.FirstOrDefault(p => p.ID == id);

            if (image != null)
            {
                var existLike = image.ImageLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(image);
                    }
                    else
                    {
                        Repository.RemoveImageLike(existLike.ID);
                    }
                }
                else
                {
                    var imageLike = new ImageLike()
                    {
                        ImageID = image.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateImageLike(imageLike);
                }
                var newImage = Repository.Images.FirstOrDefault(p => p.ID == id);
                return View(newImage);
            }
            return null;
        }

        #endregion 

        #region Organization

        [HttpGet]
        public ActionResult OrganizationLike(int id)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => p.ID == id);
            return View(organization);
        }

        [HttpPost]
        public ActionResult OrganizationLike(int id, bool value)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => p.ID == id);

            if (organization != null)
            {
                var existLike = organization.OrganizationLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(organization);
                    }
                    else
                    {
                        Repository.RemoveOrganizationLike(existLike.ID);
                    }
                }
                else
                {
                    var organizationLike = new OrganizationLike()
                    {
                        OrganizationID = organization.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateOrganizationLike(organizationLike);
                }
                var newOrganization = Repository.Organizations.FirstOrDefault(p => p.ID == id);
                return View(newOrganization);
            }
            return null;
        }

        #endregion 

        #region Publication

        [HttpGet]
        public ActionResult PublicationLike(int id)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);
            return View(publication);
        }

        [HttpPost]
        public ActionResult PublicationLike(int id, bool value)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);

            if (publication != null)
            {
                var existLike = publication.PublicationLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(publication);
                    }
                    else
                    {
                        Repository.RemovePublicationLike(existLike.ID);
                    }
                }
                else
                {
                    var publicationLike = new PublicationLike()
                    {
                        PublicationID = publication.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreatePublicationLike(publicationLike);
                }
                var newPublication = Repository.Publications.FirstOrDefault(p => p.ID == id);
                return View(newPublication);
            }
            return null;
        }

        #endregion 

        #region StudyMaterial

        [HttpGet]
        public ActionResult StudyMaterialLike(int id)
        {
            var studyMaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);
            return View(studyMaterial);
        }

        [HttpPost]
        public ActionResult StudyMaterialLike(int id, bool value)
        {
            var studyMaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);

            if (studyMaterial != null)
            {
                var existLike = studyMaterial.StudyMaterialLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(studyMaterial);
                    }
                    else
                    {
                        Repository.RemoveStudyMaterialLike(existLike.ID);
                    }
                }
                else
                {
                    var studyMaterialLike = new StudyMaterialLike()
                    {
                        StudyMaterialID = studyMaterial.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateStudyMaterialLike(studyMaterialLike);
                }
                var newStudyMaterial = Repository.StudyMaterials.FirstOrDefault(p => p.ID == id);
                return View(newStudyMaterial);
            }
            return null;
        }

        #endregion 

        #region WebLink

        [HttpGet]
        public ActionResult WebLinkLike(int id)
        {
            var webLink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);
            return View(webLink);
        }

        [HttpPost]
        public ActionResult WebLinkLike(int id, bool value)
        {
            var webLink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);

            if (webLink != null)
            {
                var existLike = webLink.WebLinkLikes.FirstOrDefault(p => p.UserID == CurrentUser.ID);

                if (existLike != null)
                {
                    if (existLike.IsLike == value)
                    {
                        return View(webLink);
                    }
                    else
                    {
                        Repository.RemoveWebLinkLike(existLike.ID);
                    }
                }
                else
                {
                    var webLinkLike = new WebLinkLike()
                    {
                        WebLinkID = webLink.ID,
                        UserID = CurrentUser.ID,
                        IsLike = value
                    };
                    Repository.CreateWebLinkLike(webLinkLike);
                }
                var newWebLink = Repository.WebLinks.FirstOrDefault(p => p.ID == id);
                return View(newWebLink);
            }
            return null;
        }

        #endregion 
    }
}
