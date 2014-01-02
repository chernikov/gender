using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentFile> DocumentFiles
        {
            get
            {
                return Db.DocumentFiles;
            }
        }

        public bool CreateDocumentFile(DocumentFile instance)
        {
            if (instance.ID == 0)
            {
                Db.DocumentFiles.InsertOnSubmit(instance);
                Db.DocumentFiles.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ClearDocumentFiles(int documentID)
        {
            var listForDelete = Db.DocumentFiles.Where(p => p.DocumentID == documentID);
            var filesForDelete = listForDelete.Select(p => p.File);
            Db.Files.DeleteAllOnSubmit(filesForDelete);
            Db.DocumentFiles.DeleteAllOnSubmit(listForDelete);
            Db.DocumentFiles.Context.SubmitChanges();
            return true;
        }
    }
}