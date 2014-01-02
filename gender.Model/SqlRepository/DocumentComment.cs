using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentComment> DocumentComments
        {
            get
            {
                return Db.DocumentComments;
            }
        }

        public bool CreateDocumentComment(DocumentComment instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentComments.InsertOnSubmit(instance);
                Db.DocumentComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateDocumentComment(DocumentComment instance)
        {
            var cache = Db.DocumentComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.DocumentID = instance.DocumentID;
				cache.CommentID = instance.CommentID;
                Db.DocumentComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveDocumentComment(int idDocumentComment)
        {
            DocumentComment instance = Db.DocumentComments.FirstOrDefault(p => p.ID == idDocumentComment);
            if (instance != null)
            {
                Db.DocumentComments.DeleteOnSubmit(instance);
                Db.DocumentComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}