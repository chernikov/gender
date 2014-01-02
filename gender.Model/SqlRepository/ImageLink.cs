using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImageLink> ImageLinks
        {
            get
            {
                return Db.ImageLinks;
            }
        }

        public bool CreateImageLink(ImageLink instance)
        {
            if (instance.ID == 0)
            {
                Db.ImageLinks.InsertOnSubmit(instance);
                Db.ImageLinks.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ClearImageLinks(int imageID)
        {
            var listForDelete = Db.ImageLinks.Where(p => p.ImageID == imageID);

            var linksForDelete = listForDelete.Select(p => p.Link);

            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.ImageLinks.DeleteAllOnSubmit(listForDelete);
            Db.ImageLinks.Context.SubmitChanges();
            return true;
        }

        public bool UpdateImageLink(ImageLink instance)
        {
            var cache = Db.ImageLinks.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ImageID = instance.ImageID;
				cache.LinkID = instance.LinkID;
                Db.ImageLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveImageLink(int idImageLink)
        {
            ImageLink instance = Db.ImageLinks.FirstOrDefault(p => p.ID == idImageLink);
            if (instance != null)
            {
                Db.ImageLinks.DeleteOnSubmit(instance);
                Db.ImageLinks.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}