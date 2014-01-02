using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationFile> PublicationFiles
        {
            get
            {
                return Db.PublicationFiles;
            }
        }

        public bool CreatePublicationFile(PublicationFile instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationFiles.InsertOnSubmit(instance);
                Db.PublicationFiles.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearPublicationFiles(int publicationID)
        {
            var listForDelete = Db.PublicationFiles.Where(p => p.PublicationID == publicationID);
            var filesForDelete = listForDelete.Select(p => p.File);
            Db.Files.DeleteAllOnSubmit(filesForDelete);
            Db.PublicationFiles.DeleteAllOnSubmit(listForDelete);
            Db.PublicationFiles.Context.SubmitChanges();
            return true;
        }

    }
}