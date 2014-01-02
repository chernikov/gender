using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkComment> WebLinkComments
        {
            get
            {
                return Db.WebLinkComments;
            }
        }

        public bool CreateWebLinkComment(WebLinkComment instance)
        {
            if (instance.ID == 0)
            {
                Db.WebLinkComments.InsertOnSubmit(instance);
                Db.WebLinkComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateWebLinkComment(WebLinkComment instance)
        {
            var cache = Db.WebLinkComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.WebLinkID = instance.WebLinkID;
				cache.CommentID = instance.CommentID;
                Db.WebLinkComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveWebLinkComment(int idWebLinkComment)
        {
            WebLinkComment instance = Db.WebLinkComments.FirstOrDefault(p => p.ID == idWebLinkComment);
            if (instance != null)
            {
                Db.WebLinkComments.DeleteOnSubmit(instance);
                Db.WebLinkComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}