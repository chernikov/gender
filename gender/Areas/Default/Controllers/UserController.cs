using gender.Model;
using gender.Models.Info;
using gender.Models.ViewModels;
using gender.Models.ViewModels.User;
using gender.Tools;
using gender.Tools.Mail;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class UserController : DefaultController
    {
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            // Create a CAPTCHA image using the text stored in the Session object.
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Arial");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

        [Authorize]
        public ActionResult Index()
        {
            return Redirect(Url.Action("Item", "Person", new { url = CurrentUser.Person.Url }));
        }

        [HttpGet]
        public ActionResult Register(string link = null)
        {
            var registerUserView = new RegisterUserView();

            var invite = Repository.Invites.FirstOrDefault(p => string.Compare(p.Link, link, true) == 0 && !p.InvitedID.HasValue);
            if (invite != null)
            {
                registerUserView.InviteID = invite.ID;
            }
            return View(registerUserView);
        }

        [HttpPost]
        public ActionResult Register(RegisterUserView registerUserView)
        {
            if (Session[CaptchaImage.CaptchaValueKey] as string != registerUserView.Captcha)
            {
                ModelState.AddModelError("Captcha", "Код введен неверно");
            }
            if (!registerUserView.Agreement)
            {
                ModelState.AddModelError("Agreement", "Подтвердите согласие с правилами сайта");
            }
            if (ModelState.IsValid)
            {
                var user = (User)ModelMapper.Map(registerUserView, typeof(RegisterUserView), typeof(User));
                var person = (Person)ModelMapper.Map(registerUserView, typeof(RegisterUserView), typeof(Person));

                var invite = Repository.Invites.FirstOrDefault(p => p.ID == registerUserView.InviteID && !p.InvitedID.HasValue);

                user.Login = StringExtension.GenerateNewFile();
                if (invite != null)
                {
                    user.Invited = true;
                }

                Repository.CreateUser(user);
                
                var userEmail = new UserEmail()
                {
                    Email = registerUserView.Email,
                    IsPrimary = true,
                    UserID = user.ID,
                    ActivateLink = StringExtension.GenerateNewFile()
                };
                Repository.CreateUserEmail(userEmail);

                person.AuthorID = user.ID;
                person.UserID = user.ID;

                Repository.CreatePerson(person);

                if (invite != null)
                {
                    invite.InvitedID = user.ID;
                    invite.UsedDate = DateTime.Now;
                    Repository.UpdateInvite(invite);
                }
                NotifyMail.SendNotify("Register", userEmail.Email,
                    format => string.Format(format, HostName),
                    format => string.Format(format, userEmail.ActivateLink, HostName));

                userEmail.Sended = true;
                Repository.UpdateUserEmail(userEmail);
                Subscription.NewUser(Repository, user.ID);
                return View("RegisterSuccess", userEmail);
            }
            return View(registerUserView);
        }

        public ActionResult Activate(string id)
        {
            var userEmail = Repository.UserEmails.FirstOrDefault(p => string.Compare(p.ActivateLink, id, true) == 0);

            if (userEmail != null)
            {
                ViewBag.AccountActivate = false;
                if (!userEmail.ActivateDate.HasValue)
                {
                    userEmail.ActivateDate = DateTime.Now;
                    Repository.UpdateUserEmail(userEmail);
                    Auth.Login(userEmail.User.Login);
                    if (!userEmail.User.Activated)
                    {
                        userEmail.User.ActivatedDate = DateTime.Now;
                        Repository.UpdateUser(userEmail.User);
                        ViewBag.AccountActivate = true;
                    }
                    return View("ActivateSuccess");
                }
                else
                {
                    Auth.Login(userEmail.User.Login);
                    return Redirect("~/");
                }
            }

            return RedirectToNotFoundPage;
        }

        [HttpGet]
        public ActionResult Edit(string url = null)
        {
            Person person = null;
            if (!string.IsNullOrWhiteSpace(url))
            {
                person = Repository.Persons.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            }
            else
            {
                person = CurrentUser.Person;
            }
            if (person != null)
            {
                if (person.UserID == CurrentUser.ID)
                {
                    var userView = (UserView)ModelMapper.Map(person, typeof(Person), typeof(UserView));
                    userView.HasEmail = CurrentUser.UserEmails.Any();

                    return View(userView);
                }
                else
                {
                    return RedirectToAction("Edit", "Person", new { url = person.Url });
                }
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(UserView userView)
        {
            LinksVerification(userView.Links);
            if (ModelState.IsValid)
            {
                var user = Repository.Users.FirstOrDefault(p => p.ID == userView.ID);
                if (user != null)
                {
                    var person = (Person)ModelMapper.Map(userView, typeof(UserView), typeof(Person));
                    person.UserID = user.ID;
                    person.ID = user.Person.ID;
                    Repository.UpdatePerson(person);
                    ProcessEmail(userView, user);
                    Repository.UpdatePersonSubject(person.ID, userView.SubjectList);
                    Repository.UpdatePersonRegion(person.ID, userView.RegionList);
                    Repository.UpdatePersonOrganization(person.ID, userView.OrganizationList);
                    Repository.ClearPersonContacts(person.ID);
                    if (userView.Contacts != null)
                    {
                        foreach (var contactView in userView.Contacts)
                        {
                            var contact = (Contact)ModelMapper.Map(contactView.Value, typeof(ContactView), typeof(Contact));
                            Repository.CreateContact(contact);

                            var personContact = new PersonContact()
                            {
                                PersonID = person.ID,
                                ContactID = contact.ID
                            };
                            Repository.CreatePersonContact(personContact);
                        }
                    }
                    Repository.ClearPersonLinks(person.ID);

                    if (userView.Links != null)
                    {
                        foreach (var linkView in userView.Links)
                        {
                            var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                            Repository.CreateLink(link);

                            var personLink = new PersonLink()
                            {
                                PersonID = person.ID,
                                LinkID = link.ID
                            };
                            Repository.CreatePersonLink(personLink);
                        }
                    }
                    var url = Repository.Persons.First(p => p.ID == person.ID).Url;
                    return RedirectToAction("Item", "Person", new { url });
                }
                return RedirectToNotFoundPage;
            }
            return View(userView);
        }

        private void ProcessEmail(UserView userView, Model.User user)
        {
            if (userView.Email != user.Email)
            {
                UserEmail userEmail = null;
                if (user.UserEmails.Any())
                {
                    userEmail = user.UserEmails.OrderBy(p => p.IsPrimary ? 0 : 1).First();
                    userEmail.Email = userView.Email;
                    Repository.UpdateUserEmail(userEmail);
                    NotifyMail.SendNotify("ActivateEmail", userEmail.Email,
                       format => string.Format(format, HostName),
                       format => string.Format(format, userEmail.ActivateLink, HostName));
                    userEmail.Sended = true;
                    Repository.UpdateUserEmail(userEmail);
                }
                if (userEmail == null)
                {
                    userEmail = new UserEmail()
                    {
                        Email = userView.Email,
                        IsPrimary = true,
                        UserID = user.ID
                    };
                    Repository.CreateUserEmail(userEmail);
                    NotifyMail.SendNotify("AdminRegister", userEmail.Email,
                        format => string.Format(format, HostName),
                        format => string.Format(format, HostName, userEmail.Email, user.Password, userEmail.ActivateLink));

                    userEmail.Sended = true;
                    Repository.UpdateUserEmail(userEmail);
                }
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var changePasswordView = new ChangePasswordView()
            {
                ID = CurrentUser.ID
            };
            return View(changePasswordView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangePassword(ChangePasswordView changePasswordView)
        {
            if (ModelState.IsValid)
            {
                var user = Repository.Users.FirstOrDefault(p => p.ID == changePasswordView.ID);

                if (user != null)
                {
                    user.Password = changePasswordView.NewPassword;
                    Repository.ChangePassword(user);

                    ViewData["message"] = "Пароль успешно изменен";
                }
            }
            return View(changePasswordView);
        }

        public ActionResult SendActivation(int id)
        {
            var user = Repository.Users.FirstOrDefault(p => p.ID == id);

            if (user != null)
            {
                if (user.UserEmails.Any())
                {
                    var userEmail = user.UserEmails.OrderBy(p => p.IsPrimary ? 0 : 1).First();
                    if (!userEmail.ActivateDate.HasValue)
                    {
                        NotifyMail.SendNotify("ActivateEmail",
                            userEmail.Email,
                            format => string.Format(format, HostName),
                            format => string.Format(format, userEmail.ActivateLink, HostName));

                        userEmail.Sended = true;
                        Repository.UpdateUserEmail(userEmail);

                        return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { result = "error" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportPosts(string urls)
        {
            if (urls != null)
            {
                var urlList = urls.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var blogParser = new BlogParserTool();
                var list = new List<string>();
                foreach (var urlItem in urlList)
                {
                    var clearUrlItem = urlItem.Trim();
                    if (!clearUrlItem.StartsWith("http:") && !clearUrlItem.StartsWith("https:"))
                    {
                        clearUrlItem = "http://" + clearUrlItem;
                    }
                    var url = new Uri(clearUrlItem);
                    if (url != default(Uri))
                    {
                        var blogPostView = blogParser.ParsePost(clearUrlItem);

                        if (blogPostView != null)
                        {
                            var blogPost = (BlogPost)ModelMapper.Map(blogPostView, typeof(ParseBlogPostView), typeof(BlogPost));
                            if (blogPostView == null)
                            {
                                list.Add(string.Format(blogParser.LastError, clearUrlItem));
                            }
                            else
                            {
                                blogPost.BlogID = CurrentUser.Blog.ID;
                                blogPost.Source = clearUrlItem.Trim();
                                var exist = Repository.BlogPosts.Any(p => p.BlogID == CurrentUser.Blog.ID && string.Compare(p.Source, blogPost.Source, true) == 0);
                                if (exist)
                                {
                                    list.Add(string.Format("Материал «{0}» ({1}) был добавлен ранее.", blogPost.Header, clearUrlItem));
                                }
                                else
                                {
                                    Repository.CreateBlogPost(blogPost, blogPost.AddedDate);
                                    list.Add(string.Format("Материал «{0}» ({1}) успешно добавлен: <a href=\"{2}\">{0}</a>", blogPost.Header, clearUrlItem, Url.Action("Item", "Blog", new { url = blogPost.Url })));
                                }
                            }
                        }
                        else
                        {
                            list.Add(string.Format(blogParser.LastError, clearUrlItem));
                        }
                    }
                    else
                    {
                        list.Add(string.Format("Материал ({0}) не добавлен. Ошибка: недействительный адрес.", clearUrlItem));
                    }
                }
                return Json(new { result = "ok", data = list }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "empty" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddParser(string url)
        {
            if (url != null)
            {
                var blogParserTool = new BlogParserTool();
                url = url.Trim();
                if (!url.StartsWith("http:") && !url.StartsWith("https:"))
                {
                    url = "http://" + url;
                }
                var link = blogParserTool.FindRss(url);

                if (!string.IsNullOrWhiteSpace(link))
                {
                    var blogParser = new BlogParser()
                    {
                        Link = link,
                        BlogID = CurrentUser.Blog.ID,
                    };

                    Repository.CreateBlogParser(blogParser);
                    return Json(new { result = "ok", data = string.Format("{0} успешно добавлен в транслируемые ленты", link) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "ok", data = blogParserTool.LastError }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = "empty" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveParser(int id)
        {
            var blogParser = Repository.BlogParsers.FirstOrDefault(p => p.ID == id);

            if (blogParser != null)
            {
                if (blogParser.BlogID == CurrentUser.Blog.ID)
                {
                    Repository.RemoveBlogParser(blogParser.ID);
                }
            }
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Parsers()
        {
            return View();
        }

        public ActionResult SubjectSubscription(int id)
        {
            var list = Repository.SubjectSubscriptions.Where(p => p.UserID == id).Select(p => p.SubjectID).ToList();
            var subjects = Repository.Subjects.Where(p => !p.ParentID.HasValue).OrderBy(p => p.OrderBy).ToList();

            ViewBag.List = list;
            return View(subjects);
        }

        public ActionResult CommentSubscription(int id)
        {
            var list = new List<ISubscriptionable>();

            list.AddRange(Repository.BlogPostSubscriptions.Where(p => p.UserID == id));
            list.AddRange(Repository.DocumentSubscriptions.Where(p => p.UserID == id));
            list.AddRange(Repository.EventSubscriptions.Where(p => p.UserID == id));
            list.AddRange(Repository.ImageSubscriptions.Where(p => p.UserID == id));
            list.AddRange(Repository.PublicationSubscriptions.Where(p => p.UserID == id));
            list.AddRange(Repository.StudyMaterialSubscriptions.Where(p => p.UserID == id));
            list.AddRange(Repository.WebLinkSubscriptions.Where(p => p.UserID == id));


            return View(list);
        }

        public ActionResult EditUserNotify(int ID, int NoticeUpdatePeriod, int NoticeCommentPeriod) 
        {
            var user = Repository.Users.FirstOrDefault(p => p.ID == ID);
            if (user != null)
            {
                user.NoticeUpdatePeriod = NoticeUpdatePeriod;
                user.NoticeCommentPeriod = NoticeCommentPeriod;
                Repository.UpdateUser(user);
                return Json(new { result = "ok", message = "Сохранено!" }, JsonRequestBehavior.AllowGet);    
            }
            return Json(new { result = "error", message = "Ошибка!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSubjectSubscription(int ID,  List<int> SubjectSelect)
        {
             var user = Repository.Users.FirstOrDefault(p => p.ID == ID);
             if (user != null)
             {
                 Repository.UpdateSubjectSubscription(user.ID, SubjectSelect);
             }
            return Json(new { result = "ok", message = "Сохранено!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCommentSubscription(int ID, List<CommentSubscription> CommentSubscription)
        {
            var user = Repository.Users.FirstOrDefault(p => p.ID == ID);
            if (user != null)
            {
                var list = CommentSubscription.Where(p => !p.IsChecked);

                foreach (var item in list)
                {
                    if (item.MaterialType == "blog")
                    {
                        Repository.RemoveBlogPostSubscription(item.ID);
                    }
                    if (item.MaterialType == "document")
                    {
                        Repository.RemoveDocumentSubscription(item.ID);
                    }
                    if (item.MaterialType == "event")
                    {
                        Repository.RemoveEventSubscription(item.ID);
                    }
                    if (item.MaterialType == "image")
                    {
                        Repository.RemoveImageSubscription(item.ID);
                    }
                    if (item.MaterialType == "publication")
                    {
                        Repository.RemovePublicationSubscription(item.ID);
                    }
                    if (item.MaterialType == "study-material")
                    {
                        Repository.RemoveStudyMaterialSubscription(item.ID);
                    }
                    if (item.MaterialType == "web-link")
                    {
                        Repository.RemoveWebLinkSubscription(item.ID);
                    }
                }
            }
            return Json(new { result = "ok", message = "Сохранено!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
