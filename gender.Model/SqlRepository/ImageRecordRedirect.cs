using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageRecordRedirect> ImageRecordRedirects
        {
            get
            {
                return Db.ImageRecordRedirects;
            }
        }

        public bool CreateImageRecordRedirect(ImageRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.ImageRecordRedirects.InsertOnSubmit(instance);
                Db.ImageRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateImageRecordRedirect(ImageRecordRedirect instance)
        {
            var cache = Db.ImageRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ImageID = instance.ImageID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.ImageRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveImageRecordRedirect(int idImageRecordRedirect)
        {
            ImageRecordRedirect instance = Db.ImageRecordRedirects.FirstOrDefault(p => p.ID == idImageRecordRedirect);
            if (instance != null)
            {
                Db.ImageRecordRedirects.DeleteOnSubmit(instance);
                Db.ImageRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}