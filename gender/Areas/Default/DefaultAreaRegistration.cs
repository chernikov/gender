using System.Web.Mvc;

namespace gender.Areas.Default
{
    public class DefaultAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Default";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "error",
                "error",
                new { controller = "Error", action = "Index" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "feed",
                new { controller = "Home", action = "Feed" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                "search",
                "search",
                new { controller = "Search", action = "Index" },
                new[] { "gender.Areas.Default.Controllers" }
            );


            context.MapRoute(
                null,
                "activity/author/{url}",
                new { controller = "Activity", action = "Author" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "activity",
                new { controller = "Activity", action = "Index" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "comment",
                new { controller = "Comment", action = "Index" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "blog/{url}",
                new { controller = "Blog", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "blogs/author/{url}",
                new { controller = "Blog", action = "Author" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "blogs/{action}/{id}",
                new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "glossary/article",
                new { controller = "Article", action = "Index" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "article/AlsoSubject",
                new { controller = "Article", action = "AlsoSubject" },
                new[] { "gender.Areas.Default.Controllers" }
            );
            
            context.MapRoute(
                null,
                "glossary/{url}",
                new { controller = "Article", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "section",
                new { controller = "Section", action = "Index" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            
            context.MapRoute(
                null,
                "events/{url}",
                new { controller = "Event", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "event/organization/{url}",
                new { controller = "Event", action = "Organization" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "event/{action}/{id}",
                new { controller = "Event", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "images/{url}",
                new { controller = "Image", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "image/author/{url}",
                new { controller = "Image", action = "Author" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "image/{action}/{id}",
                new { controller = "Image", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "persons/{url}",
                new { controller = "Person", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "person/organization/{url}",
                new { controller = "Person", action = "Organization" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "user/edit/{url}",
                new { controller = "User", action = "Edit", url = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "person/edit/{url}",
                new { controller = "Person", action = "Edit", url = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "person/{action}/{id}",
                new { controller = "Person", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "web-links/{url}",
                new { controller = "WebLink", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "web-link/{action}/{id}",
                new { controller = "WebLink", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "persons/{url}",
                new { controller = "Person", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "person/{action}/{id}",
                new { controller = "Person", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "study-materials/{url}",
                new { controller = "StudyMaterial", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "study-material/organization/{url}",
                new { controller = "StudyMaterial", action = "Organization" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "study-material/author/{url}",
                new { controller = "StudyMaterial", action = "Author" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "study-material/{action}/{id}",
                new { controller = "StudyMaterial", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "organization/{url}",
                new { controller = "Organization", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "organizations/setting/{url}",
                new { controller = "Organization", action = "Setting" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "organizations/{action}/{id}",
                new { controller = "Organization", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "documents/{url}",
                new { controller = "Document", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "document/{action}/{id}",
                new { controller = "Document", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "subjects/{*path}",
                new { controller = "Subject", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "subject/{action}/{id}",
                new { controller = "Subject", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
               null,
               "regions/{*path}",
               new { controller = "Region", action = "Item" },
               new[] { "gender.Areas.Default.Controllers" }
           );

            context.MapRoute(
                null,
                "region/{action}/{id}",
                new { controller = "Region", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "publication/author/{url}",
                new { controller = "Publication", action = "Author" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "publication/organization/{url}",
                new { controller = "Publication", action = "Organization" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "publication/year/{year}",
                new { controller = "Publication", action = "Year" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "publications/{url}",
                new { controller = "Publication", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "publication/{action}/{id}",
                new { controller = "Publication", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "user-activate/{id}",
                new { controller = "User", action = "Activate" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "captcha",
                new { controller = "User", action = "Captcha" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "register/{link}",
                new { controller = "User", action = "Register", link = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "pages/{url}",
                new { controller = "Page", action = "Item" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                "notFoundPage",
                "not-found-page",
                new { controller = "Error", action = "NotFoundPage" },
                new[] { "gender.Areas.Default.Controllers" }
            );

            context.MapRoute(
                null,
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "gender.Areas.Default.Controllers" }
            );
        }
    }
}
