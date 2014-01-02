using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PersonLink> PersonLinks
        {
            get
            {
                return Db.PersonLinks;
            }
        }

        public bool CreatePersonLink(PersonLink instance)
        {
            if (instance.ID == 0)
            {
                Db.PersonLinks.InsertOnSubmit(instance);
                Db.PersonLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearPersonLinks(int personID)
        {
            var listForDelete = Db.PersonLinks.Where(p => p.PersonID == personID);

            var linksForDelete = listForDelete.Select(p => p.Link);

            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.PersonLinks.DeleteAllOnSubmit(listForDelete);
            Db.PersonLinks.Context.SubmitChanges();
            return true;
        }
    }
}