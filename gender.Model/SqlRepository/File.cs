using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<File> Files
        {
            get
            {
                return Db.Files;
            }
        }

        public bool CreateFile(File instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.Files.InsertOnSubmit(instance);
                Db.Files.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveFile(int idFile)
        {
            File instance = Db.Files.FirstOrDefault(p => p.ID == idFile);
            if (instance != null)
            {
                Db.Files.DeleteOnSubmit(instance);
                Db.Files.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}