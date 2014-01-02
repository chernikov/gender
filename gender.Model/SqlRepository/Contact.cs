using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Contact> Contacts
        {
            get
            {
                return Db.Contacts;
            }
        }

        public bool CreateContact(Contact instance)
        {
            if (instance.ID == 0)
            {
                Db.Contacts.InsertOnSubmit(instance);
                Db.Contacts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateContact(Contact instance)
        {
            var cache = Db.Contacts.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Type = instance.Type;
				cache.Value = instance.Value;
                Db.Contacts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveContact(int idContact)
        {
            Contact instance = Db.Contacts.FirstOrDefault(p => p.ID == idContact);
            if (instance != null)
            {
                Db.Contacts.DeleteOnSubmit(instance);
                Db.Contacts.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}