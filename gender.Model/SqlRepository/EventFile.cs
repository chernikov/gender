using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<EventFile> EventFiles
        {
            get
            {
                return Db.EventFiles;
            }
        }

        public bool CreateEventFile(EventFile instance)
        {
            if (instance.ID == 0)
            {
                Db.EventFiles.InsertOnSubmit(instance);
                Db.EventFiles.Context.SubmitChanges();
                return true;
            }

            return false;
        }


        public bool ClearEventFiles(int eventID)
        {
            var listForDelete = Db.EventFiles.Where(p => p.EventID == eventID);
            var filesForDelete = listForDelete.Select(p => p.File);
            Db.Files.DeleteAllOnSubmit(filesForDelete);
            Db.EventFiles.DeleteAllOnSubmit(listForDelete);
            Db.EventFiles.Context.SubmitChanges();
            return true;
        }
    }
}