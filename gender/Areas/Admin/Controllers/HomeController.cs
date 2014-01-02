using gender.Tools;
using gender.Tools.FineUploader;
using ImageResizer;
using System;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;

namespace gender.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class HomeController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminMenu()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public FineUploaderResult UploadFile(FineUpload upload)
        {
            var uDir = "Content/files/uploads/";
            var uFile = StringExtension.GenerateNewFile() + Path.GetExtension(upload.Filename);
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);
            try
            {
                ImageBuilder.Current.Build(upload.InputStream, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
            return new FineUploaderResult(true, new { fileUrl = "/" + uDir + uFile });
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var url = new FileUploader(Server.MapPath("~")).Upload(upload);
            var message = "Image was saved correctly";

            // since it is an ajax request it requires this string
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", null);</script></body></html>";
            return Content(output);
        }

        public ActionResult UpdateUrls()
        {
            foreach (var item in Repository.Articles)
            {
                Repository.UpdateArticle(item);
            }

            foreach (var item in Repository.Subjects)
            {
                Repository.UpdateSubject(item);
            }

            foreach (var item in Repository.Regions)
            {
                Repository.UpdateRegion(item);
            }

            foreach (var item in Repository.BlogPosts)
            {
                Repository.UpdateBlogPost(item);
            }

            foreach (var item in Repository.Documents)
            {
                Repository.UpdateDocument(item);
            }

            foreach (var item in Repository.Events)
            {
                Repository.UpdateEvent(item);
            }

            foreach (var item in Repository.Images)
            {
                Repository.UpdateImage(item);
            }

            foreach (var item in Repository.Organizations)
            {
                Repository.UpdateOrganization(item);
            }

            foreach (var item in Repository.Persons)
            {
                Repository.UpdatePerson(item);
            }

            foreach (var item in Repository.Publications)
            {
                var boolValue = false;
                Repository.UpdatePublication(item, out boolValue);
            }

            foreach (var item in Repository.StudyMaterials)
            {
                Repository.UpdateStudyMaterial(item);
            }

            foreach (var item in Repository.WebLinks)
            {
                Repository.UpdateWebLink(item);
            }
            return Content("OK");
        }
    }
}
