using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationComment> PublicationComments
        {
            get
            {
                return Db.PublicationComments;
            }
        }

        public bool CreatePublicationComment(PublicationComment instance)
        {
            if (instance.ID == 0)
            {
                Db.PublicationComments.InsertOnSubmit(instance);
                Db.PublicationComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePublicationComment(PublicationComment instance)
        {
            var cache = Db.PublicationComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.PublicationID = instance.PublicationID;
				cache.CommentID = instance.CommentID;
                Db.PublicationComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemovePublicationComment(int idPublicationComment)
        {
            PublicationComment instance = Db.PublicationComments.FirstOrDefault(p => p.ID == idPublicationComment);
            if (instance != null)
            {
                Db.PublicationComments.DeleteOnSubmit(instance);
                Db.PublicationComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}