using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<OrganizationContact> OrganizationContacts
        {
            get
            {
                return Db.OrganizationContacts;
            }
        }

        public bool CreateOrganizationContact(OrganizationContact instance)
        {
            if (instance.ID == 0)
            {
                Db.OrganizationContacts.InsertOnSubmit(instance);
                Db.OrganizationContacts.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearOrganizationContacts(int organizationID)
        {
            var listForDelete = Db.OrganizationContacts.Where(p => p.OrganizationID == organizationID);

            var contactsForDelete = listForDelete.Select(p => p.Contact);

            Db.Contacts.DeleteAllOnSubmit(contactsForDelete);
            Db.OrganizationContacts.DeleteAllOnSubmit(listForDelete);
            Db.OrganizationContacts.Context.SubmitChanges();
            return true;
        }
    }
}