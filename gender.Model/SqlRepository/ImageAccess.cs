using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageAccess> ImageAccesses
        {
            get
            {
                return Db.ImageAccesses;
            }
        }

        public bool CreateImageAccess(ImageAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.ImageAccesses.InsertOnSubmit(instance);
                Db.ImageAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateImageAccess(ImageAccess instance)
        {
            var cache = Db.ImageAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ImageID = instance.ImageID;
				cache.UserID = instance.UserID;
                Db.ImageAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveImageAccess(int idImageAccess)
        {
            ImageAccess instance = Db.ImageAccesses.FirstOrDefault(p => p.ID == idImageAccess);
            if (instance != null)
            {
                Db.ImageAccesses.DeleteOnSubmit(instance);
                Db.ImageAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}