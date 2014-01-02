using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationLike> PublicationLikes
        {
            get
            {
                return Db.PublicationLikes;
            }
        }

        public bool CreatePublicationLike(PublicationLike instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationLikes.InsertOnSubmit(instance);
                Db.PublicationLikes.Context.SubmitChanges();
                RecalculatePublicationLikes(instance.PublicationID);
                return true;
            }

            return false;
        }

        public bool RemovePublicationLike(int idPublicationLike)
        {
            var instance = Db.PublicationLikes.FirstOrDefault(p => p.ID == idPublicationLike);
            if (instance != null)
            {
                var publicationID = instance.PublicationID;

                Db.PublicationLikes.DeleteOnSubmit(instance);
                Db.PublicationLikes.Context.SubmitChanges();
                RecalculatePublicationLikes(publicationID);

                return true;
            }
            return false;
        }

        private void RecalculatePublicationLikes(int publicationID)
        {
            var cache = Db.Publications.FirstOrDefault(p => p.ID == publicationID);

            if (cache != null)
            {
                var list = Db.PublicationLikes.Where(p => p.PublicationID == publicationID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.PublicationLikes.Context.SubmitChanges();
            }
        }
    }
}