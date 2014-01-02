using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkLike> WebLinkLikes
        {
            get
            {
                return Db.WebLinkLikes;
            }
        }

        public bool CreateWebLinkLike(WebLinkLike instance)
        {
            if (instance.ID == 0)
            {
                Db.WebLinkLikes.InsertOnSubmit(instance);
                Db.WebLinkLikes.Context.SubmitChanges();
                RecalculateWebLinkLikes(instance.WebLinkID);
                return true;
            }

            return false;
        }

        public bool RemoveWebLinkLike(int idWebLinkLike)
        {
            var instance = Db.WebLinkLikes.FirstOrDefault(p => p.ID == idWebLinkLike);
            if (instance != null)
            {
                var webLinkID = instance.WebLinkID;

                Db.WebLinkLikes.DeleteOnSubmit(instance);
                Db.WebLinkLikes.Context.SubmitChanges();
                RecalculateWebLinkLikes(webLinkID);

                return true;
            }
            return false;
        }

        private void RecalculateWebLinkLikes(int webLinkID)
        {
            var cache = Db.WebLinks.FirstOrDefault(p => p.ID == webLinkID);

            if (cache != null)
            {
                var list = Db.WebLinkLikes.Where(p => p.WebLinkID == webLinkID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.WebLinkLikes.Context.SubmitChanges();
            }
        }
    }
}