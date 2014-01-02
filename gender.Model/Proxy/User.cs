using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class User
    {
        public enum CategoryType : int
        {
            Usual = 0x00,
            Privilege = 0x01
        }

        public enum NoticePeriodType : int
        {
            Immediately = 0x01,
            OnceDay = 0x02,
            TwiceWeek = 0x03
        }

        public bool Activated
        {
            get
            {
                return ActivatedDate.HasValue;
            }
        }

        public bool Privileged
        {
            get
            {
                return Category == (int)CategoryType.Privilege;
            }
        }

        public bool InvitedPrivileged
        {
            get
            {
                return Invited || Privileged;
            }
        }
        public UserEmail PrimaryEmail
        {
            get
            {
                var email = UserEmails.OrderBy(p => p.IsPrimary ? 0 : 1).FirstOrDefault();
                if (email != null)
                {
                    return email;
                }

                return new UserEmail()
                {
                    UserID = ID,
                    Email = "fake@gender.ru"
                };
            }
        }
        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }

            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
                var hasRole = UserRoles.Any(p => string.Compare(p.Role.Code, role, true) == 0);
                if (hasRole)
                {
                    return true;
                }
            }
            return false;
        }

        public string Email
        {
            get
            {
                if (UserEmails.Any())
                {
                    return UserEmails.OrderBy(p => p.IsPrimary ? 0 : 1).First().Email;
                }
                return string.Empty;
            }
        }

        public bool ActivatedEmail
        {
            get
            {
                if (UserEmails.Any())
                {
                    return UserEmails.OrderBy(p => p.IsPrimary ? 0 : 1).First().Activated;
                }
                return true;
            }
        }

        public Person Person
        {
            get
            {
                var person = Persons1.FirstOrDefault();

                if (person != null)
                {
                    return person;
                }
                return new Person();
            }
        }

        public bool CanEdit(User user)
        {
            if (user == null)
            {
                return false;
            }
            if (ID == user.ID)
            {
                return true;
            }
            if (user.Person.PersonAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            return false;
        }


        //ACCESS 
        #region Blog
        public bool CanCreateBlog()
        {
            if (Privileged)
            {
                return true;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            return false;
        }

        public bool CanEdit(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (blogPost.Blog.UserID == ID)
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (blogPost.Blog.UserID == ID)
            {
                return true;
            }
            return false;
        }

        public Blog Blog
        {
            get
            {
                return Blogs.FirstOrDefault();
            }
        }
        #endregion


        #region Document
        public bool CanCreateDocument()
        {
            //if (InRoles("admin,moderator"))
            //{
            //    return true;
            //}
            //if (InvitedPrivileged)
            //{
            //    return true;
            //}
            return true;
        }

        public bool CanEdit(Document document)
        {
            if (document == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (document.DocumentAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            //€ создал и нету владельцев
            if (document.UserID == ID && !document.DocumentAccesses.Any())
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(Document document)
        {
            if (document == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (document.UserID == ID)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Event

        public bool CanCreateEvent()
        {

            /*if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (InvitedPrivileged)
            {
                return true;
            }*/
            return true;
        }

        public bool CanEdit(Event @event)
        {
            if (@event == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (@event.EventAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            return false;
        }

        public bool CanDelete(Event @event)
        {
            if (@event == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Image
        public bool CanCreateImage()
        {

            /*if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (InvitedPrivileged)
            {
                return true;
            }*/
            return true;
        }

        public bool CanEdit(Image image)
        {
            if (image == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (image.ImageAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(Image image)
        {
            if (image == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Organization

        public bool CanCreateOrganization()
        {

            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (InvitedPrivileged)
            {
                return true;
            }
            return false;
        }

        public bool CanEdit(Organization organization)
        {
            if (organization == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (organization.OrganizationAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            //€ создал и нету владельцев
            if (organization.UserID == ID && !organization.OrganizationAccesses.Any())
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(Organization organization)
        {
            if (organization == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (organization.UserID == ID)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Person

        public bool CanCreatePerson()
        {

            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (InvitedPrivileged)
            {
                return true;
            }
            return false;
        }

        public bool CanEdit(Person person)
        {
            if (person == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (person.UserID != null && ID == person.UserID)
            {
                return true;
            }

            if (person.PersonAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            //мной создан и не переданы никому права
            if (person.AuthorID == ID && !person.PersonAccesses.Any())
            {
                return true;
            }

            return false;
        }

        public bool CanDelete(Person person)
        {
            if (person == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (person.AuthorID == ID && !person.PersonAccesses.Any())
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Publication

        public bool CanCreatePublication()
        {

            //if (InRoles("admin,moderator"))
            //{
            //    return true;
            //}
            //if (InvitedPrivileged)
            //{
            //    return true;
            //}
            return true;
        }

        public bool CanEdit(Publication publication)
        {
            if (publication == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (publication.PublicationAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            //€ создал и нету владельцев
            if (publication.UserID == ID && !publication.PublicationAccesses.Any())
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(Publication publication)
        {
            if (publication == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (publication.UserID == ID)
            {
                return true;
            }
            return false;
        }
        
        #endregion

        #region StudyMaterial

        public bool CanCreateStudyMaterial()
        {

           /* if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (InvitedPrivileged)
            {
                return true;
            }*/
            return true;
        }

        public bool CanEdit(StudyMaterial studyMaterial)
        {
            if (studyMaterial == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (studyMaterial.StudyMaterialAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }

            //€ создал и нету владельцев
            if (studyMaterial.UserID == ID && !studyMaterial.StudyMaterialAccesses.Any())
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(StudyMaterial studyMaterial)
        {
            if (studyMaterial == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (studyMaterial.UserID == ID)
            {
                return true;
            }
            return false;
        }
        
        #endregion

        #region WebLink

        public bool CanCreateWebLink()
        {

           /* if (InRoles("admin,moderator"))
            {
                return true;
            }
            if (InvitedPrivileged)
            {
                return true;
            }*/
            return true;
        }

        public bool CanEdit(WebLink webLink)
        {
            if (webLink == null)
            {
                return false;
            }

            if (InRoles("admin,moderator"))
            {
                return true;
            }

            if (webLink.WebLinkAccesses.Any(p => p.UserID == ID))
            {
                return true;
            }
            return false;
        }

        public bool CanDelete(WebLink webLink)
        {
            if (webLink == null)
            {
                return false;
            }
            if (InRoles("admin,moderator"))
            {
                return true;
            }
            return false;
        }
        
        #endregion

        //LIKES
        public bool CanLikeComment
        {
            get
            {
                if (InRoles("admin,moderator"))
                {
                    return true;
                }
                if (InvitedPrivileged)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanLike
        {
            get
            {
                if (InRoles("admin,moderator"))
                {
                    return true;
                }
                if (InvitedPrivileged)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsLike(BlogPost blogPost, bool sign)
        {
            return BlogPostLikes.Any(p => p.BlogPostID == blogPost.ID && p.IsLike == sign);
        }

        public bool IsLike(Comment comment, bool sign)
        {
            return CommentLikes.Any(p => p.CommentID == comment.ID && p.IsLike == sign);
        }

        public bool IsLike(Document document, bool sign)
        {
            return DocumentLikes.Any(p => p.DocumentID == document.ID && p.IsLike == sign);
        }

        public bool IsLike(Event @event, bool sign)
        {
            return EventLikes.Any(p => p.EventID == @event.ID && p.IsLike == sign);
        }

        public bool IsLike(Image image, bool sign)
        {
            return ImageLikes.Any(p => p.ImageID == image.ID && p.IsLike == sign);
        }

        public bool IsLike(Organization organization, bool sign)
        {
            return OrganizationLikes.Any(p => p.OrganizationID == organization.ID && p.IsLike == sign);
        }

        public bool IsLike(Publication publication, bool sign)
        {
            return PublicationLikes.Any(p => p.PublicationID == publication.ID && p.IsLike == sign);
        }

        public bool IsLike(StudyMaterial studyMaterial, bool sign)
        {
            return StudyMaterialLikes.Any(p => p.StudyMaterialID == studyMaterial.ID && p.IsLike == sign);
        }

        public bool IsLike(WebLink webLink, bool sign)
        {
            return WebLinkLikes.Any(p => p.WebLinkID == webLink.ID && p.IsLike == sign);
        }

        public bool IsSubjectSubscribed(int idSubject)
        {
            return SubjectSubscriptions.Any(p => p.SubjectID == idSubject);
        }

        public bool IsCommentSubscribed(Event @event)
        {
            return EventSubscriptions.Any(p => p.EventID == @event.ID);
        }

        public bool IsCommentSubscribed(Document document)
        {
            return DocumentSubscriptions.Any(p => p.DocumentID == document.ID);
        }

        public bool IsCommentSubscribed(BlogPost blogPost)
        {
            return BlogPostSubscriptions.Any(p => p.BlogPostID == blogPost.ID);
        }

        public bool IsCommentSubscribed(Image image)
        {
            return ImageSubscriptions.Any(p => p.ImageID == image.ID);
        }

        public bool IsCommentSubscribed(Publication publication)
        {
            return PublicationSubscriptions.Any(p => p.PublicationID == publication.ID);
        }

        public bool IsCommentSubscribed(StudyMaterial studyMaterial)
        {
            return StudyMaterialSubscriptions.Any(p => p.StudyMaterialID == studyMaterial.ID);
        }

        public bool IsCommentSubscribed(WebLink webLink)
        {
            return WebLinkSubscriptions.Any(p => p.WebLinkID == webLink.ID);
        }

        public IEnumerable<UpdateRecord> SubUpdateRecord
        {
            get
            {
                return UpdateRecords.OrderByDescending(p => p.AddedDate).ToList();
            }
        }
    }
}