using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentLike> DocumentLikes
        {
            get
            {
                return Db.DocumentLikes;
            }
        }

        public bool CreateDocumentLike(DocumentLike instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentLikes.InsertOnSubmit(instance);
                Db.DocumentLikes.Context.SubmitChanges();
                RecalculateDocumentLikes(instance.DocumentID);
                return true;
            }

            return false;
        }

        public bool RemoveDocumentLike(int idDocumentLike)
        {
            var instance = Db.DocumentLikes.FirstOrDefault(p => p.ID == idDocumentLike);
            if (instance != null)
            {
                var documentID = instance.DocumentID;

                Db.DocumentLikes.DeleteOnSubmit(instance);
                Db.DocumentLikes.Context.SubmitChanges();
                RecalculateDocumentLikes(documentID);

                return true;
            }
            return false;
        }

        private void RecalculateDocumentLikes(int documentID)
        {
            var cache = Db.Documents.FirstOrDefault(p => p.ID == documentID);

            if (cache != null)
            {
                var list = Db.DocumentLikes.Where(p => p.DocumentID == documentID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.DocumentLikes.Context.SubmitChanges();
            }
        }

    }
}