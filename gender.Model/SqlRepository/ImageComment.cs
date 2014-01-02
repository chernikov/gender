using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageComment> ImageComments
        {
            get
            {
                return Db.ImageComments;
            }
        }

        public bool CreateImageComment(ImageComment instance)
        {
            if (instance.ID == 0)
            {
                Db.ImageComments.InsertOnSubmit(instance);
                Db.ImageComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateImageComment(ImageComment instance)
        {
            var cache = Db.ImageComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ImageID = instance.ImageID;
				cache.CommentID = instance.CommentID;
                Db.ImageComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveImageComment(int idImageComment)
        {
            ImageComment instance = Db.ImageComments.FirstOrDefault(p => p.ID == idImageComment);
            if (instance != null)
            {
                Db.ImageComments.DeleteOnSubmit(instance);
                Db.ImageComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}