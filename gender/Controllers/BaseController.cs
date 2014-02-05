using System.Web.Mvc;
using Ninject;
using Ninject.Web.Mvc;
using gender.Global.Auth;
using gender.Global.Config;
using gender.Model;
using System.Web.Routing;
using gender.Mappers;
using System.Threading;
using System.Globalization;
using gender.Models.ViewModels;
using System.Collections.Generic;
using System;
using gender.Tools;

namespace gender.Controllers
{
    public abstract class BaseController : Controller, IModelMapperController
    {
        public static string HostName = string.Empty;

        protected static string NotFoundPage = "~/not-found-page";

        protected static string LoginPage = "~/Login";

        [Inject]
        public IRepository Repository { get; set; }

        [Inject]
        public IAuthentication Auth { get; set; }

        [Inject]
        public IConfig Config { get; set; }

        [Inject]
        public IMapper ModelMapper { get; set; }

        public User CurrentUser
        {
            get
            {
                if (Auth != null && Auth.CurrentUser.Identity is IUserable)
                {
                    return ((IUserable)Auth.CurrentUser.Identity).User;
                }
                return null;
            }
        }

        public RedirectResult RedirectToNotFoundPage
        {
            get
            {
                return Redirect(NotFoundPage);
            }
        }


        public RedirectResult RedirectToLoginPage
        {
            get
            {
                return Redirect(LoginPage);
            }
        }

        public RedirectResult RedirectBack
        {
            get
            {
                if (Request.UrlReferrer != null)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
                return RedirectToNotFoundPage;
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.Url != null)
            {
                HostName = requestContext.HttpContext.Request.Url.Authority;
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("RU-ru");
            base.Initialize(requestContext);
        }

        protected void LinksVerification(Dictionary<string, LinkView> Links)
        {
            if (Links != null)
            {
                foreach (var link in Links)
                {
                    if (Uri.IsWellFormedUriString(link.Value.Url, UriKind.Absolute))
                    {
                        link.Value.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";

                        link.Value.Url.GetFavicon(Server.MapPath(link.Value.Icon));
                        var uri = new Uri(link.Value.Url);
                        var result = uri.Authority;
                        link.Value.Title = result.Substring(0, result.LastIndexOf(".")).ToUpper() + result.Substring(result.LastIndexOf("."));
                    }
                    else
                    {
                        ModelState.AddModelError("Links[" + link.Key + "].Title", "Введите корректную ссылку");
                    }
                }
            }
        }
    }
}
