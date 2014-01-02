using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentLink> DocumentLinks
        {
            get
            {
                return Db.DocumentLinks;
            }
        }

        public bool CreateDocumentLink(DocumentLink instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentLinks.InsertOnSubmit(instance);
                Db.DocumentLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearDocumentLinks(int DocumentID)
        {
            var listForDelete = Db.DocumentLinks.Where(p => p.DocumentID == DocumentID);
            var linksForDelete = listForDelete.Select(p => p.Link);
            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.DocumentLinks.DeleteAllOnSubmit(listForDelete);
            Db.DocumentLinks.Context.SubmitChanges();
            return true;

        }
       
    }
}