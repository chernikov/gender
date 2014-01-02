using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventLink> EventLinks
        {
            get
            {
                return Db.EventLinks;
            }
        }

        public bool CreateEventLink(EventLink instance)
        {
            if (instance.ID == 0)
            {
                Db.EventLinks.InsertOnSubmit(instance);
                Db.EventLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

     
        public bool ClearEventLinks(int eventID)
        {
            var listForDelete = Db.EventLinks.Where(p => p.EventID == eventID);

            var linksForDelete = listForDelete.Select(p => p.Link);

            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.EventLinks.DeleteAllOnSubmit(listForDelete);
            Db.EventLinks.Context.SubmitChanges();
            return true;
        }
    }
}