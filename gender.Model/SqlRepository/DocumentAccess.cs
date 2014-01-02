using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentAccess> DocumentAccesses
        {
            get
            {
                return Db.DocumentAccesses;
            }
        }

        public bool CreateDocumentAccess(DocumentAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentAccesses.InsertOnSubmit(instance);
                Db.DocumentAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateDocumentAccess(DocumentAccess instance)
        {
            var cache = Db.DocumentAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.DocumentID = instance.DocumentID;
				cache.UserID = instance.UserID;
                Db.DocumentAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveDocumentAccess(int idDocumentAccess)
        {
            DocumentAccess instance = Db.DocumentAccesses.FirstOrDefault(p => p.ID == idDocumentAccess);
            if (instance != null)
            {
                Db.DocumentAccesses.DeleteOnSubmit(instance);
                Db.DocumentAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}