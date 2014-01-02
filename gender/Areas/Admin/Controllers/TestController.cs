using gender.Model;
using gender.Tools.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Admin.Controllers
{
    public class TestController : AdminController
    {
        //
        // GET: /Admin/Test/

        public ActionResult Index()
        {
            var mailSender = DependencyResolver.Current.GetService<IMailSender>();

            mailSender.SendMail("chernikov@gmail.com", "Test", "Test");
            return Content("OK");
        }


        public ActionResult ModerateAll()
        {
            foreach (var document in Repository.Documents)
            {
                Repository.ModerateDocument(document.ID);
            }
            foreach (var person in Repository.Persons)
            {
                Repository.ModeratePerson(person.ID);
            }
            foreach (var @event in Repository.Events)
            {
                Repository.ModerateEvent(@event.ID);
            }

            foreach (var image in Repository.Images)
            {
                Repository.ModerateImage(image.ID);
            }

            foreach (var publication in Repository.Publications)
            {
                Repository.ModeratePublication(publication.ID);
            }            

            foreach (var studyMaterial in Repository.StudyMaterials)
            {
                Repository.ModerateStudyMaterial(studyMaterial.ID);
            }

            foreach (var webLink in Repository.WebLinks)
            {
                Repository.ModerateWebLink(webLink.ID);
            }
            return null;
        }

        public ActionResult CreateBlogs()
        {
            foreach (var user in Repository.Users.Where(p => !p.Blogs.Any()))
            {
                var blog = new Blog()
                {
                    LastUpdate = DateTime.Now,
                    UserID = user.ID
                };
                Repository.CreateBlog(blog);
            }
            return null;
        }
    }
}
