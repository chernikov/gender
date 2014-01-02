using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<User> Users
        {
            get
            {
                return Db.Users;
            }
        }

        public bool CreateUser(User instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.LastVisitDate = DateTime.Now;
                instance.ActivatedLink = StringExtension.GenerateNewFile();
                instance.NoticeCommentPeriod = (int)User.NoticePeriodType.OnceDay;
                instance.NoticeUpdatePeriod = (int)User.NoticePeriodType.OnceDay;
                Db.Users.InsertOnSubmit(instance);
                Db.Users.Context.SubmitChanges();
                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.Person,
                    Type = (int)UpdateRecord.TypeEnum.NewUser,
                    AddedDate = DateTime.Now,
                    UserID = instance.ID
                });

                //create blog 
                var blog = new Blog()
                {
                    UserID = instance.ID,
                    LastUpdate = DateTime.Now,
                };
                CreateBlog(blog);
                return true;
            }

            return false;
        }

        public bool UpdateUser(User instance)
        {
            var cache = Db.Users.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Invited = instance.Invited;
                cache.Category = instance.Category;
				cache.StartRating = instance.StartRating;
                cache.NoticeCommentPeriod = instance.NoticeCommentPeriod;
                cache.NoticeUpdatePeriod = instance.NoticeUpdatePeriod;
                Db.Users.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUser(int idUser)
        {
            User instance = Db.Users.FirstOrDefault(p => p.ID == idUser);
            if (instance != null)
            {
                if (instance.Person != null)
                {
                    foreach (var person in instance.Persons1.ToList())
                    {
                        person.UserID = null;
                        Db.Persons.Context.SubmitChanges();
                    }
                }
                if (instance.Persons.Any())
                {
                    Db.Persons.DeleteAllOnSubmit(instance.Persons.ToList());
                    Db.Persons.Context.SubmitChanges();
                }
                Db.Users.DeleteOnSubmit(instance);
                Db.Users.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public User GetUser(string login)
        {
            return Db.Users.FirstOrDefault(p => string.Compare(p.Login, login, true) == 0);
        }

        public User Login(string email, string password)
        {
            
            return Db.Users.FirstOrDefault(p => p.UserEmails.Any(r => string.Compare(r.Email, email, true) == 0) && p.Password == password);
        }

        public bool ActivateUser(User instance)
        {
            var cache = Db.Users.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.ActivatedDate = DateTime.Now;
                Db.Users.Context.SubmitChanges();
                return true;
            }

            return false;
        }


        public bool ChangePassword(User instance)
        {
            var cache = Db.Users.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Password = instance.Password;
                Db.Users.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateUserRating(User instance)
        {
            var cache = Db.Users.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Rating = instance.Rating;
                if (cache.Rating >= 5)
                {
                    cache.Category = (int)User.CategoryType.Privilege;
                }
                Db.Users.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}