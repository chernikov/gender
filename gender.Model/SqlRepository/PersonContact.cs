using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PersonContact> PersonContacts
        {
            get
            {
                return Db.PersonContacts;
            }
        }

        public bool CreatePersonContact(PersonContact instance)
        {
            if (instance.ID == 0)
            {
                Db.PersonContacts.InsertOnSubmit(instance);
                Db.PersonContacts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearPersonContacts(int personID)
        {
            var listForDelete = Db.PersonContacts.Where(p => p.PersonID == personID);

            var contactsForDelete = listForDelete.Select(p => p.Contact);

            Db.Contacts.DeleteAllOnSubmit(contactsForDelete);
            Db.PersonContacts.DeleteAllOnSubmit(listForDelete);
            Db.PersonContacts.Context.SubmitChanges();
            return true;
        }
    }
}