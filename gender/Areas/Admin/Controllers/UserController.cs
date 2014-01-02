using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Models.ViewModels.User;
using gender.Global;
using gender.Tools;
using gender.Tools.Mail;


namespace gender.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : AdminController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index(int page = 1)
        {
            var list = Repository.Users.OrderBy(p => p.ID);
            var data = new PageableData<User>();
            data.Init(list, page, "Index");
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var createAdminUserView = new CreateAdminUserView();
            return View(createAdminUserView);
        }

        [HttpPost]
        public ActionResult Create(CreateAdminUserView createAdminUserView)
        {
            if (ModelState.IsValid)
            {
                var user = (User)ModelMapper.Map(createAdminUserView, typeof(CreateAdminUserView), typeof(User));
                var person = (Person)ModelMapper.Map(createAdminUserView, typeof(CreateAdminUserView), typeof(Person));

                user.Login = StringExtension.GenerateNewFile();
                user.Password = StringExtension.CreateRandomPassword(10);
                Repository.CreateUser(user);
              

                var userEmail = new UserEmail()
                {
                    Email = createAdminUserView.Email,
                    IsPrimary = true,
                    UserID = user.ID,
                    ActivateLink = StringExtension.GenerateNewFile()
                };
                Repository.CreateUserEmail(userEmail);

                person.AuthorID = CurrentUser.ID;
                person.UserID = user.ID;

                Repository.CreatePerson(person);

                NotifyMail.SendNotify("AdminRegister", userEmail.Email,
                    format => string.Format(format, HostName),
                    format => string.Format(format, HostName, userEmail.Email, user.Password, userEmail.ActivateLink));

                userEmail.Sended = true;
                Repository.UpdateUserEmail(userEmail);
                Subscription.NewUser(Repository, user.ID);
                return RedirectToAction("Index");
            }
            return View(createAdminUserView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = Repository.Users.FirstOrDefault(p => p.ID == id);

            if (user != null)
            {
                var adminUserView = (AdminUserView)ModelMapper.Map(user, typeof(User), typeof(AdminUserView));
                return View(adminUserView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(AdminUserView userView)
        {
            if (ModelState.IsValid)
            {
                var user = (User)ModelMapper.Map(userView, typeof(AdminUserView), typeof(User));
                var person = (Person)ModelMapper.Map(userView, typeof(AdminUserView), typeof(Person));

                person.ID = userView.PersonID;
                person.UserID = user.ID;
                Repository.UpdateUser(user);
                Repository.UpdatePerson(person);

                UpdateRoles(userView, user);

                foreach (var userEmailView in userView.UserEmails)
                {
                    var userEmail = (UserEmail)ModelMapper.Map(userEmailView.Value, typeof(UserEmailView), typeof(UserEmail));
                    userEmail.UserID = user.ID;
                    if (userEmail.ID == 0)
                    {
                        Repository.CreateUserEmail(userEmail);
                    }
                    else
                    {
                        Repository.UpdateUserEmail(userEmail);
                    }

                    if (!userEmail.Activated && !userEmail.Sended)
                    {
                        var activateLink = Repository.UserEmails.First(p => p.ID == userEmail.ID).ActivateLink;
                        NotifyMail.SendNotify("ActivateEmail",
                            userEmail.Email,
                            format => string.Format(format, HostName),
                            format => string.Format(format, activateLink, HostName));

                        userEmail.Sended = true;
                        Repository.UpdateUserEmail(userEmail);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(userView);
        }

        private void UpdateRoles(AdminUserView userView, Model.User user)
        {
            if (user.InRoles("admin"))
            {
                if (!userView.IsAdmin)
                {
                    var userRole = Repository.UserRoles.FirstOrDefault(p => string.Compare(p.Role.Code, "admin", true) == 0 && p.UserID == user.ID);
                    if (userRole != null)
                    {
                        Repository.RemoveUserRole(userRole.ID);
                    };
                }
            }
            else
            {
                if (userView.IsAdmin)
                {
                    var role = Repository.Roles.First(p => string.Compare(p.Code, "admin", true) == 0);
                    var userRole = new UserRole()
                    {
                        UserID = user.ID,
                        RoleID = role.ID
                    };
                    Repository.CreateUserRole(userRole);
                }
            }
            if (user.InRoles("moderator"))
            {
                if (!userView.IsModerator)
                {
                    var userRole = Repository.UserRoles.FirstOrDefault(p => string.Compare(p.Role.Code, "moderator", true) == 0 && p.UserID == user.ID);
                    if (userRole != null)
                    {
                        Repository.RemoveUserRole(userRole.ID);
                    }
                }
            }
            else
            {
                if (userView.IsModerator)
                {
                    var role = Repository.Roles.First(p => string.Compare(p.Code, "moderator", true) == 0);
                    var userRole = new UserRole()
                    {
                        UserID = user.ID,
                        RoleID = role.ID
                    };
                    Repository.CreateUserRole(userRole);
                }
            }
        }

        public ActionResult Delete(int id)
        {
            if (id != CurrentUser.Person.ID)
            {
                Repository.RemoveUser(id);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Login(int id)
        {
            var user = Repository.Users.FirstOrDefault(p => p.ID == id);
            if (user != null)
            {
                Auth.Login(user.Login);
            }
            return RedirectToAction("Index", "Home", new { area = "Default" });
        }

        [HttpGet]
        public ActionResult EditBatch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditBatch(string Batch)
        {
            var records = Batch.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var emails = new List<string>();

            foreach (var item in records)
            {
                var splitItem = item.Split(new string[] { "\t" }, StringSplitOptions.None);

                if (splitItem.Count() > 4)
                {
                    var fio = splitItem[0];
                    var about = splitItem[1];
                    var email = splitItem[2];
                    var subjects = splitItem[3];
                    var regions = splitItem[4];

                    var fioSplit = fio.Split(new string[] { " ", "." }, StringSplitOptions.None);

                    var surname = "";
                    var firstName = "";
                    var patronymic = "";

                    if (fioSplit.Count() > 0)
                    {
                        surname = fioSplit[0];
                        surname = surname.Substring(0, 1).ToUpper() + surname.Substring(1).ToLower();
                    }
                    if (fioSplit.Count() > 1)
                    {
                        firstName = fioSplit[1];
                        if (firstName.Length > 0)
                        {
                            firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();
                        }
                    }
                    if (fioSplit.Count() > 2)
                    {
                        patronymic = fioSplit[2];
                        if (patronymic.Length > 0)
                        {
                            patronymic = patronymic.Substring(0, 1).ToUpper() + patronymic.Substring(1).ToLower();
                        }
                    }

                    var person = new Person()
                    {
                        AuthorID = CurrentUser.ID,
                        Bio = about.Trim(),
                        FirstName = firstName.Trim(),
                        LastName = surname.Trim(),
                        Patronymic = patronymic.Trim(),
                        ModeratedDate = DateTime.Now,
                    };
                    Repository.CreatePerson(person);

                    if (email.Length > 0)
                    {
                        var emailsSplit = email.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var emailItem in emailsSplit)
                        {
                            if (!emailItem.Trim().IsEmail())
                            {
                                logger.Debug("Email: " + emailItem);
                            }
                            emails.Add(emailItem);

                            var contact = new Contact()
                            {
                                Type = (int)Contact.TypeEnum.Email,
                                Value = emailItem.Trim()
                            };

                            Repository.CreateContact(contact);

                            var personContact = new PersonContact()
                            {
                                PersonID = person.ID,
                                ContactID = contact.ID
                            };
                            Repository.CreatePersonContact(personContact);
                        }
                    }

                    if (subjects.Length > 0)
                    {
                        var subjectsSplit = subjects.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var list = new List<int>();
                        foreach (var subjectItem in subjectsSplit)
                        {
                            var subjectRecord = Repository.Subjects.FirstOrDefault(p => string.Compare(p.Name, subjectItem.Trim(), true) == 0);
                            if (subjectRecord == null)
                            {
                                logger.Debug("subjectItem: " + subjectItem.Trim());
                            }
                            else
                            {
                                list.Add(subjectRecord.ID);
                            }
                        }
                        Repository.UpdatePersonSubject(person.ID, list);
                    }

                    if (regions.Length > 0)
                    {
                        var regionsSplit = regions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var list = new List<int>();
                        foreach (var regionItem in regionsSplit)
                        {
                            var regionRecord = Repository.Regions.FirstOrDefault(p => string.Compare(p.Name, regionItem.Trim(), true) == 0);
                            if (regionRecord == null)
                            {
                                logger.Debug("regionItem: " + regionItem.Trim());
                            }
                            else
                            {
                                list.Add(regionRecord.ID);
                            }
                        }
                        Repository.UpdatePersonRegion(person.ID, list);
                    }
                }
                else
                {
                    logger.Debug("item: " + item);
                }
            }
            var duplicates = emails.GroupBy(s => s).SelectMany(grp => grp.Skip(1)).Distinct().ToList();
            foreach (var email in duplicates)
            {
                logger.Debug("Dublicates : " + email);
            }

            return View();
        }

        public ActionResult PatchNames()
        {
            foreach (var item in Repository.Persons.ToList())
            {
                if (item.LastName.Contains(" ") || item.LastName.Contains(" "))
                {
                    var split = item.LastName.Split(new string[] { " ", " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Count() > 1)
                    {
                        item.LastName = split[0];
                        item.FirstName = split[1];
                    }
                    if (split.Count() > 2)
                    {
                        item.Patronymic = split[2];
                    }
                }
                if (item.FirstName != null)
                {
                    if (item.FirstName.Length > 0)
                    {
                        if (item.FirstName.Length == 1)
                        {
                            item.FirstName = item.FirstName.ToUpper() + ".";
                        }
                        else
                        {
                            item.FirstName = item.FirstName.Substring(0, 1).ToUpper() + item.FirstName.Substring(1);
                        }
                    }
                }
                if (item.Patronymic != null)
                {
                    if (item.Patronymic.Length > 0)
                    {
                        if (item.Patronymic.Length == 1)
                        {
                            item.Patronymic = item.Patronymic.ToUpper() + ".";
                        }
                        else
                        {
                            item.Patronymic = item.Patronymic.Substring(0, 1).ToUpper() + item.Patronymic.Substring(1);
                        }
                    }
                }
                Repository.UpdatePerson(item);
            }
            return Content("OK");
        }
    }
}