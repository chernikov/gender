using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace gender.Tools
{
    public class Subscription
    {
        public static void NewUser(IRepository repository, int userID, string host = "http://gender.ru/")
        {
            var moderators = repository.Users.Where(p => p.UserRoles.Any(r => r.Role.Code == "moderator" || r.Role.Code == "admin")).ToList();

            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.NewUser);
            var user = repository.Users.FirstOrDefault(p => p.ID == userID);
            if (moderators.Any() && template != null && user != null && user.Person != null)
            {
                foreach (var moderator in moderators)
                {
                    repository.CreateSubscriptionPart(new SubscriptionPart()
                    {
                        UserID = moderator.ID,
                        AddedDate = DateTime.Now,
                        IsProcessed = false,
                        UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Immediate,
                        Text = string.Format(template.Template,
                            host + "/user/" + user.Person.Url,
                            user.Person.FullName)
                    });
                }
            }
        }

        public static void GiveRight(IRepository repository, IAccess access, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.GiveRights);
            if (template != null)
            {
                var material = access.Material;

                repository.CreateSubscriptionPart(new SubscriptionPart()
                {
                    AddedDate = DateTime.Now,
                    IsProcessed = false,
                    UserID = access.User.ID,
                    UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                    Text = string.Format(template.Template,

                        host + access.Material.TypeUrl,
                        access.Material.Url,
                        access.Material.Name,
                        access.Material.MaterialType,
                        access.User.PrimaryEmail.ActivateLink)
                });
            }
        }

        public static void AddSubject(IRepository repository, List<int> subjects, IMaterial material, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.AddSubject);
            if (template != null)
            {
                var dictionary = GetUserSubjects(repository, subjects);
                foreach (var userSubject in dictionary)
                {
                    repository.CreateSubscriptionPart(new SubscriptionPart()
                    {
                        AddedDate = DateTime.Now,
                        IsProcessed = false,
                        UserID = userSubject.Key.ID,
                        UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                        Text = string.Format(template.Template,
                            userSubject.Value.Name,
                            host + material.TypeUrl,
                            material.Url,
                            material.Name,
                            material.MaterialType)
                    });
                }
            }
        }

        public static void AddMaterial(IRepository repository, List<int> subjects, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            AddMaterialSubscribers(repository, subjects, material, authors, host);
            if (authors != null)
            {
                AddMaterialAuthors(repository, material, authors, host);
            }
        }

        private static void AddMaterialSubscribers(IRepository repository, List<int> subjects, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.AddMaterialSubscribers);

            if (template != null)
            {
                var dictionary = GetUserSubjects(repository, subjects);
                foreach (var userSubject in dictionary)
                {
                    if (authors == null || !authors.Contains(userSubject.Key.Person))
                    {
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = userSubject.Key.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                            Text = string.Format(template.Template,
                                material.MaterialType,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                userSubject.Value.Name)
                        });
                    }
                }
            }
        }

        private static void AddMaterialAuthors(IRepository repository, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.AddMaterialAuthor);

            if (template != null)
            {
                foreach (var author in authors)
                {
                    var person = repository.Persons.FirstOrDefault(p => p.ID == author.ID);
                    if (person != null && person.SiteUser != null)
                    {
                        var user = person.SiteUser;
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = user.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                            Text = string.Format(template.Template,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                material.ID,
                                user.PrimaryEmail.ActivateLink)
                        });
                    }
                }
            }
        }

        public static void AddMaterialText(IRepository repository, List<int> subjects, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            AddMaterialTextSubscribers(repository, subjects, material, authors);
            if (authors != null)
            {
                AddMaterialTextAuthors(repository, material, authors, host);
            }
        }

        private static void AddMaterialTextSubscribers(IRepository repository, List<int> subjects, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.NewFileSubscribers);

            if (template != null)
            {
                var dictionary = GetUserSubjects(repository, subjects);
                foreach (var userSubject in dictionary)
                {
                    if (authors == null || !authors.Contains(userSubject.Key.Person))
                    {
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = userSubject.Key.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                            Text = string.Format(template.Template,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                userSubject.Value.Name)
                        });
                    }
                }
            }
        }

        private static void AddMaterialTextAuthors(IRepository repository, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.NewFileAuthor);

            if (template != null)
            {
                foreach (var author in authors)
                {
                    var person = repository.Persons.FirstOrDefault(p => p.ID == author.ID);
                    if (person != null && person.SiteUser != null)
                    {
                        var user = person.SiteUser;
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = user.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                            Text = string.Format(template.Template,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                material.ID,
                                user.PrimaryEmail.ActivateLink)
                        });
                    }
                }
            }
        }

        public static void AddMaterialFiles(IRepository repository, List<int> subjects, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            AddMaterialFilesSubscribers(repository, subjects, material, authors, host);
            if (authors != null)
            {
                AddMaterialFilesAuthors(repository, material, authors, host);
            }
        }

        private static void AddMaterialFilesSubscribers(IRepository repository, List<int> subjects, IMaterial material, List<Person> authors, string host)
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.NewFileSubscribers);

            if (template != null)
            {
                var dictionary = GetUserSubjects(repository, subjects);
                foreach (var userSubject in dictionary)
                {
                    if (authors == null || !authors.Contains(userSubject.Key.Person))
                    {
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = userSubject.Key.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                            Text = string.Format(template.Template,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                userSubject.Value.Name)
                        });
                    }
                }
            }
        }

        private static void AddMaterialFilesAuthors(IRepository repository, IMaterial material, List<Person> authors, string host = "http://gender.ru/")
        {
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.NewFileAuthor);

            if (template != null)
            {
                foreach (var author in authors)
                {
                    var person = repository.Persons.FirstOrDefault(p => p.ID == author.ID);
                    if (person != null && person.SiteUser != null)
                    {
                        var user = person.SiteUser;
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = user.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Update,
                            Text = string.Format(template.Template,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                material.ID,
                                user.PrimaryEmail.ActivateLink)
                        });
                    }
                }
            }
        }

        public static void NewComment(IRepository repository, ICommentable commentable, string host = "http://gender.ru/")
        {
            var material = commentable.Material;
            var template = repository.SubscriptionTemplates.FirstOrDefault(p => p.IsActive && p.Type == (int)SubscriptionTemplate.TypeEnum.NewComment);
            if (template != null)
            {
                if (material.CommentSubscribers != null)
                {
                    foreach (var user in material.CommentSubscribers)
                    {
                        repository.CreateSubscriptionPart(new SubscriptionPart()
                        {
                            AddedDate = DateTime.Now,
                            IsProcessed = false,
                            UserID = user.ID,
                            UpdateType = (int)SubscriptionPart.UpdateTypeEnum.Comment,
                            Text = string.Format(template.Template,
                                host + material.TypeUrl,
                                material.Url,
                                material.Name,
                                commentable.Comment.ID)
                        });
                    }
                }
            }
        }

        private static Dictionary<User, Subject> GetUserSubjects(IRepository repository, List<int> newSubjects)
        {
            var result = new Dictionary<User, Subject>();

            foreach (var subjectNum in newSubjects)
            {
                var subject = repository.Subjects.FirstOrDefault(p => p.ID == subjectNum);

                var userList = subject.SubjectSubscriptions.Select(p => p.User);

                foreach (var user in userList)
                {
                    if (!result.ContainsKey(user))
                    {
                        result.Add(user, subject);
                    }
                }
            }

            return result;
        }

        public static void Process(IRepository repository)
        {

            foreach (var user in repository.Users)
            {
                ProcessUpdates(repository, user);
                ProcessComments(repository, user);

            }
        }

        private static void ProcessUpdates(IRepository repository, User user)
        {
            //update
            var prepared = new List<SubscriptionPart>();
            switch ((User.NoticePeriodType)user.NoticeUpdatePeriod)
            {
                case User.NoticePeriodType.Immediately:
                    prepared = repository.SubscriptionParts.Where(p => p.UserID == user.ID
                        && !p.IsProcessed
                        && (p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Update ||
                        p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Immediate)).ToList();
                    break;
                case User.NoticePeriodType.OnceDay:
                    prepared = repository.SubscriptionParts.Where(p => p.UserID == user.ID
                        && !p.IsProcessed
                        && (p.AddedDate.Date < DateTime.Now.Date || p.AddedDate.Hour < 4)
                        && (p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Update ||
                        p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Immediate)).ToList();
                    break;
                case User.NoticePeriodType.TwiceWeek:
                    prepared = repository.SubscriptionParts.Where(p => p.UserID == user.ID
                        && !p.IsProcessed
                        && (p.AddedDate.Date < DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddHours(4)
                        || p.AddedDate < DateTime.Now.StartOfWeek(DayOfWeek.Thursday).AddHours(4))
                        && (p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Update ||
                        p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Immediate)).ToList();
                    break;
            }

            if (prepared.Count > 0)
            {
                var sb = new StringBuilder();
                var templateHeader = repository.SubscriptionTemplates.FirstOrDefault(p => p.Type == (int)SubscriptionTemplate.TypeEnum.Header);
                if (templateHeader != null)
                {
                    sb.AppendLine(string.Format(templateHeader.Template+"<br/>", user.Person.FullName));
                }
                foreach (var preparedLine in prepared)
                {
                    sb.AppendLine(preparedLine.Text + "<br/>");
                }
                var templateFooter = repository.SubscriptionTemplates.FirstOrDefault(p => p.Type == (int)SubscriptionTemplate.TypeEnum.Footer);
                if (templateFooter != null)
                {
                    sb.AppendLine(templateFooter.Template + "<br/>");
                }

                var mail = new Model.Mail()
                {
                    UserID = user.ID,
                    Email = user.PrimaryEmail.Email,
                    Subject = "Новое на Гендер.ру",
                    Body = sb.ToString(),
                    AddedDate = DateTime.Now
                };
                repository.PushMail(mail);
                repository.SetProcessSubscriptionPart(prepared);
            }
        }

        private static void ProcessComments(IRepository repository, User user)
        {
            //update
            var prepared = new List<SubscriptionPart>();
            switch ((User.NoticePeriodType)user.NoticeUpdatePeriod)
            {
                case User.NoticePeriodType.Immediately:
                    prepared = repository.SubscriptionParts.Where(p => p.UserID == user.ID
                        && !p.IsProcessed
                        && p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Comment).ToList();
                    break;
                case User.NoticePeriodType.OnceDay:
                    prepared = repository.SubscriptionParts.Where(p => p.UserID == user.ID
                        && !p.IsProcessed
                        && (p.AddedDate.Date < DateTime.Now.Date || p.AddedDate.Hour < 4)
                        && p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Comment).ToList();
                    break;
                case User.NoticePeriodType.TwiceWeek:
                    prepared = repository.SubscriptionParts.Where(p => p.UserID == user.ID
                        && !p.IsProcessed
                        && (p.AddedDate.Date < DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddHours(4)
                        || p.AddedDate < DateTime.Now.StartOfWeek(DayOfWeek.Thursday).AddHours(4))
                        && p.UpdateType == (int)SubscriptionPart.UpdateTypeEnum.Comment).ToList();
                    break;
            }
            if (prepared.Count > 0)
            {
                var sb = new StringBuilder();
                var templateHeader = repository.SubscriptionTemplates.FirstOrDefault(p => p.Type == (int)SubscriptionTemplate.TypeEnum.Header);
                if (templateHeader != null)
                {
                    sb.AppendLine(string.Format(templateHeader.Template, user.Person.FullName));
                }
                foreach (var preparedLine in prepared)
                {
                    sb.AppendLine(preparedLine.Text);
                }
                var templateFooter = repository.SubscriptionTemplates.FirstOrDefault(p => p.Type == (int)SubscriptionTemplate.TypeEnum.Footer);
                if (templateFooter != null)
                {
                    sb.AppendLine(templateFooter.Template);
                }
                var mail = new Model.Mail()
                {
                    UserID = user.ID,
                    Email = user.PrimaryEmail.Email,
                    Subject = "Новые комментарии на Гендер.ру",
                    Body = sb.ToString(),
                    AddedDate = DateTime.Now
                };
                repository.PushMail(mail);
                repository.SetProcessSubscriptionPart(prepared);
            }
        }
    }
}