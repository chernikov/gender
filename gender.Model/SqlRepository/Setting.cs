using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Setting> Settings
        {
            get
            {
                return Db.Settings;
            }
        }

        public bool CreateSetting(Setting instance)
        {
            if (instance.ID == 0)
            {
                Db.Settings.InsertOnSubmit(instance);
                Db.Settings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateSetting(Setting instance)
        {
            var cache = Db.Settings.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Name = instance.Name;
				cache.Value = instance.Value;
				cache.ValueInt = instance.ValueInt;
				cache.ValueDouble = instance.ValueDouble;
                Db.Settings.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveSetting(int idSetting)
        {
            Setting instance = Db.Settings.FirstOrDefault(p => p.ID == idSetting);
            if (instance != null)
            {
                Db.Settings.DeleteOnSubmit(instance);
                Db.Settings.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}