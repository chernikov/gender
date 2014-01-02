using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
     public IQueryable<Person> Persons
        {
            get
            {
                return Db.Persons;
            }
        }

        public bool CreatePerson(Person instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;
                var baseUrl = Translit.Translate(string.Format("{0} {1} {2}", instance.LastName, instance.FirstName, instance.Patronymic).Trim());
                var url = baseUrl;
                var i = 1;
                var exist = Db.Persons.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Persons.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;
                Db.Persons.InsertOnSubmit(instance);
                Db.Persons.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePerson(Person instance)
        {
            var cache = Db.Persons.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.FirstName = instance.FirstName;
				cache.LastName = instance.LastName;
				cache.Patronymic = instance.Patronymic;
                var baseUrl = Translit.Translate(string.Format("{0} {1} {2}", instance.LastName, instance.FirstName, instance.Patronymic).Trim());
                var url = baseUrl;
                var i = 1;
                var exist = Db.Persons.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Persons.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
                cache.Photo = instance.Photo;
				cache.Bio = instance.Bio;
				cache.Category = instance.Category;
                cache.ChangedDate = DateTime.Now;

				cache.ModeratedDate = instance.ModeratedDate;
                Db.Persons.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePerson(int idPerson)
        {
            Person instance = Db.Persons.FirstOrDefault(p => p.ID == idPerson);
            if (instance != null)
            {
                if (instance.UserID.HasValue)
                {
                    RemoveUser(instance.UserID.Value);
                }
                Db.Persons.DeleteOnSubmit(instance);
                Db.Persons.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModeratePerson(int idPerson)
        {
            Person instance = Db.Persons.FirstOrDefault(p => p.ID == idPerson);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.Persons.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}