using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentSubscription> DocumentSubscriptions
        {
            get
            {
                return Db.DocumentSubscriptions;
            }
        }

        public bool CreateDocumentSubscription(DocumentSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentSubscriptions.InsertOnSubmit(instance);
                Db.DocumentSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateDocumentSubscription(DocumentSubscription instance)
        {
            var cache = Db.DocumentSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.DocumentID = instance.DocumentID;
				cache.UserID = instance.UserID;
                Db.DocumentSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveDocumentSubscription(int idDocumentSubscription)
        {
            DocumentSubscription instance = Db.DocumentSubscriptions.FirstOrDefault(p => p.ID == idDocumentSubscription);
            if (instance != null)
            {
                Db.DocumentSubscriptions.DeleteOnSubmit(instance);
                Db.DocumentSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}