using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageLike> ImageLikes
        {
            get
            {
                return Db.ImageLikes;
            }
        }

        public bool CreateImageLike(ImageLike instance)
        {
            if (instance.ID == 0)
            {
                Db.ImageLikes.InsertOnSubmit(instance);
                Db.ImageLikes.Context.SubmitChanges();
                RecalculateImageLikes(instance.ImageID);
                return true;
            }

            return false;
        }

        public bool RemoveImageLike(int idImageLike)
        {
            var instance = Db.ImageLikes.FirstOrDefault(p => p.ID == idImageLike);
            if (instance != null)
            {
                var imageID = instance.ImageID;

                Db.ImageLikes.DeleteOnSubmit(instance);
                Db.ImageLikes.Context.SubmitChanges();
                RecalculateImageLikes(imageID);

                return true;
            }
            return false;
        }

        private void RecalculateImageLikes(int imageID)
        {
            var cache = Db.Images.FirstOrDefault(p => p.ID == imageID);

            if (cache != null)
            {
                var list = Db.ImageLikes.Where(p => p.ImageID == imageID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.ImageLikes.Context.SubmitChanges();
            }
        }
    }
}