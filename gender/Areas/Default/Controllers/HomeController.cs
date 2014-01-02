using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Controllers;
using ImageResizer;
using gender.Tools;
using System.IO;
using gender.Tools.FineUploader;
using gender.Model;
using System.ServiceModel.Syndication;
using gender.Helpers;

namespace gender.Areas.Default.Controllers
{
     public class HomeController : DefaultController
     {
         public ActionResult Index()
         {
             return View();
         }

         public ActionResult UserLogin()
         {
             return View(CurrentUser);
         }

         public ActionResult Subjects()
         {
             var list = Repository.Subjects.Where(p => p.ParentID == null && p.MainShow).OrderBy(p => p.OrderBy).ToList();

             return View(list);
         }

         public ActionResult MimeType(string mime)
         {
             var mimeType = Config.MimeTypes.FirstOrDefault(p => string.Compare(p.Name, mime, true) == 0);
             return View(mimeType);
         }

         public ActionResult UrgentUser()
         {
             if (CurrentUser != null)
             {
                 if (string.IsNullOrWhiteSpace(CurrentUser.Email))
                 {
                     ViewData["message"] = "Добавьте Email";
                     return View();
                 }

                 if (!CurrentUser.Activated)
                 {
                     ViewData["message"] = "Активируйте аккаунт";
                     return View();
                 }
             }
             return null;
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

         [OutputCache(Duration = 3600)]
         public ActionResult NavigationSubject()
         {
             var list = Repository.Subjects.Where(p => p.ParentID == null).OrderBy(p => p.OrderBy).ToList();
             return View(list);
         }

         [OutputCache(Duration=3600)]
         public ActionResult NavigationRegion()
         {
             var list = Repository.Regions.Where(p => p.ParentID == null).OrderBy(p => p.OrderBy).ToList();
             return View(list);
         }

         public ActionResult SearchForm()
         {
             return View();
         }

         public ActionResult AboutShort()
         {
             var page = Repository.Pages.FirstOrDefault(p => string.Compare(p.Url, "about-short", true) == 0);

             return View(page);
         }

         public ActionResult Short(string url)
         {
             var page = Repository.Pages.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
             if (page != null)
             {
                 return View(page);
             }
             return null;
         }

         public ActionResult LastActivity()
         {
             var list = Repository.UpdateRecords.OrderByDescending(p => p.AddedDate).ToList();

             var checkList = new List<UpdateRecord>();
             foreach (var update in list)
             {
                 switch((UpdateRecord.MaterialTypeEnum)update.MaterialType) 
                 {
                     case UpdateRecord.MaterialTypeEnum.Article:
                         {
                             var item = Repository.Articles.FirstOrDefault(p => p.ID == update.ResourceID);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Article", new { url = item.Url });
                                 update.NameLink = item.Header;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.Document:
                         {
                             var item = Repository.Documents.FirstOrDefault(p => p.ID == update.ResourceID && p.ModeratedDate.HasValue);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Document", new { url = item.Url });
                                 update.NameLink = item.Header;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.Event:
                         {
                             var item = Repository.Events.FirstOrDefault(p => p.ID == update.ResourceID && p.ModeratedDate.HasValue);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Event", new { url = item.Url });
                                 update.NameLink = item.Header;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.Image:
                         {
                             var item = Repository.Images.FirstOrDefault(p => p.ID == update.ResourceID && p.ModeratedDate.HasValue);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Image", new { url = item.Url });
                                 update.NameLink = item.Header;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.Organization:
                         {
                             var item = Repository.Organizations.FirstOrDefault(p => p.ID == update.ResourceID);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Organization", new { url = item.Url });
                                 update.NameLink = item.Name;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.Publication:
                         {
                             var item = Repository.Publications.FirstOrDefault(p => p.ID == update.ResourceID && p.ModeratedDate.HasValue);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Publication", new { url = item.Url });
                                 update.NameLink = item.Header;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.StudyMaterial:
                         {
                             var item = Repository.StudyMaterials.FirstOrDefault(p => p.ID == update.ResourceID && p.ModeratedDate.HasValue);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "StudyMaterial", new { url = item.Url });
                                 update.NameLink = item.Name;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.Person:
                         {
                             var item = Repository.Users.FirstOrDefault(p => p.ID == update.ResourceID);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "Person", new { url = item.Person.Url });
                                 update.NameLink = item.Person.FullName;
                                 checkList.Add(update);
                             }
                         }
                         break;
                     case UpdateRecord.MaterialTypeEnum.WebLink:
                         {
                             var item = Repository.WebLinks.FirstOrDefault(p => p.ID == update.ResourceID && p.ModeratedDate.HasValue);
                             if (item != null)
                             {
                                 update.Link = Url.Action("Item", "WebLink", new { url = item.Url });
                                 update.NameLink = item.Name;
                                 checkList.Add(update);
                             }
                         }
                         break;
                 }
                 if (checkList.Count >= 7)
                 {
                     break;
                 }
             }
             return View(checkList);
         }

         public ActionResult LastBlogs()
         {
             var list = Repository.BlogPosts.OrderByDescending(p => p.AddedDate).Take(8);

             return View(list);
         }

         public ActionResult LastComments()
         {
             var list = Repository.Comments.OrderByDescending(p => p.AddedDate).Take(8);

             return View(list);
         }

         public ActionResult Feed()
         {
             var items = new List<SyndicationItem>();

             SyndicationFeed feed = new SyndicationFeed("Gender.ru RSS",
                             "Научный и просветительский портал",
                             new Uri(string.Format("http://{0}", HostName)),
                             "gender.ru RSS 1.0",
                             DateTime.Now);

             var blogPosts = Repository.BlogPosts.OrderByDescending(p => p.AddedDate).Take(100);
             foreach (var blogPost in blogPosts)
             {
                 var item = new SyndicationItem(blogPost.Header,
                                     blogPost.Content,
                                     new Uri(string.Format("http://{0}/blog/{1}", HostName, blogPost.Url)),
                                     blogPost.ID.ToString(),
                                     blogPost.AddedDate);
                 items.Add(item);
             }
             feed.Items = items;

             return new RssActionResult() { Feed = feed };
         }
     }
}
