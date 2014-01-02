using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Model
{
    public interface IRepository
    {
        IQueryable<T> GetTable<T>() where T : class;

        #region Article

        #region Article

        IQueryable<Article> Articles { get; }

        bool CreateArticle(Article instance, int? idUser = null);

        bool UpdateArticle(Article instance);

        bool RemoveArticle(int idArticle);

        #endregion

        #region ArticleSubject

        IQueryable<ArticleSubject> ArticleSubjects { get; }

        bool UpdateArticleSubject(int idArticle, List<int> subjects);

        #endregion

        #endregion 

        #region Blog

        #region Blog

        IQueryable<Blog> Blogs { get; }

        bool CreateBlog(Blog instance);

        bool UpdateBlog(Blog instance);

        bool RemoveBlog(int idBlog);

        #endregion

        #region BlogParser

        IQueryable<BlogParser> BlogParsers { get; }

        bool CreateBlogParser(BlogParser instance);

        bool UpdateBlogParser(BlogParser instance);

        bool RemoveBlogParser(int idBlogParser);

        #endregion

        #endregion 

        #region BlogPost

        #region BlogPost

        IQueryable<BlogPost> BlogPosts { get; }

        bool  CreateBlogPost(BlogPost instance, DateTime? AddedDate = null);

        bool UpdateBlogPost(BlogPost instance);

        bool RemoveBlogPost(int idBlogPost);

        #endregion

        #region BlogPostComment

        IQueryable<BlogPostComment> BlogPostComments { get; }

        bool CreateBlogPostComment(BlogPostComment instance);

        bool UpdateBlogPostComment(BlogPostComment instance);

        bool RemoveBlogPostComment(int idBlogPostComment);

        #endregion

        #region BlogPostEvent

        IQueryable<BlogPostEvent> BlogPostEvents { get; }

        bool UpdateBlogPostEvent(int idBlogPost, List<int> events);

        #endregion

        #region BlogPostOrganization

        IQueryable<BlogPostOrganization> BlogPostOrganizations { get; }

        bool UpdateBlogPostOrganization(int idBlogPost, List<int> organizations);

        #endregion

        #region BlogPostPerson

        IQueryable<BlogPostPerson> BlogPostPersons { get; }

        bool UpdateBlogPostPerson(int idBlogPost, List<int> persons);

        #endregion

        #region BlogPostRegion

        IQueryable<BlogPostRegion> BlogPostRegions { get; }

        bool UpdateBlogPostRegion(int idBlogPost, List<int> regions);

        #endregion

        #region BlogPostSubject

        IQueryable<BlogPostSubject> BlogPostSubjects { get; }

        List<int> UpdateBlogPostSubject(int idBlogPost, List<int> subjects);

        #endregion 

        #region BlogPostLike

        IQueryable<BlogPostLike> BlogPostLikes { get; }

        bool CreateBlogPostLike(BlogPostLike instance);

        bool RemoveBlogPostLike(int idBlogPostLike);

        #endregion 

        #endregion 

        #region Comment

        IQueryable<Comment> Comments { get; }

        bool CreateComment(Comment instance);

        bool UpdateComment(Comment instance);

        bool RemoveComment(int idComment);

        #region CommentLike

        IQueryable<CommentLike> CommentLikes { get; }

        bool CreateCommentLike(CommentLike instance);

        bool RemoveCommentLike(int idCommentLike);

        #endregion 

        #endregion 

        #region Contact

        IQueryable<Contact> Contacts { get; }

        bool CreateContact(Contact instance);

        #endregion 

        #region Document

        #region Document

        IQueryable<Document> Documents { get; }

        bool CreateDocument(Document instance);

        bool UpdateDocument(Document instance);

        bool RemoveDocument(int idDocument);

        bool ModerateDocument(int idDocument);

        #endregion

        #region DocumentAccess

        IQueryable<DocumentAccess> DocumentAccesses { get; }

        bool CreateDocumentAccess(DocumentAccess instance);

        bool UpdateDocumentAccess(DocumentAccess instance);

        bool RemoveDocumentAccess(int idDocumentAccess);

        #endregion

        #region DocumentComment

        IQueryable<DocumentComment> DocumentComments { get; }

        bool CreateDocumentComment(DocumentComment instance);

        bool UpdateDocumentComment(DocumentComment instance);

        bool RemoveDocumentComment(int idDocumentComment);

        #endregion

        #region DocumentFile

        IQueryable<DocumentFile> DocumentFiles { get; }

        bool CreateDocumentFile(DocumentFile instance);

        bool ClearDocumentFiles(int documentID);

        #endregion

        #region DocumentLink

        IQueryable<DocumentLink> DocumentLinks { get; }

        bool CreateDocumentLink(DocumentLink instance);

        bool ClearDocumentLinks(int documentID);

        #endregion

        #region DocumentRegion

        IQueryable<DocumentRegion> DocumentRegions { get; }

        bool UpdateDocumentRegion(int idDocument, List<int> regions);

        #endregion 

        #region DocumentSubject

        IQueryable<DocumentSubject> DocumentSubjects { get; }

        List<int> UpdateDocumentSubject(int idDocument, List<int> subjects);

        #endregion

        #region DocumentLike

        IQueryable<DocumentLike> DocumentLikes { get; }

        bool CreateDocumentLike(DocumentLike instance);

        bool RemoveDocumentLike(int idDocumentLike);

        #endregion

        #region DocumentOrganization

        IQueryable<DocumentOrganization> DocumentOrganizations { get; }

        bool UpdateDocumentOrganization(int idDocument, List<int> organizations);

        #endregion 

        #endregion 

        #region Event

        #region Event

        IQueryable<Event> Events { get; }

        bool CreateEvent(Event instance, int? idUser = null);

        bool UpdateEvent(Event instance);

        bool RemoveEvent(int idEvent);

        bool ModerateEvent(int idEvent);

        #endregion

        #region EventFile

        IQueryable<EventFile> EventFiles { get; }

        bool CreateEventFile(EventFile instance);

        bool ClearEventFiles(int eventID);

        #endregion

        #region EventAccess

        IQueryable<EventAccess> EventAccesses { get; }

        bool CreateEventAccess(EventAccess instance);

        bool UpdateEventAccess(EventAccess instance);

        bool RemoveEventAccess(int idEventAccess);

        #endregion

        #region EventComment

        IQueryable<EventComment> EventComments { get; }

        bool CreateEventComment(EventComment instance);

        bool UpdateEventComment(EventComment instance);

        bool RemoveEventComment(int idEventComment);

        #endregion

        #region EventLike

        IQueryable<EventLike> EventLikes { get; }

        bool CreateEventLike(EventLike instance);

        bool RemoveEventLike(int idEventLike);

        #endregion

        #region EventLink

        IQueryable<EventLink> EventLinks { get; }

        bool CreateEventLink(EventLink instance);

        bool ClearEventLinks(int eventID);

        #endregion

        #region EventOrganization

        IQueryable<EventOrganization> EventOrganizations { get; }

        bool UpdateEventOrganization(int idEvent, List<int> organizations);

        #endregion

        #region EventPerson

        IQueryable<EventPerson> EventPersons { get; }

        bool UpdateEventPerson(int idPublication, List<int> persons);

        #endregion

        #region EventRegion

        IQueryable<EventRegion> EventRegions { get; }

        bool UpdateEventRegion(int idEvent, List<int> regions);

  
        #endregion

        #region EventSubject

        IQueryable<EventSubject> EventSubjects { get; }

        List<int> UpdateEventSubject(int idEvent, List<int> subjects);

        #endregion

        #endregion

        #region File

        IQueryable<File> Files { get; }

        bool CreateFile(File instance);

        bool RemoveFile(int idFile);

        #endregion 

        #region Image

        #region Image

        IQueryable<Image> Images { get; }

        bool CreateImage(Image instance, int? idUser = null);

        bool UpdateImage(Image instance);

        bool RemoveImage(int idImage);

        bool ModerateImage(int idImage);

        #endregion

        #region ImageAccess

        IQueryable<ImageAccess> ImageAccesses { get; }

        bool CreateImageAccess(ImageAccess instance);

        bool UpdateImageAccess(ImageAccess instance);

        bool RemoveImageAccess(int idImageAccess);

        #endregion

        #region ImageComment

        IQueryable<ImageComment> ImageComments { get; }

        bool CreateImageComment(ImageComment instance);

        bool UpdateImageComment(ImageComment instance);

        bool RemoveImageComment(int idImageComment);

        #endregion

        #region ImageRegion

        IQueryable<ImageRegion> ImageRegions { get; }

        bool UpdateImageRegion(int idImage, List<int> regions);

        #endregion

        #region ImageSubject

        IQueryable<ImageSubject> ImageSubjects { get; }

        List<int> UpdateImageSubject(int idImage, List<int> subjects);

        #endregion

        #region ImageLike

        IQueryable<ImageLike> ImageLikes { get; }

        bool CreateImageLike(ImageLike instance);

        bool RemoveImageLike(int idImageLike);

        #endregion

        #region ImageLink

        IQueryable<ImageLink> ImageLinks { get; }

        bool CreateImageLink(ImageLink instance);

        bool ClearImageLinks(int imageID);

        #endregion

        #region ImagePerson

        IQueryable<ImagePerson> ImagePersons { get; }

        bool UpdateImagePerson(int idImage, List<int> persons);

        #endregion 

        #endregion 

        #region Link

        IQueryable<Link> Links { get; }

        bool CreateLink(Link instance);

        bool UpdateLink(Link instance);

        bool RemoveLink(int idLink);

        #endregion 

        #region Organization

        #region Organization

        IQueryable<Organization> Organizations { get; }

        bool CreateOrganization(Organization instance);

        bool UpdateOrganization(Organization instance);

        bool RemoveOrganization(int idOrganization);

        #endregion

        #region OrganizationAccess

        IQueryable<OrganizationAccess> OrganizationAccesses { get; }

        bool CreateOrganizationAccess(OrganizationAccess instance);

        bool UpdateOrganizationAccess(OrganizationAccess instance);

        bool RemoveOrganizationAccess(int idOrganizationAccess);

        #endregion

        #region OrganizationLike

        IQueryable<OrganizationLike> OrganizationLikes { get; }

        bool CreateOrganizationLike(OrganizationLike instance);

        bool RemoveOrganizationLike(int idOrganizationLike);

        #endregion

        #region OrganizationSubject

        IQueryable<OrganizationSubject> OrganizationSubjects { get; }

        List<int> UpdateOrganizationSubject(int idOrganization, List<int> subjects);

        #endregion 

        #region OrganizationContact

        IQueryable<OrganizationContact> OrganizationContacts { get; }

        bool CreateOrganizationContact(OrganizationContact instance);

        bool ClearOrganizationContacts(int organizationID);

        #endregion

        #region OrganizationLink

        IQueryable<OrganizationLink> OrganizationLinks { get; }

        bool CreateOrganizationLink(OrganizationLink instance);

        bool ClearOrganizationLinks(int organizationID);

        #endregion

        #region OrganizationRegion

        IQueryable<OrganizationRegion> OrganizationRegions { get; }

        bool UpdateOrganizationRegion(int idOrganization, List<int> regions);

        #endregion 

        #endregion

        #region Person

        #region Person

        IQueryable<Person> Persons { get; }

        bool CreatePerson(Person instance);

        bool UpdatePerson(Person instance);

        bool RemovePerson(int idPerson);

        bool ModeratePerson(int idPerson);

        #endregion

        #region PersonAccess

        IQueryable<PersonAccess> PersonAccesses { get; }

        bool CreatePersonAccess(PersonAccess instance);

        bool UpdatePersonAccess(PersonAccess instance);

        bool RemovePersonAccess(int idPersonAccess);

        #endregion

        #region PersonLink

        IQueryable<PersonLink> PersonLinks { get; }

        bool CreatePersonLink(PersonLink instance);

        bool ClearPersonLinks(int personID);

        #endregion

        #region PersonOrganization

        IQueryable<PersonOrganization> PersonOrganizations { get; }

        bool UpdatePersonOrganization(int idPerson, List<int> organizations);

        #endregion

        #region PersonSubject

        IQueryable<PersonSubject> PersonSubjects { get; }

        List<int> UpdatePersonSubject(int idPerson, List<int> subjects);

        #endregion 

        #region PersonContact

        IQueryable<PersonContact> PersonContacts { get; }

        bool CreatePersonContact(PersonContact instance);

        bool ClearPersonContacts(int personID);
        #endregion

        #region PersonRegion

        IQueryable<PersonRegion> PersonRegions { get; }

        bool UpdatePersonRegion(int idPerson, List<int> regions);

        #endregion 

        #endregion 

        #region Publication

        #region Publication

        IQueryable<Publication> Publications { get; }

        bool CreatePublication(Publication instance);

        bool UpdatePublication(Publication instance, out bool fillText, int? UserID = null);

        bool RemovePublication(int idPublication);

        bool ModeratePublication(int idPublication);

        #endregion

        #region PublicationAccess

        IQueryable<PublicationAccess> PublicationAccesses { get; }

        bool CreatePublicationAccess(PublicationAccess instance);

        bool UpdatePublicationAccess(PublicationAccess instance);

        bool RemovePublicationAccess(int idPublicationAccess);

        #endregion

        #region PublicationComment

        IQueryable<PublicationComment> PublicationComments { get; }

        bool CreatePublicationComment(PublicationComment instance);

        bool UpdatePublicationComment(PublicationComment instance);

        bool RemovePublicationComment(int idPublicationComment);

        #endregion

        #region PublicationFile

        IQueryable<PublicationFile> PublicationFiles { get; }

        bool CreatePublicationFile(PublicationFile instance);

        bool ClearPublicationFiles(int publicationID);

        #endregion

        #region PublicationLike

        IQueryable<PublicationLike> PublicationLikes { get; }

        bool CreatePublicationLike(PublicationLike instance);

        bool RemovePublicationLike(int idPublicationLike);

        #endregion

        #region PublicationLink

        IQueryable<PublicationLink> PublicationLinks { get; }

        bool CreatePublicationLink(PublicationLink instance);

        bool ClearPublicationLinks(int publicationID);

        #endregion

        #region PublicationRegion

        IQueryable<PublicationRegion> PublicationRegions { get; }

        bool UpdatePublicationRegion(int idPublication, List<int> regions);

        #endregion

        #region PublicationSubject

        IQueryable<PublicationSubject> PublicationSubjects { get; }

        List<int> UpdatePublicationSubject(int idPublication, List<int> subjects);

        #endregion 

        #region PublicationPerson

        IQueryable<PublicationPerson> PublicationPersons { get; }

        bool UpdatePublicationPerson(int idPublication, List<int> persons);

        #endregion 

        #region PublicationOrganization

        IQueryable<PublicationOrganization> PublicationOrganizations { get; }

        bool UpdatePublicationOrganization(int idPublication, List<int> organizations);

        #endregion 

        #endregion 

        #region Redirect

        IQueryable<Redirect> Redirects { get; }

        bool CreateRedirect(Redirect instance);

        bool UpdateRedirect(Redirect instance);

        bool RemoveRedirect(int idRedirect);

        #endregion 
        
        #region Region

        IQueryable<Region> Regions { get; }

        bool CreateRegion(Region instance);

        bool UpdateRegion(Region instance);

        bool RemoveRegion(int idRegion);

        bool MoveRegion(int id, int placeBefore);

        bool ChangeParentRegion(int id, int idParent);

        void UpdateRegionsHasEntry();

        #endregion

        #region Role

        IQueryable<Role> Roles { get; }

        bool CreateRole(Role instance);

        bool UpdateRole(Role instance);

        bool RemoveRole(int idRole);

        #endregion

        #region StudyMaterial

        #region StudyMaterial

        IQueryable<StudyMaterial> StudyMaterials { get; }

        bool CreateStudyMaterial(StudyMaterial instance);

        bool UpdateStudyMaterial(StudyMaterial instance);

        bool RemoveStudyMaterial(int idStudyMaterial);

        bool ModerateStudyMaterial(int idStudyMaterial);

        #endregion

        #region StudyMaterialAccess

        IQueryable<StudyMaterialAccess> StudyMaterialAccesses { get; }

        bool CreateStudyMaterialAccess(StudyMaterialAccess instance);

        bool UpdateStudyMaterialAccess(StudyMaterialAccess instance);

        bool RemoveStudyMaterialAccess(int idStudyMaterialAccess);

        #endregion

        #region StudyMaterialComment

        IQueryable<StudyMaterialComment> StudyMaterialComments { get; }

        bool CreateStudyMaterialComment(StudyMaterialComment instance);

        bool UpdateStudyMaterialComment(StudyMaterialComment instance);

        bool RemoveStudyMaterialComment(int idStudyMaterialComment);

        #endregion

        #region StudyMaterialFile

        IQueryable<StudyMaterialFile> StudyMaterialFiles { get; }

        bool CreateStudyMaterialFile(StudyMaterialFile instance);

        bool ClearStudyMaterialFiles(int studyMaterialID);

        #endregion

        #region StudyMaterialLike

        IQueryable<StudyMaterialLike> StudyMaterialLikes { get; }

        bool CreateStudyMaterialLike(StudyMaterialLike instance);

        bool RemoveStudyMaterialLike(int idStudyMaterialLike);

        #endregion

        #region StudyMaterialLink

        IQueryable<StudyMaterialLink> StudyMaterialLinks { get; }

        bool CreateStudyMaterialLink(StudyMaterialLink instance);

        bool ClearStudyMaterialLinks(int studyMaterialID);

        #endregion

        #region StudyMaterialSubject

        IQueryable<StudyMaterialSubject> StudyMaterialSubjects { get; }

        List<int> UpdateStudyMaterialSubject(int idStudyMaterial, List<int> subjects);

        #endregion

        #region StudyMaterialOrganization

        IQueryable<StudyMaterialOrganization> StudyMaterialOrganizations { get; }

        bool UpdateStudyMaterialOrganization(int idStudyMaterial, List<int> organizations);

        #endregion

        #region StudyMaterialPerson

        IQueryable<StudyMaterialPerson> StudyMaterialPersons { get; }

        bool UpdateStudyMaterialPerson(int idStudyMaterial, List<int> persons);

        #endregion

        #region StudyMaterialRegion

        IQueryable<StudyMaterialRegion> StudyMaterialRegions { get; }

        bool UpdateStudyMaterialRegion(int idStudyMaterial, List<int> regions);

        #endregion 

        #endregion 

        #region Subject

        IQueryable<Subject> Subjects { get; }

        bool CreateSubject(Subject instance);

        bool UpdateSubject(Subject instance);

        bool RemoveSubject(int idSubject);

        bool MoveSubject(int id, int placeBefore);

        bool ChangeParentSubject(int id, int idParent);

        #endregion 

        #region User

        #region User

        IQueryable<User> Users { get; }

        bool CreateUser(User instance);

        bool UpdateUser(User instance);

        bool RemoveUser(int idUser);

        User GetUser(string login);

        User Login(string login, string password);

        bool ActivateUser(User instance);

        bool ChangePassword(User instance);

        bool UpdateUserRating(User instance);

        #endregion

        #region UserRole

        IQueryable<UserRole> UserRoles { get; }

        bool CreateUserRole(UserRole instance);

        bool UpdateUserRole(UserRole instance);

        bool RemoveUserRole(int idUserRole);

        #endregion

     

        #region UserEmail

        IQueryable<UserEmail> UserEmails { get; }

        bool CreateUserEmail(UserEmail instance);

        bool UpdateUserEmail(UserEmail instance);

        bool RemoveUserEmail(int idUserEmail);

        

        #region UserSocial

        IQueryable<UserSocial> UserSocials { get; }

        bool CreateUserSocial(UserSocial instance);

        bool UpdateUserSocial(UserSocial instance);

        bool RemoveUserSocial(int idUserSocial);

        #endregion 

        #endregion

        #endregion 

        #region WebLink

        #region WebLink

        IQueryable<WebLink> WebLinks { get; }

        bool CreateWebLink(WebLink instance, int? idUser = null);

        bool UpdateWebLink(WebLink instance);

        bool RemoveWebLink(int idWebLink);

        bool ModerateWebLink(int idWebLink);

        #endregion

        #region WebLinkAccess

        IQueryable<WebLinkAccess> WebLinkAccesses { get; }

        bool CreateWebLinkAccess(WebLinkAccess instance);

        bool UpdateWebLinkAccess(WebLinkAccess instance);

        bool RemoveWebLinkAccess(int idWebLinkAccess);

        #endregion

        #region WebLinkRegion

        IQueryable<WebLinkRegion> WebLinkRegions { get; }

        bool UpdateWebLinkRegion(int idWebLink, List<int> regions);

        #endregion

        #region WebLinkComment

        IQueryable<WebLinkComment> WebLinkComments { get; }

        bool CreateWebLinkComment(WebLinkComment instance);

        bool UpdateWebLinkComment(WebLinkComment instance);

        bool RemoveWebLinkComment(int idWebLinkComment);

        #endregion

        #region WebLinkSubject

        IQueryable<WebLinkSubject> WebLinkSubjects { get; }

        List<int> UpdateWebLinkSubject(int idWebLink, List<int> subjects);

        #endregion

        #region WebLinkLike

        IQueryable<WebLinkLike> WebLinkLikes { get; }

        bool CreateWebLinkLike(WebLinkLike instance);

        bool RemoveWebLinkLike(int idWebLinkLike);

        #endregion 

        #endregion

        #region Redirect

        #region ArticleRecordRedirect

        IQueryable<ArticleRecordRedirect> ArticleRecordRedirects { get; }

        bool CreateArticleRecordRedirect(ArticleRecordRedirect instance);

        bool UpdateArticleRecordRedirect(ArticleRecordRedirect instance);

        bool RemoveArticleRecordRedirect(int idArticleRecordRedirect);

        #endregion

        #region BlogPostRecordRedirect

        IQueryable<BlogPostRecordRedirect> BlogPostRecordRedirects { get; }

        bool CreateBlogPostRecordRedirect(BlogPostRecordRedirect instance);

        bool UpdateBlogPostRecordRedirect(BlogPostRecordRedirect instance);

        bool RemoveBlogPostRecordRedirect(int idBlogPostRecordRedirect);

        #endregion

        #region RecordRedirect

        IQueryable<RecordRedirect> RecordRedirects { get; }

        bool CreateRecordRedirect(RecordRedirect instance);

        bool UpdateRecordRedirect(RecordRedirect instance);

        bool RemoveRecordRedirect(int idRecordRedirect);

        #endregion

        #region DocumentRecordRedirect

        IQueryable<DocumentRecordRedirect> DocumentRecordRedirects { get; }

        bool CreateDocumentRecordRedirect(DocumentRecordRedirect instance);

        bool UpdateDocumentRecordRedirect(DocumentRecordRedirect instance);

        bool RemoveDocumentRecordRedirect(int idDocumentRecordRedirect);

        #endregion

        #region EventRecordRedirect

        IQueryable<EventRecordRedirect> EventRecordRedirects { get; }

        bool CreateEventRecordRedirect(EventRecordRedirect instance);

        bool UpdateEventRecordRedirect(EventRecordRedirect instance);

        bool RemoveEventRecordRedirect(int idEventRecordRedirect);

        #endregion

        #region ImageRecordRedirect

        IQueryable<ImageRecordRedirect> ImageRecordRedirects { get; }

        bool CreateImageRecordRedirect(ImageRecordRedirect instance);

        bool UpdateImageRecordRedirect(ImageRecordRedirect instance);

        bool RemoveImageRecordRedirect(int idImageRecordRedirect);

        #endregion

        #region OrganizationRecordRedirect

        IQueryable<OrganizationRecordRedirect> OrganizationRecordRedirects { get; }

        bool CreateOrganizationRecordRedirect(OrganizationRecordRedirect instance);

        bool UpdateOrganizationRecordRedirect(OrganizationRecordRedirect instance);

        bool RemoveOrganizationRecordRedirect(int idOrganizationRecordRedirect);

        #endregion

        #region PersonRecordRedirect

        IQueryable<PersonRecordRedirect> PersonRecordRedirects { get; }

        bool CreatePersonRecordRedirect(PersonRecordRedirect instance);

        bool UpdatePersonRecordRedirect(PersonRecordRedirect instance);

        bool RemovePersonRecordRedirect(int idPersonRecordRedirect);

        #endregion

        #region PublicationRecordRedirect

        IQueryable<PublicationRecordRedirect> PublicationRecordRedirects { get; }

        bool CreatePublicationRecordRedirect(PublicationRecordRedirect instance);

        bool UpdatePublicationRecordRedirect(PublicationRecordRedirect instance);

        bool RemovePublicationRecordRedirect(int idPublicationRecordRedirect);

        #endregion

        #region StudyMaterialRecordRedirect

        IQueryable<StudyMaterialRecordRedirect> StudyMaterialRecordRedirects { get; }

        bool CreateStudyMaterialRecordRedirect(StudyMaterialRecordRedirect instance);

        bool UpdateStudyMaterialRecordRedirect(StudyMaterialRecordRedirect instance);

        bool RemoveStudyMaterialRecordRedirect(int idStudyMaterialRecordRedirect);

        #endregion

        #region WebLinkRecordRedirect

        IQueryable<WebLinkRecordRedirect> WebLinkRecordRedirects { get; }

        bool CreateWebLinkRecordRedirect(WebLinkRecordRedirect instance);

        bool UpdateWebLinkRecordRedirect(WebLinkRecordRedirect instance);

        bool RemoveWebLinkRecordRedirect(int idWebLinkRecordRedirect);

        #endregion 

        #endregion

        #region Setting

        IQueryable<Setting> Settings { get; }

        bool CreateSetting(Setting instance);

        bool UpdateSetting(Setting instance);

        bool RemoveSetting(int idSetting);

        #endregion 

        #region Page
        
        IQueryable<Page> Pages { get; }
        
        bool CreatePage(Page instance);
        
        bool UpdatePage(Page instance);
        
        bool RemovePage(int idPage);
        
        #endregion 

        #region BlogPostSubscription
        
        IQueryable<BlogPostSubscription> BlogPostSubscriptions { get; }
        
        bool CreateBlogPostSubscription(BlogPostSubscription instance);
        
        bool UpdateBlogPostSubscription(BlogPostSubscription instance);
        
        bool RemoveBlogPostSubscription(int idBlogPostSubscription);
        
        #endregion 

        #region DocumentSubscription
        
        IQueryable<DocumentSubscription> DocumentSubscriptions { get; }
        
        bool CreateDocumentSubscription(DocumentSubscription instance);
        
        bool UpdateDocumentSubscription(DocumentSubscription instance);
        
        bool RemoveDocumentSubscription(int idDocumentSubscription);
        
        #endregion 

        #region EventSubscription
        
        IQueryable<EventSubscription> EventSubscriptions { get; }
        
        bool CreateEventSubscription(EventSubscription instance);
        
        bool UpdateEventSubscription(EventSubscription instance);
        
        bool RemoveEventSubscription(int idEventSubscription);
        
        #endregion 

        #region ImageSubscription
        
        IQueryable<ImageSubscription> ImageSubscriptions { get; }
        
        bool CreateImageSubscription(ImageSubscription instance);
        
        bool UpdateImageSubscription(ImageSubscription instance);
        
        bool RemoveImageSubscription(int idImageSubscription);
        
        #endregion 

        #region PublicationSubscription
        
        IQueryable<PublicationSubscription> PublicationSubscriptions { get; }
        
        bool CreatePublicationSubscription(PublicationSubscription instance);
        
        bool UpdatePublicationSubscription(PublicationSubscription instance);
        
        bool RemovePublicationSubscription(int idPublicationSubscription);
        
        #endregion 

        #region StudyMaterialSubscription
        
        IQueryable<StudyMaterialSubscription> StudyMaterialSubscriptions { get; }
        
        bool CreateStudyMaterialSubscription(StudyMaterialSubscription instance);
        
        bool UpdateStudyMaterialSubscription(StudyMaterialSubscription instance);
        
        bool RemoveStudyMaterialSubscription(int idStudyMaterialSubscription);
        
        #endregion 

        #region WebLinkSubscription
        
        IQueryable<WebLinkSubscription> WebLinkSubscriptions { get; }
        
        bool CreateWebLinkSubscription(WebLinkSubscription instance);
        
        bool UpdateWebLinkSubscription(WebLinkSubscription instance);
        
        bool RemoveWebLinkSubscription(int idWebLinkSubscription);
        
        #endregion 

        #region SubjectSubscription
        
        IQueryable<SubjectSubscription> SubjectSubscriptions { get; }
        
        bool CreateSubjectSubscription(SubjectSubscription instance);
        
        bool RemoveSubjectSubscription(int idSubjectSubscription);

        bool UpdateSubjectSubscription(int idUser, List<int> subjects);
        
        #endregion 

        #region UpdateRecord
        
        IQueryable<UpdateRecord> UpdateRecords { get; }
        
        bool CreateUpdateRecord(UpdateRecord instance);
        
        bool UpdateUpdateRecord(UpdateRecord instance);
        
        bool RemoveUpdateRecord(int idUpdateRecord);
        
        #endregion 

        #region Invite
        
        IQueryable<Invite> Invites { get; }
        
        bool CreateInvite(Invite instance);
        
        bool UpdateInvite(Invite instance);
        
        bool RemoveInvite(int idInvite);
        
        #endregion 

        #region Mail

        IQueryable<Mail> Mails { get; }

        bool SaveMail(Mail instance);

        bool PushMail(Mail instance);

        Mail PopMail();

        Mail PopMail(int id);

        void ClearMailBody(int id);
       
        #endregion 

        #region SubscriptionPart
        
        IQueryable<SubscriptionPart> SubscriptionParts { get; }
        
        bool CreateSubscriptionPart(SubscriptionPart instance);
        
        bool UpdateSubscriptionPart(SubscriptionPart instance);
        
        bool RemoveSubscriptionPart(int idSubscriptionPart);

        void SetProcessSubscriptionPart(IList<SubscriptionPart> list);
        
        #endregion 

        #region SubscriptionTemplate
        
        IQueryable<SubscriptionTemplate> SubscriptionTemplates { get; }
        
        bool CreateSubscriptionTemplate(SubscriptionTemplate instance);
        
        bool UpdateSubscriptionTemplate(SubscriptionTemplate instance);
        
        bool RemoveSubscriptionTemplate(int idSubscriptionTemplate);
        
        #endregion 

        #region Distribution
        
        IQueryable<Distribution> Distributions { get; }
        
        bool CreateDistribution(Distribution instance);
        
        bool UpdateDistribution(Distribution instance);
        
        bool RemoveDistribution(int idDistribution);

        bool StartDistribution(int idDistribution);

        bool ClearDistribution(int idDistribution);
        
        #endregion 
    }
}