using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationLink> PublicationLinks
        {
            get
            {
                return Db.PublicationLinks;
            }
        }

        public bool CreatePublicationLink(PublicationLink instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationLinks.InsertOnSubmit(instance);
                Db.PublicationLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearPublicationLinks(int publicationID) 
        {

            var listForDelete = Db.PublicationLinks.Where(p => p.PublicationID == publicationID);
            var linksForDelete = listForDelete.Select(p => p.Link);
            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.PublicationLinks.DeleteAllOnSubmit(listForDelete);
            Db.PublicationLinks.Context.SubmitChanges();
            return true;

        }

    }
}