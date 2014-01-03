using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using gender.Global.Auth;
using gender.Global.Config;
using gender.Model;
using gender.Areas.Default.Controllers;
using System.Threading;
using Ninject;
using gender.Tools;
using gender.Tools.Mail;

namespace gender
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private Thread mailThread { get; set; }

        protected void Application_Start()
        {
            DefaultModelBinder.ResourceClassKey = "Messages";

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            mailThread = new Thread(ThreadFunc);
            mailThread.Start();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (Context.Session != null)
            {
                var auth = DependencyResolver.Current.GetService<IAuthentication>();
                auth.AuthCookieProvider = new HttpContextCookieProvider(Context);
            }
        }
        
        protected void Application_Error(object sender, EventArgs e)
        {
            logger.Debug("Application Error:" + Request.RawUrl);

            //try find redirect 
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var url = Request.RawUrl;
            var redirectUrl = repository.Redirects.FirstOrDefault(p => string.Compare(p.OldLink, url, true) == 0 && !p.IsForum);
            if (redirectUrl != null)
            {
                Response.StatusCode = 301;
                Response.AddHeader("Location", redirectUrl.NewLink);
            }

            var recordRedirectUrl = repository.RecordRedirects.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0 && !p.IsForum);
            if (recordRedirectUrl != null)
            {
                Response.StatusCode = 301;
                Response.AddHeader("Location", recordRedirectUrl.NewUrl);
            }

            Exception exception = Server.GetLastError();
            // Log the exception.

            logger.Error(exception);

            Response.Clear();


            // Clear the error on server.
            Server.ClearError();

            if (redirectUrl != null || recordRedirectUrl != null)
            {
                return;
            }
            
            //fail - not found
            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Http404");
            routeData.Values.Add("area", "Default");
            // Call target Controller and pass the routeData.

            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                 new HttpContextWrapper(Context), routeData));
        }

        private static void ThreadFunc()
        {
            while (true)
            {
                try
                {
                    logger.Info("Start mail thread");
                    var ratingThread = new Thread(RatingCheck);
                    var importThread = new Thread(PeriodCheck);
                    var subscriptionThread = new Thread(ProcessSubscription);
                    var mailThread = new Thread(MailCheck);
                    ratingThread.Start();
                    importThread.Start();
                    subscriptionThread.Start();
                    mailThread.Start();
                    
                    logger.Info("Wait for end mail thread");
                    importThread.Join();
                    subscriptionThread.Join();
                    mailThread.Join();
                    ratingThread.Join();
                    logger.Info("Sleep 60 seconds");
                }
                catch (Exception ex)
                {
                    logger.ErrorException("Thread period error", ex);
                }
                Thread.Sleep(60000);
            }
        }

        private static void PeriodCheck()
        {
            var kernel = DependencyResolver.Current.GetService<IKernel>();
            var repository = kernel.Get<IRepository>();
            ImportRssProcessor.Process(repository);
        }

        private static void ProcessSubscription()
        {
            var kernel = DependencyResolver.Current.GetService<IKernel>();
            var repository = kernel.Get<IRepository>();
             Subscription.Process(repository);
        }

        private static void MailCheck()
        {
            var kernel = DependencyResolver.Current.GetService<IKernel>();
            var repository = kernel.Get<IRepository>();
            var mailSender = kernel.Get<MailSender>();
            while (MailProcessor.SendNextMail(repository, mailSender)) { }
        }


        private static void RatingCheck()
        {
            var kernel = DependencyResolver.Current.GetService<IKernel>();
            var repository = kernel.Get<IRepository>();
            Rating.Process(repository);
        }
    }
}