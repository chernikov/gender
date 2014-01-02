using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using gender.Model;
using gender.Models.ViewModels;
using gender.Models.ViewModels.User;

namespace gender.Mappers
{
    public static class MapperCollection
    {
        public static class LoginUserMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<User, LoginViewModel>();
                Mapper.CreateMap<LoginViewModel, User>();
            }
        }

        public static class UserMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<RegisterUserView, User>();
                Mapper.CreateMap<CreateAdminUserView, User>();
                Mapper.CreateMap<AddUserView, User>();
                Mapper.CreateMap<SocialRegisterUserView, User>();
                Mapper.CreateMap<AdminUserView, User>()
                    .ForMember(dest => dest.UserEmails,
                    opt => opt.Ignore());

                Mapper.CreateMap<User, AdminUserView>()
                    .ForMember(dest => dest.FirstName,
                        opt => opt.MapFrom(p => p.Person.FirstName))
                    .ForMember(dest => dest.LastName,
                        opt => opt.MapFrom(p => p.Person.LastName))
                    .ForMember(dest => dest.Patronymic,
                        opt => opt.MapFrom(p => p.Person.Patronymic))
                    .ForMember(dest => dest.Photo,
                        opt => opt.MapFrom(p => p.Person.Photo))
                    .ForMember(dest => dest.IsModerator,
                        opt => opt.MapFrom(p => p.UserRoles.Any(r => string.Compare("moderator", r.Role.Code, true) == 0)))
                    .ForMember(dest => dest.IsAdmin,
                        opt => opt.MapFrom(p => p.UserRoles.Any(r => string.Compare("admin", r.Role.Code, true) == 0)))
                    .ForMember(dest => dest.UserEmails, 
                        opt => opt.MapFrom(p =>
                            p.UserEmails.Select(r => new KeyValuePair<string, UserEmailView>(Guid.NewGuid().ToString("N"), (UserEmailView)Mapper.Map(r, typeof(UserEmail), typeof(UserEmailView))))));
                   
               
            }
        }
        
        public static class PublicationMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Publication, ArticlePublicationView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.PublicationSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.PublicationRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.PublicationPersons.Select(r => r.PersonID)))
                    .ForMember(dest => dest.ParentName, opt => opt.MapFrom(p => p.Parent != null ?  p.Parent.Header : string.Empty))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.PublicationLinks.Where(r => !r.IsShop).Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.Files,
                        opt => opt.MapFrom(p =>
                        p.PublicationFiles.Select(r => new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), (FileView)Mapper.Map(r.File, typeof(File), typeof(FileView))))));
        		Mapper.CreateMap<ArticlePublicationView, Publication>();

                Mapper.CreateMap<Publication, BookPublicationView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.PublicationSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.PublicationRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.PublicationOrganizations.Select(r => r.OrganizationID)))
                    .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.PublicationPersons.Select(r => r.PersonID)))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.PublicationLinks.Where(r => !r.IsShop).Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.ShopLinks,
                        opt => opt.MapFrom(p =>
                        p.PublicationLinks.Where(r => r.IsShop).Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.Files,
                        opt => opt.MapFrom(p =>
                        p.PublicationFiles.Select(r => new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), (FileView)Mapper.Map(r.File, typeof(File), typeof(FileView))))));
                Mapper.CreateMap<BookPublicationView, Publication>();

                Mapper.CreateMap<Publication, ThesisPublicationView>()
                   .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.PublicationSubjects.Select(r => r.SubjectID)))
                   .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.PublicationRegions.Select(r => r.RegionID)))
                   .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.PublicationOrganizations.Select(r => r.OrganizationID)))
                   .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.PublicationPersons.Select(r => r.PersonID)))
                   .ForMember(dest => dest.Links,
                       opt => opt.MapFrom(p =>
                       p.PublicationLinks.Where(r => !r.IsShop).Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                   .ForMember(dest => dest.Files,
                       opt => opt.MapFrom(p =>
                       p.PublicationFiles.Select(r => new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), (FileView)Mapper.Map(r.File, typeof(File), typeof(FileView))))));
                Mapper.CreateMap<ThesisPublicationView, Publication>();
        	}
        }

        public static class RegionMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Region, RegionView>();
        		Mapper.CreateMap<RegionView, Region>();
        	}
        }

        
        public static class StudyMaterialMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<StudyMaterial, StudyMaterialView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.StudyMaterialSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.StudyMaterialRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.StudyMaterialOrganizations.Select(r => r.OrganizationID)))
                    .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.StudyMaterialPersons.Select(r => r.PersonID)))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.StudyMaterialLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.Files,
                        opt => opt.MapFrom(p =>
                        p.StudyMaterialFiles.Select(r => new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), (FileView)Mapper.Map(r.File, typeof(File), typeof(FileView))))));
        		Mapper.CreateMap<StudyMaterialView, StudyMaterial>();
        	}
        }

        
        public static class SubjectMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Subject, SubjectView>();
        		Mapper.CreateMap<SubjectView, Subject>();
        	}
        }

        
        public static class WebLinkMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<WebLink, WebLinkView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.WebLinkSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.WebLinkRegions.Select(r => r.RegionID)));
        		Mapper.CreateMap<WebLinkView, WebLink>();
        	}
        }

        
        public static class DocumentMapper
        {
        	public static void Init()
        	{
                Mapper.CreateMap<Document, DocumentView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.DocumentSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.DocumentRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.DocumentOrganizations.Select(r => r.OrganizationID)))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.DocumentLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.Files,
                        opt => opt.MapFrom(p =>
                        p.DocumentFiles.Select(r => new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), (FileView)Mapper.Map(r.File, typeof(File), typeof(FileView))))));
        		Mapper.CreateMap<DocumentView, Document>();
        	}
        }

        
        public static class EventMapper
        {
        	public static void Init()
        	{
                Mapper.CreateMap<Event, EventView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.EventSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.EventRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.EventOrganizations.Select(r => r.OrganizationID)))
                    .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.EventPersons.Select(r => r.PersonID)))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.EventLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.Files,
                        opt => opt.MapFrom(p =>
                        p.EventFiles.Select(r => new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), (FileView)Mapper.Map(r.File, typeof(File), typeof(FileView))))));
        		Mapper.CreateMap<EventView, Event>();
        	}
        }

        
        public static class ImageMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Image, ImageView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.ImageSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.ImageRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.ImagePersons.Select(r => r.PersonID)))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.ImageLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))));
        		Mapper.CreateMap<ImageView, Image>();
        	}
        }

        public static class ContactMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Contact, ContactView>();
                Mapper.CreateMap<ContactView, Contact>();
            }
        }
        
        public static class PersonMapper
        {
        	public static void Init()
        	{
                Mapper.CreateMap<Person, PersonView>()
                    .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.PersonOrganizations.Select(r => r.OrganizationID)))
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.PersonSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.PersonRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.Contacts, 
                        opt => opt.MapFrom(p => 
                        p.PersonContacts.Select(r => new KeyValuePair<string,ContactView>(Guid.NewGuid().ToString("N"), (ContactView)Mapper.Map(r.Contact, typeof(Contact), typeof(ContactView))))))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.PersonLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))));

                Mapper.CreateMap<Person, UserView>()
                   .ForMember(dest => dest.ID, opt => opt.MapFrom(p => p.UserID))
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(p => p.SiteUser != null ? p.SiteUser.Email : ""))

                   .ForMember(dest => dest.ActivatedEmail, opt => opt.MapFrom(p => p.SiteUser != null ? p.SiteUser.ActivatedEmail : true))
                   .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.PersonOrganizations.Select(r => r.OrganizationID)))
                   .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.PersonSubjects.Select(r => r.SubjectID)))
                   .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.PersonRegions.Select(r => r.RegionID)))
                   .ForMember(dest => dest.Contacts,
                       opt => opt.MapFrom(p =>
                       p.PersonContacts.Select(r => new KeyValuePair<string, ContactView>(Guid.NewGuid().ToString("N"), (ContactView)Mapper.Map(r.Contact, typeof(Contact), typeof(ContactView))))))
                   .ForMember(dest => dest.Links,
                       opt => opt.MapFrom(p =>
                       p.PersonLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))))
                    .ForMember(dest => dest.NoticeCommentPeriod, opt => opt.MapFrom(p => p.SiteUser != null ? p.SiteUser.NoticeCommentPeriod : 1))
                    .ForMember(dest => dest.NoticeUpdatePeriod, opt => opt.MapFrom(p => p.SiteUser != null ? p.SiteUser.NoticeUpdatePeriod : 1));

        		Mapper.CreateMap<PersonView, Person>();

                Mapper.CreateMap<CreateAdminUserView, Person>();
                Mapper.CreateMap<AdminUserView, Person>()
                    .ForSourceMember(source => source.UserEmails,
                        opt => opt.Ignore());
                Mapper.CreateMap<RegisterUserView, Person>();
                Mapper.CreateMap<SocialRegisterUserView, Person>();
                Mapper.CreateMap<UserView, Person>();
        	}
        }

        
        public static class OrganizationMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Organization, OrganizationView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.OrganizationSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.OrganizationRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.Contacts,
                        opt => opt.MapFrom(p =>
                        p.OrganizationContacts.Select(r => new KeyValuePair<string, ContactView>(Guid.NewGuid().ToString("N"), (ContactView)Mapper.Map(r.Contact, typeof(Contact), typeof(ContactView))))))
                    .ForMember(dest => dest.Links,
                        opt => opt.MapFrom(p =>
                        p.OrganizationLinks.Select(r => new KeyValuePair<string, LinkView>(Guid.NewGuid().ToString("N"), (LinkView)Mapper.Map(r.Link, typeof(Link), typeof(LinkView))))));
        		Mapper.CreateMap<OrganizationView, Organization>();
        	}
        }

        public static class ArticleMapper
        {
        	public static void Init()
        	{
                Mapper.CreateMap<Article, ArticleView>()
                         .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.ArticleSubjects.Select(r => r.SubjectID)));
        		Mapper.CreateMap<ArticleView, Article>();
        	}
        }

        
        public static class BlogPostMapper
        {
        	public static void Init()
        	{
                Mapper.CreateMap<BlogPost, BlogPostView>()
                    .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(p => p.BlogPostSubjects.Select(r => r.SubjectID)))
                    .ForMember(dest => dest.RegionList, opt => opt.MapFrom(p => p.BlogPostRegions.Select(r => r.RegionID)))
                    .ForMember(dest => dest.OrganizationList, opt => opt.MapFrom(p => p.BlogPostOrganizations.Select(r => r.OrganizationID)))
                    .ForMember(dest => dest.PersonList, opt => opt.MapFrom(p => p.BlogPostPersons.Select(r => r.PersonID)))
                    .ForMember(dest => dest.EventList, opt => opt.MapFrom(p => p.BlogPostEvents.Select(r => r.EventID)));
        		Mapper.CreateMap<BlogPostView, BlogPost>();

                Mapper.CreateMap<ParseBlogPostView, BlogPost>();
        	}
        }

        
        public static class LinkMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Link, LinkView>();
        		Mapper.CreateMap<LinkView, Link>();
        	}
        }

        public static class BlogMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Blog, BlogView>();
        		Mapper.CreateMap<BlogView, Blog>();
        	}
        }

        
        public static class FileMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<File, FileView>();
        		Mapper.CreateMap<FileView, File>();
        	}
        }

        
        public static class RedirectMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Redirect, RedirectView>();
        		Mapper.CreateMap<RedirectView, Redirect>();
        	}
        }

        
        public static class UserEmailMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<UserEmail, UserEmailView>();
        		Mapper.CreateMap<UserEmailView, UserEmail>();
        	}
        }

        public static class CommentMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Comment, CommentView>();
                Mapper.CreateMap<CommentView, Comment>();
            }
        }

        
        public static class PageMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Page, PageView>();
        		Mapper.CreateMap<PageView, Page>();
        	}
        }

        
        public static class SubscriptionTemplateMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<SubscriptionTemplate, SubscriptionTemplateView>();
        		Mapper.CreateMap<SubscriptionTemplateView, SubscriptionTemplate>()
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
        	}
        }

        
        public static class DistributionMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Distribution, DistributionView>();
        		Mapper.CreateMap<DistributionView, Distribution>();
        	}
        }

    }
}