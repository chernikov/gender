using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Global;
using gender.Models.ViewModels.User;
using gender.Tools;
using gender.Tools.Mail;


namespace gender.Areas.Admin.Controllers
{
    public class PersonController : AdminController
    {
        public ActionResult Index(int page = 1)
        {
            var list = Repository.Persons.OrderByDescending(p => p.ID);
            var data = new PageableData<Person>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Create()
        {
            var personView = new PersonView();
            personView.Contacts = new Dictionary<string, ContactView>();
            personView.Contacts.Add(Guid.NewGuid().ToString("N"), new ContactView());
            personView.Links = new Dictionary<string, LinkView>();
            personView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());

            return View("Edit", personView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var person = Repository.Persons.FirstOrDefault(p => p.ID == id);

            if (person != null)
            {
                var personView = (PersonView)ModelMapper.Map(person, typeof(Person), typeof(PersonView));
                return View(personView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(PersonView personView)
        {
            LinksVerification(personView.Links);
            if (ModelState.IsValid)
            {
                var person = (Person)ModelMapper.Map(personView, typeof(PersonView), typeof(Person));
                if (person.ID == 0)
                {
                    person.AuthorID = CurrentUser.ID;
                    Repository.CreatePerson(person);
                }
                else
                {
                    Repository.UpdatePerson(person);
                }
                Repository.UpdatePersonSubject(person.ID, personView.SubjectList);
                Repository.UpdatePersonRegion(person.ID, personView.RegionList);
                Repository.UpdatePersonOrganization(person.ID, personView.OrganizationList);

                Repository.ClearPersonContacts(person.ID);
                if (personView.Contacts != null)
                {
                    foreach (var contactView in personView.Contacts)
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

                if (personView.Links != null)
                {
                    foreach (var linkView in personView.Links)
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

                return RedirectToAction("Index");
            }
            return View(personView);
        }

        public ActionResult Delete(int id)
        {
            var person = Repository.Persons.FirstOrDefault(p => p.ID == id);
            if (person != null)
            {
                Repository.RemovePerson(person.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Access(int id)
        {
            var list = Repository.PersonAccesses.Where(p => p.PersonID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(PersonAccess personAccess)
        {
            if (personAccess.UserID != 0 && personAccess.PersonID != 0)
            {
                var exist = Repository.PersonAccesses.Any(p => p.PersonID == personAccess.PersonID && p.UserID == personAccess.UserID);

                if (!exist)
                {
                    Repository.CreatePersonAccess(personAccess);
                    Subscription.GiveRight(Repository, personAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.PersonAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemovePersonAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Add(string name)
        {
            var names = name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (names.Count() > 0)
            {
                var person = new Person()
               {
                   LastName = names[0],
                   FirstName = names.Count() > 1 ? names[1] : "",
                   Patronymic = names.Count() > 2 ? names[2] : "",
                   AuthorID = CurrentUser.ID
               };

                Repository.CreatePerson(person);
                return Json(new
                {
                    result = "ok",
                    data = new {
                        id = person.ID,
                        name = person.FullName
                    }
                });
            }
            return Json(new {
                result = "error",
            });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.PersonRecordRedirects.Where(p => p.PersonID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(PersonRecordRedirect personRecordRedirect)
        {
            Repository.CreatePersonRecordRedirect(personRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var personRecordRedirect = Repository.PersonRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (personRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(personRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        [HttpGet]
        public ActionResult AddUser(int id)
        {
            var addUserView = new AddUserView()
            {
                PersonID = id
            };
            return View(addUserView);
        }

        [HttpPost]
        public ActionResult AddUser(AddUserView addUserView)
        {
            if (ModelState.IsValid) 
            {
                var person = Repository.Persons.FirstOrDefault(p => p.ID == addUserView.PersonID);
                if (person != null)
                {
                    var user = (User)ModelMapper.Map(addUserView, typeof(AddUserView), typeof(User));
                    user.ID = 0;
                    user.Login = StringExtension.GenerateNewFile();
                    user.Password = StringExtension.CreateRandomPassword(10);
                    Repository.CreateUser(user);
                   
                    var userEmail = new UserEmail()
                    {
                        Email = addUserView.Email,
                        IsPrimary = true,
                        UserID = user.ID,
                        ActivateLink = StringExtension.GenerateNewFile()
                    };
                    Repository.CreateUserEmail(userEmail);
                    person.UserID = user.ID;
                    Repository.UpdatePerson(person);

                    NotifyMail.SendNotify("AdminRegister", userEmail.Email,
                        format => string.Format(format, HostName),
                        format => string.Format(format, HostName, userEmail.Email, user.Password, userEmail.ActivateLink));

                    userEmail.Sended = true;
                    Repository.UpdateUserEmail(userEmail);
                    Subscription.NewUser(Repository, user.ID);
                }

                return RedirectToAction("Index");
            }
            return View(addUserView);
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModeratePerson(id);
            return RedirectBack;
        }
    }
}