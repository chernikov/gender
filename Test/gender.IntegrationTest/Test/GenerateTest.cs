using gender.Global.Config;
using gender.IntegrationTest.Tools;
using gender.Mappers;
using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using GenerateData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace gender.IntegrationTest.Test
{
    [TestFixture]
    public class GenerateTest
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        [Test]
        public void GenerateData()
        {
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    GenerateArticles(2);
                    GenerateOrganizations(2);
                    GeneratePersons(2);
                    GenerateEvents(2);
                    GenerateDocuments(2);
                    GeneratePublications(3);
                    GenerateWebLinks(2);
                    GenerateStudyMaterials(2);
                    GenerateImages(2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        #region Article

        public void GenerateArticles(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();
            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeArticle(repository, user);
            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeArticle(IRepository repository, User user)
        {
            var article = new Article()
            {
                Header = OrganizationGenerate.GetRandomOne(),
            };

            article.Text = string.Format("{0} &mdash; {1}", article.Header, Textarium.GetRandomHtmlText(1));
            article.Url = Translit.Translate(article.Header);
            repository.CreateArticle(article, user.ID);
            SelectArticleSubjects(repository, article);
            Console.WriteLine("Создана статья: " + article.Header);
        }

        private static void SelectArticleSubjects(IRepository repository, Article article)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateArticleSubject(article.ID, subjectList);
        }

        #endregion

        #region Organization

        public void GenerateOrganizations(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeOrganization(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeOrganization(IRepository repository, User user)
        {
            var organization = new Organization()
            {
                UserID = user.ID,
                Name = OrganizationGenerate.GetRandom(),
                Logo = Imaginarium.SaveRandomImage("/Content/files/uploads/"),
                Info = Textarium.GetRandomHtmlText(4),
            };
            repository.CreateOrganization(organization);

            var male = rand.Next(2) % 2 == 0;
            var firstName = male ? NameGenerate.GetFemaleNameRandom() : NameGenerate.GetMaleNameRandom();
            var lastName = male ? NameGenerate.GetFemaleSurnameRandom() : NameGenerate.GetMaleSurnameRandom();

            MakeOrganizationContact(repository, organization, firstName, lastName);

            for (int j = 0; j < 2; j++)
            {
                MakeOrganizationLink(repository, organization, firstName, lastName);
            }
            SelectOrganizationSubjects(repository, organization);
            SelectOrganizationRegions(repository, organization);

            Console.WriteLine("Создана организация: " + organization.Name);
        }

        private static void MakeOrganizationContact(IRepository repository, Organization organization, string firstName, string lastName)
        {
            var contact = new Contact()
            {
                Type = (int)Contact.TypeEnum.Email,
                Value = EmailGenerate.GetRandomOrganization(organization.Name)
            };
            repository.CreateContact(contact);
            repository.CreateOrganizationContact(new OrganizationContact()
            {
                OrganizationID = organization.ID,
                ContactID = contact.ID
            });
            contact = new Contact()
            {
                Type = (int)Contact.TypeEnum.Phone,
                Value = PhoneGenerate.GetRandom()
            };
            repository.CreateContact(contact);
            repository.CreateOrganizationContact(new OrganizationContact()
            {
                OrganizationID = organization.ID,
                ContactID = contact.ID
            });

            contact = new Contact()
            {
                Type = (int)Contact.TypeEnum.Skype,
                Value = SkypeGenerate.GetRandom(firstName, lastName)
            };
            repository.CreateContact(contact);
            repository.CreateOrganizationContact(new OrganizationContact()
            {
                OrganizationID = organization.ID,
                ContactID = contact.ID
            });
        }

        private static void MakeOrganizationLink(IRepository repository, Organization organization, string firstName, string LastName)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomSocial(firstName, LastName)
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Страница на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreateOrganizationLink(new OrganizationLink()
            {
                LinkID = link.ID,
                OrganizationID = organization.ID
            });
        }

        private static void SelectOrganizationSubjects(IRepository repository, Organization organization)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateOrganizationSubject(organization.ID, subjectList);
        }

        private static void SelectOrganizationRegions(IRepository repository, Organization organization)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdateOrganizationRegion(organization.ID, regionList);
        }

        #endregion

        #region Person
        [Test]
        public void GeneratePersons(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakePerson(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakePerson(IRepository repository, User user)
        {
            var male = rand.Next(2) % 2 == 0;
            var personUser = new User()
            {
                Invited = rand.Next(2) % 2 == 0,
                Login = StringExtension.GenerateNewFile(),
                NoticeCommentPeriod = rand.Next(3) % 3 + 1,
                NoticeUpdatePeriod = rand.Next(3) % 3 + 1,
                Category = rand.Next(2) % 2
            };

            repository.CreateUser(personUser);

            var person = new Person()
            {
                AuthorID = personUser.ID,
                UserID = personUser.ID,
                Category = 0,
                Bio = Textarium.GetRandomHtmlText(4),
                FirstName = male ? NameGenerate.GetFemaleNameRandom() : NameGenerate.GetMaleNameRandom(),
                LastName = male ? NameGenerate.GetFemaleSurnameRandom() : NameGenerate.GetMaleSurnameRandom(),
                Patronymic = male ? NameGenerate.GetFemalePatronymicRandom() : NameGenerate.GetMalePatronymicRandom(),
                Photo = Imaginarium.SaveRandomImage("/Content/files/uploads/")
            };
            repository.CreatePerson(person);

            var userEmail = new UserEmail()
            {
                Email = EmailGenerate.GetRandom(person.FirstName, person.LastName),
                UserID = personUser.ID,
                Sended = true
            };

            repository.CreateUserEmail(userEmail);
            repository.ActivateUser(personUser);
            
            userEmail.ActivateDate = DateTime.Now;
            repository.UpdateUserEmail(userEmail);

            MakePersonContact(repository, person);

            for (int j = 0; j < 2; j++)
            {
                MakePersonLink(repository, person);
            }
            SelectPersonSubjects(repository, person);
            SelectPersonRegions(repository, person);

            Console.WriteLine("Создана персона: " + person.FullName);
        }

        private static void SelectPersonSubjects(IRepository repository, Person person)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdatePersonSubject(person.ID, subjectList);
        }

        private static void SelectPersonRegions(IRepository repository, Person person)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdatePersonRegion(person.ID, regionList);
        }

        private static void MakePersonLink(IRepository repository, Person person)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomSocial(person.FirstName, person.LastName)
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Страница на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreatePersonLink(new PersonLink()
            {
                LinkID = link.ID,
                PersonID = person.ID
            });
        }

        private static void MakePersonContact(IRepository repository, Person person)
        {
            var contact = new Contact()
            {
                Type = (int)Contact.TypeEnum.Email,
                Value = EmailGenerate.GetRandom(person.FirstName, person.LastName)
            };
            repository.CreateContact(contact);
            repository.CreatePersonContact(new PersonContact()
            {
                PersonID = person.ID,
                ContactID = contact.ID
            });
            contact = new Contact()
            {
                Type = (int)Contact.TypeEnum.Phone,
                Value = PhoneGenerate.GetRandom()
            };
            repository.CreateContact(contact);
            repository.CreatePersonContact(new PersonContact()
            {
                PersonID = person.ID,
                ContactID = contact.ID
            });

            contact = new Contact()
            {
                Type = (int)Contact.TypeEnum.Skype,
                Value = SkypeGenerate.GetRandom(person.FirstName, person.LastName)
            };
            repository.CreateContact(contact);
            repository.CreatePersonContact(new PersonContact()
            {
                PersonID = person.ID,
                ContactID = contact.ID
            });
        }
        #endregion

        #region Event

        public void GenerateEvents(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeEvent(repository, user);
            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeEvent(IRepository repository, User user)
        {
            var male = rand.Next(2) % 2 == 0;
            var @event = new Event()
            {
                Header = EventGenerate.GetRandom(),
                Image = Imaginarium.SaveRandomImage("/Content/files/uploads/"),
                Teaser = Textarium.GetRandomText(3),
                EventDate = DateGenerate.GetRandom()
            };
            repository.CreateEvent(@event, user.ID);

            for (int j = 0; j < 2; j++)
            {
                MakeEventLink(repository, @event);
            }
            for (int j = 0; j < 2; j++)
            {
                MakeEventFile(repository, @event);
            }
            SelectEventSubjects(repository, @event);
            SelectEventRegions(repository, @event);
            SelectEventPerson(repository, @event);
            SelectEventOrganization(repository, @event);

            for (int j = 0; j < 15; j++)
            {
                CreateEventComment(repository, @event);
            }

            Console.WriteLine("Создано событие: " + @event.Header);
        }

        private static void SelectEventSubjects(IRepository repository, Event @event)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateEventSubject(@event.ID, subjectList);
        }

        private static void SelectEventPerson(IRepository repository, Event @event)
        {
            var smallRand = rand.Next(3) + 1;
            var personList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var person = repository.Persons.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                personList.Add(person.ID);
            }
            repository.UpdateEventPerson(@event.ID, personList);
        }

        private static void SelectEventOrganization(IRepository repository, Event @event)
        {
            var smallRand = rand.Next(3) + 1;
            var organizationList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var organization = repository.Organizations.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                organizationList.Add(organization.ID);
            }
            repository.UpdateEventOrganization(@event.ID, organizationList);
        }

        private static void SelectEventRegions(IRepository repository, Event @event)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdateEventRegion(@event.ID, regionList);
        }

        private static void MakeEventLink(IRepository repository, Event @event)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomLink()
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Страница на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreateEventLink(new EventLink()
            {
                LinkID = link.ID,
                EventID = @event.ID
            });
        }

        private static void MakeEventFile(IRepository repository, Event @event)
        {
            var config = DependencyResolver.Current.GetService<IConfig>();
            string name = string.Empty;

            var file = new Model.File()
            {
                Path = Filerarium.CopyRandomFile("/Content/files/attaches/", out name),
                Name = name,
                IsImage = false,
                MimeType = ""
            };
            var extension = Path.GetExtension(file.Name);
            var mimeType = config.MimeTypes.FirstOrDefault(p => string.Compare(p.Extension, extension, true) == 0);

            if (mimeType != null)
            {
                file.MimeType = mimeType.Name;
                file.IsImage = PreviewCreator.SupportMimeType(mimeType.Name);
            }

            repository.CreateFile(file);
            repository.CreateEventFile(new EventFile()
            {
                FileID = file.ID,
                EventID = @event.ID
            });
        }

        private static void CreateEventComment(IRepository repository, Event @event)
        {
            var randomUser = repository.Users.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var randomComment = repository.EventComments.Where(p => p.EventID == @event.ID).ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            
            var comment = new Comment()
            {
                UserID = randomUser.ID,
                Text = Textarium.GetRandomText(1),
            };
            if (randomComment != null)
            {
                comment.ParentID = rand.Next(2) % 2 == 0 ? null : (int?)randomComment.CommentID;
            }
            repository.CreateComment(comment);
            repository.CreateEventComment(new EventComment()
            {
                CommentID = comment.ID,
                EventID = @event.ID
            });
        }

        #endregion

        #region Document

        public void GenerateDocuments(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeDocument(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeDocument(IRepository repository, User user)
        {
            var selectEvent = repository.Events.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var document = new Document()
            {
                UserID = user.ID,
                EventID = selectEvent.ID,
                Header = DocumentGenerate.GetRandom(),
                Teaser = Textarium.GetRandomText(4),
                Content = Textarium.GetRandomHtmlText(40),
            };
            repository.CreateDocument(document);

            var male = rand.Next(2) % 2 == 0;
            var firstName = male ? NameGenerate.GetFemaleNameRandom() : NameGenerate.GetMaleNameRandom();
            var lastName = male ? NameGenerate.GetFemaleSurnameRandom() : NameGenerate.GetMaleSurnameRandom();

            for (int j = 0; j < 2; j++)
            {
                MakeDocumentLink(repository, document);
            }
            for (int j = 0; j < 2; j++)
            {
                MakeDocumentFile(repository, document);
            }
            SelectDocumentSubjects(repository, document);
            SelectDocumentRegions(repository, document);
            SelectDocumentOrganizations(repository, document);

            for (int j = 0; j < 15; j++)
            {
                CreateDocumentComment(repository, document);
            }

            Console.WriteLine("Создан документ: " + document.Header);
        }

        private static void MakeDocumentFile(IRepository repository, Document document)
        {
            var config = DependencyResolver.Current.GetService<IConfig>();
            string name = string.Empty;

            var file = new Model.File()
            {
                Path = Filerarium.CopyRandomFile("/Content/files/attaches/", out name),
                Name = name,
                IsImage = false,
                MimeType = ""
            }; 
            var extension = Path.GetExtension(file.Name);
            var mimeType = config.MimeTypes.FirstOrDefault(p => string.Compare(p.Extension, extension, true) == 0);

            if (mimeType != null)
            {
                file.MimeType = mimeType.Name;
                file.IsImage = PreviewCreator.SupportMimeType(mimeType.Name);
            }

            repository.CreateFile(file);
            repository.CreateDocumentFile(new DocumentFile()
            {
                FileID = file.ID,
                DocumentID = document.ID
            });
        }

        private static void MakeDocumentLink(IRepository repository, Document document)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomLink()
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Страница на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreateDocumentLink(new DocumentLink()
            {
                LinkID = link.ID,
                DocumentID = document.ID
            });
        }

        private static void SelectDocumentSubjects(IRepository repository, Document document)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateDocumentSubject(document.ID, subjectList);
        }

        private static void SelectDocumentRegions(IRepository repository, Document document)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdateDocumentRegion(document.ID, regionList);
        }

        private static void SelectDocumentOrganizations(IRepository repository, Document document)
        {
            var smallRand = rand.Next(3) + 1;

            var organizationList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var organization = repository.Organizations.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                organizationList.Add(organization.ID);
            }
            repository.UpdateDocumentOrganization(document.ID, organizationList);
        }

        private static void CreateDocumentComment(IRepository repository, Document document)
        {
            var randomUser = repository.Users.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var randomComment = repository.DocumentComments.Where(p => p.DocumentID == document.ID).ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var comment = new Comment()
            {
                UserID = randomUser.ID,
                Text = Textarium.GetRandomText(1)
            };
            if (randomComment != null)
            {
                comment.ParentID = rand.Next(2) % 2 == 0 ? null : (int?)randomComment.CommentID;
            }
            repository.CreateComment(comment);
            repository.CreateDocumentComment(new DocumentComment()
            {
                CommentID = comment.ID,
                DocumentID = document.ID
            });
        }
        
        #endregion

        #region Publication

        public void GeneratePublications(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakePublication(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakePublication(IRepository repository, User user)
        {
            var mapper = DependencyResolver.Current.GetService<IMapper>();
            var selectEvent = repository.Events.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();

            var type = (Publication.TypeEnum)rand.Next(3) + 1;

            Publication publication = null;
            switch (type)
            {
                case Publication.TypeEnum.Book:
                    var bookPublicationView = new BookPublicationView()
                    {
                        Cover = Imaginarium.SaveRandomImage("/Content/files/uploads/"),
                        Bibliographic = Textarium.GetRandomText(2),
                        Header = PublicationGenerate.GetRandom(),
                        Year = DateGenerate.GetRandom().Year,
                        Content = Textarium.GetRandomHtmlText(20),
                        Teaser = Textarium.GetRandomText(3),
                    };
                    publication = (Publication)mapper.Map(bookPublicationView, typeof(BookPublicationView), typeof(Publication));
                    break;
                case Publication.TypeEnum.Article:
                    var parent = repository.Publications.Where(p => p.Type == (int)Publication.TypeEnum.Book).FirstOrDefault();
                    var articlePublicationView = new ArticlePublicationView()
                    {
                        Bibliographic = Textarium.GetRandomText(2),
                        Header = PublicationGenerate.GetRandom(),
                        Year = DateGenerate.GetRandom().Year,
                        Content = Textarium.GetRandomHtmlText(20),
                        Teaser = Textarium.GetRandomText(3),
                        ParentID = parent != null ? (int?)parent.ID : null,
                    };
                    publication = (Publication)mapper.Map(articlePublicationView, typeof(ArticlePublicationView), typeof(Publication));
                    break;
                case Publication.TypeEnum.Thesis:
                    var thesisPublicationView = new ThesisPublicationView()
                   {
                       Bibliographic = Textarium.GetRandomText(2),
                       Header = PublicationGenerate.GetRandom(),
                       Year = DateGenerate.GetRandom().Year,
                       Content = Textarium.GetRandomHtmlText(20),
                       Teaser = Textarium.GetRandomText(3),
                   };
                    publication = (Publication)mapper.Map(thesisPublicationView, typeof(ThesisPublicationView), typeof(Publication));
                    break;
            }

            publication.UserID = user.ID;
            repository.CreatePublication(publication);

            SelectPublicationPersons(repository, publication);

            var male = rand.Next(2) % 2 == 0;
            var firstName = male ? NameGenerate.GetFemaleNameRandom() : NameGenerate.GetMaleNameRandom();
            var lastName = male ? NameGenerate.GetFemaleSurnameRandom() : NameGenerate.GetMaleSurnameRandom();

            for (int j = 0; j < 2; j++)
            {
                MakePublicationLink(repository, publication);
            }
            for (int j = 0; j < 2; j++)
            {
                MakePublicationFile(repository, publication);
            }

            if (publication.Type == (int)Publication.TypeEnum.Book)
            {
                for (int j = 0; j < 2; j++)
                {
                    MakePublicationShopLink(repository, publication);
                }
            }
            if (publication.Type == (int)Publication.TypeEnum.Book || publication.Type == (int)Publication.TypeEnum.Thesis)
            {
                SelectPublicationOrganizations(repository, publication);
            }

            SelectPublicationRegions(repository, publication);
            SelectPublicationSubjects(repository, publication);

            for (int j = 0; j < 15; j++)
            {
                CreatePublicationComment(repository, publication);
            }

            Console.WriteLine("Создана публикация: " + publication.Header + " (" + publication.TypeStr + ")");
        }

        private static void MakePublicationFile(IRepository repository, Publication publication)
        {
            var config = DependencyResolver.Current.GetService<IConfig>();
            string name = string.Empty;

            var file = new Model.File()
            {
                Path = Filerarium.CopyRandomFile("/Content/files/attaches/", out name),
                Name = name,
                IsImage = false,
                MimeType = ""
            };
            var extension = Path.GetExtension(file.Name);
            var mimeType = config.MimeTypes.FirstOrDefault(p => string.Compare(p.Extension, extension, true) == 0);

            if (mimeType != null)
            {
                file.MimeType = mimeType.Name;
                file.IsImage = PreviewCreator.SupportMimeType(mimeType.Name);
            }

            repository.CreateFile(file);
            repository.CreatePublicationFile(new PublicationFile()
            {
                FileID = file.ID,
                PublicationID = publication.ID
            });
        }

        private static void MakePublicationLink(IRepository repository, Publication publication)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomLink()
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Страница на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreatePublicationLink(new PublicationLink()
            {
                LinkID = link.ID,
                PublicationID = publication.ID
            });
        }

        private static void MakePublicationShopLink(IRepository repository, Publication publication)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomLink()
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Купить на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreatePublicationLink(new PublicationLink()
            {
                LinkID = link.ID,
                IsShop = true,
                PublicationID = publication.ID
            });
        }

        private static void SelectPublicationPersons(IRepository repository, Publication publication)
        {
            var smallRand = rand.Next(3) + 1;
            var personList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var person = repository.Persons.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                personList.Add(person.ID);
            }
            repository.UpdatePublicationPerson(publication.ID, personList);
        }

        private static void SelectPublicationOrganizations(IRepository repository, Publication publication)
        {
            var smallRand = rand.Next(3) + 1;
            var organizationList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var organization = repository.Organizations.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                organizationList.Add(organization.ID);
            }
            repository.UpdatePublicationOrganization(publication.ID, organizationList);
        }

        private static void SelectPublicationSubjects(IRepository repository, Publication publication)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdatePublicationSubject(publication.ID, subjectList);
        }

        private static void SelectPublicationRegions(IRepository repository, Publication publication)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdatePublicationRegion(publication.ID, regionList);
        }

        private static void CreatePublicationComment(IRepository repository, Publication publication)
        {
            var randomUser = repository.Users.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var randomComment = repository.PublicationComments.Where(p => p.PublicationID == publication.ID).ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var comment = new Comment()
            {
                UserID = randomUser.ID,
                Text = Textarium.GetRandomText(1)
            };
            if (randomComment != null)
            {
                comment.ParentID = rand.Next(2) % 2 == 0 ? null : (int?)randomComment.CommentID;
            }
            repository.CreateComment(comment);
            repository.CreatePublicationComment(new PublicationComment()
            {
                CommentID = comment.ID,
                PublicationID = publication.ID
            });
        }

        #endregion

        #region WebLink

        public void GenerateWebLinks(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeWebLink(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeWebLink(IRepository repository, User user)
        {
            var webLink = new WebLink()
            {
                Url = LinkGenerate.GetRandomLink(),
                Name = StupidoNameGenerate.GetRandom(),
                Screenshot = Imaginarium.SaveRandomImage("/Content/files/uploads/"),
                Description = Textarium.GetRandomText(3)
            };

            repository.CreateWebLink(webLink, user.ID);
            SelectWebLinkRegions(repository, webLink);
            SelectWebLinkSubjects(repository, webLink);

            for (int j = 0; j < 15; j++)
            {
                CreateWebLinkComment(repository, webLink);
            }

            Console.WriteLine("Создан веб-ресурс: " + webLink.Name);
        }

        private static void SelectWebLinkRegions(IRepository repository, WebLink webLink)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateWebLinkSubject(webLink.ID, subjectList);
        }

        private static void SelectWebLinkSubjects(IRepository repository, WebLink webLink)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdateWebLinkRegion(webLink.ID, regionList);
        }

        private static void CreateWebLinkComment(IRepository repository, WebLink webLink)
        {
            var randomUser = repository.Users.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var randomComment = repository.WebLinkComments.Where(p => p.WebLinkID == webLink.ID).ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var comment = new Comment()
            {
                UserID = randomUser.ID,
                Text = Textarium.GetRandomText(1)
            };
            if (randomComment != null)
            {
                comment.ParentID = rand.Next(2) % 2 == 0 ? null : (int?)randomComment.CommentID;
            }
            repository.CreateComment(comment);
            repository.CreateWebLinkComment(new WebLinkComment()
            {
                CommentID = comment.ID,
                WebLinkID = webLink.ID
            });
        }

        #endregion

        #region StudyMaterial

        public void GenerateStudyMaterials(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeStudyMaterial(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeStudyMaterial(IRepository repository, User user)
        {
            var studyMaterial = new StudyMaterial()
            {
                UserID = user.ID,
                Name = PublicationGenerate.GetRandom(),
                Teaser = Textarium.GetRandomText(3),
                Content = Textarium.GetRandomHtmlText(20)
            };
            repository.CreateStudyMaterial(studyMaterial);

            var male = rand.Next(2) % 2 == 0;
            var firstName = male ? NameGenerate.GetFemaleNameRandom() : NameGenerate.GetMaleNameRandom();
            var lastName = male ? NameGenerate.GetFemaleSurnameRandom() : NameGenerate.GetMaleSurnameRandom();

            for (int j = 0; j < 2; j++)
            {
                MakeStudyMaterialLink(repository, studyMaterial);
            }
            for (int j = 0; j < 2; j++)
            {
                MakeStudyMaterialFile(repository, studyMaterial);
            }

            SelectStudyMaterialOrganizations(repository, studyMaterial);
            SelectStudyMaterialPersons(repository, studyMaterial);
            SelectStudyMaterialRegions(repository, studyMaterial);
            SelectStudyMaterialSubjects(repository, studyMaterial);

            for (int j = 0; j < 15; j++)
            {
                CreateStudyMaterialComment(repository, studyMaterial);
            }

            Console.WriteLine("Создан учебный материал: " + studyMaterial.Name);
        }

        private static void MakeStudyMaterialFile(IRepository repository, StudyMaterial studyMaterial)
        {
            var config = DependencyResolver.Current.GetService<IConfig>();
            string name = string.Empty;

            var file = new Model.File()
            {
                Path = Filerarium.CopyRandomFile("/Content/files/attaches/", out name),
                Name = name,
                IsImage = false,
                MimeType = ""
            };
            var extension = Path.GetExtension(file.Name);
            var mimeType = config.MimeTypes.FirstOrDefault(p => string.Compare(p.Extension, extension, true) == 0);

            if (mimeType != null)
            {
                file.MimeType = mimeType.Name;
                file.IsImage = PreviewCreator.SupportMimeType(mimeType.Name);
            }

            repository.CreateFile(file);
            repository.CreateStudyMaterialFile(new StudyMaterialFile()
            {
                FileID = file.ID,
                StudyMaterialID = studyMaterial.ID
            });
        }

        private static void MakeStudyMaterialLink(IRepository repository, StudyMaterial studyMaterial)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomLink()
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Страница на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreateStudyMaterialLink(new StudyMaterialLink()
            {
                LinkID = link.ID,
                StudyMaterialID = studyMaterial.ID
            });
        }

        private static void SelectStudyMaterialPersons(IRepository repository, StudyMaterial studyMaterial)
        {
            var smallRand = rand.Next(3) + 1;
            var personList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var person = repository.Persons.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                personList.Add(person.ID);
            }
            repository.UpdateStudyMaterialPerson(studyMaterial.ID, personList);
        }

        private static void SelectStudyMaterialOrganizations(IRepository repository, StudyMaterial studyMaterial)
        {
            var smallRand = rand.Next(3) + 1;
            var organizationList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var organization = repository.Organizations.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                organizationList.Add(organization.ID);
            }
            repository.UpdateStudyMaterialOrganization(studyMaterial.ID, organizationList);
        }

        private static void SelectStudyMaterialSubjects(IRepository repository, StudyMaterial studyMaterial)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateStudyMaterialSubject(studyMaterial.ID, subjectList);
        }

        private static void SelectStudyMaterialRegions(IRepository repository, StudyMaterial studyMaterial)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdateStudyMaterialRegion(studyMaterial.ID, regionList);
        }

        private static void CreateStudyMaterialComment(IRepository repository, StudyMaterial studyMaterial)
        {
            var randomUser = repository.Users.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var randomComment = repository.StudyMaterialComments.Where(p => p.StudyMaterialID == studyMaterial.ID).ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var comment = new Comment()
            {
                UserID = randomUser.ID,
                Text = Textarium.GetRandomText(1)
            };
            if (randomComment != null)
            {
                comment.ParentID = rand.Next(2) % 2 == 0 ? null : (int?)randomComment.CommentID;
            }
            repository.CreateComment(comment);
            repository.CreateStudyMaterialComment(new StudyMaterialComment()
            {
                CommentID = comment.ID,
                StudyMaterialID = studyMaterial.ID
            });
        }

        #endregion

        #region Image

        public void GenerateImages(int count = 10)
        {
            var repository = DependencyResolver.Current.GetService<IRepository>();

            var user = repository.Users.First();
            for (int i = 0; i < count; i++)
            {
                MakeImage(repository, user);

            }
            Assert.AreEqual(0, 0);
        }

        private static void MakeImage(IRepository repository, User user)
        {
            var image = new Image()
            {
                Path = Imaginarium.SaveRandomImage("/Content/files/uploads/"),
                Header = StupidoNameGenerate.GetRandom(),
                Description = Textarium.GetRandomText(1),
            };
            repository.CreateImage(image, user.ID);


            for (int j = 0; j < 2; j++)
            {
                MakeImageLink(repository, image);
            }

            SelectImagePerson(repository, image);
            SelectImageRegions(repository, image);
            SelectImageSubjects(repository, image);

            for (int j = 0; j < 15; j++)
            {
                CreateImageComment(repository, image);
            }

            Console.WriteLine("Создано изображение: " + image.Header);
        }

        private static void MakeImageLink(IRepository repository, Image image)
        {
            var link = new Link()
            {
                Url = LinkGenerate.GetRandomLink()
            };

            link.Icon = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
            link.Url.GetFavicon(Filerarium.MakeAbsFolder(link.Icon));
            link.Title = "Изображение на " + link.Url.Domain();

            repository.CreateLink(link);
            repository.CreateImageLink(new ImageLink()
            {
                LinkID = link.ID,
                ImageID = image.ID
            });
        }

        private static void SelectImagePerson(IRepository repository, Image image)
        {
            var smallRand = rand.Next(3) + 1;
            var personList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var person = repository.Persons.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                personList.Add(person.ID);
            }
            repository.UpdateImagePerson(image.ID, personList);
        }

        private static void SelectImageSubjects(IRepository repository, Image image)
        {
            var smallRand = rand.Next(3) + 1;
            var subjectList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var subject = repository.Subjects.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                subjectList.Add(subject.ID);
            }
            repository.UpdateImageSubject(image.ID, subjectList);
        }

        private static void SelectImageRegions(IRepository repository, Image image)
        {
            var smallRand = rand.Next(3) + 1;

            var regionList = new List<int>();
            for (int j = 0; j < smallRand; j++)
            {
                var region = repository.Regions.ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
                regionList.Add(region.ID);
            }
            repository.UpdateImageRegion(image.ID, regionList);
        }

        private static void CreateImageComment(IRepository repository, Image image)
        {
            var randomUser = repository.Users.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var randomComment = repository.ImageComments.Where(p => p.ImageID == image.ID).ToList().OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            var comment = new Comment()
            {
                UserID = randomUser.ID,
                Text = Textarium.GetRandomText(1)
            };
            if (randomComment != null)
            {
                comment.ParentID = rand.Next(2) % 2 == 0 ? null : (int?)randomComment.CommentID;
            }
            repository.CreateComment(comment);
            repository.CreateImageComment(new ImageComment()
            {
                CommentID = comment.ID,
                ImageID = image.ID
            });
        }

        #endregion
    }
}
