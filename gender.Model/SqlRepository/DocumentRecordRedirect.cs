using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentRecordRedirect> DocumentRecordRedirects
        {
            get
            {
                return Db.DocumentRecordRedirects;
            }
        }

        public bool CreateDocumentRecordRedirect(DocumentRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentRecordRedirects.InsertOnSubmit(instance);
                Db.DocumentRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateDocumentRecordRedirect(DocumentRecordRedirect instance)
        {
            var cache = Db.DocumentRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.DocumentID = instance.DocumentID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.DocumentRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveDocumentRecordRedirect(int idDocumentRecordRedirect)
        {
            DocumentRecordRedirect instance = Db.DocumentRecordRedirects.FirstOrDefault(p => p.ID == idDocumentRecordRedirect);
            if (instance != null)
            {
                Db.DocumentRecordRedirects.DeleteOnSubmit(instance);
                Db.DocumentRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}