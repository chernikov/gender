using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<UpdateRecord> UpdateRecords
        {
            get
            {
                return Db.UpdateRecords;
            }
        }

        public bool CreateUpdateRecord(UpdateRecord instance)
        {
            if (instance.ID == 0)
            {
                Db.UpdateRecords.InsertOnSubmit(instance);
                Db.UpdateRecords.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateUpdateRecord(UpdateRecord instance)
        {
            var cache = Db.UpdateRecords.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.AddedDate = instance.AddedDate;
				cache.UserID = instance.UserID;
				cache.Type = instance.Type;
				cache.MaterialType = instance.MaterialType;
				cache.ResourceID = instance.ID;
                Db.UpdateRecords.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveUpdateRecord(int idUpdateRecord)
        {
            UpdateRecord instance = Db.UpdateRecords.FirstOrDefault(p => p.ID == idUpdateRecord);
            if (instance != null)
            {
                Db.UpdateRecords.DeleteOnSubmit(instance);
                Db.UpdateRecords.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}