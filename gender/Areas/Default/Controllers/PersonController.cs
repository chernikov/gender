using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class PersonController : DefaultController
    {
        public ActionResult Index(int page = 1, string letter = null)
        {
            IQueryable<Person> list = Repository.Persons.Where(p => p.ModeratedDate.HasValue).OrderBy(p => p.LastName);
            if (letter != null)
            {
                list = list.Where(p => p.LastName.StartsWith(letter));
            }
            var data = new PageableData<Person>();
            data.Init(list, page, "Index", itemPerPage : 20);
            ViewBag.Letter = letter;
            return View(data);
        }

        public ActionResult Item(string url)
        {
            var person = Repository.Persons.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            return View(person);
        }

        public ActionResult Organization(string url)
        {
            var organization = Repository.Organizations.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (organization != null)
            {
                return View(organization);
            }
            return RedirectToNotFoundPage;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreatePerson())
            {
                var personView = new PersonView();
                personView.Contacts = new Dictionary<string, ContactView>();
                personView.Contacts.Add(Guid.NewGuid().ToString("N"), new ContactView());
                personView.Links = new Dictionary<string, LinkView>();
                personView.Links.Add(Guid.NewGuid().ToString("N"), new LinkView());
                return View("Edit", personView);
            }

            return RedirectToLoginPage;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string url)
        {
            var person = Repository.Persons.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (person != null && CurrentUser.CanEdit(person))
            {
                var personView = (PersonView)ModelMapper.Map(person, typeof(Person), typeof(PersonView));
                return View(personView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
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
                var url = Repository.Persons.First(p => p.ID == person.ID).Url;
                return RedirectToAction("Item", "Person", new { url });
            }
            return View(personView);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var person = Repository.Persons.FirstOrDefault(p => p.ID == id);
            if (person != null && CurrentUser.CanDelete(person))
            {
                Repository.RemovePerson(person.ID);
            }
            return RedirectToAction("Index");
        }
       
    }
}
