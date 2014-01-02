using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventComment> EventComments
        {
            get
            {
                return Db.EventComments;
            }
        }

        public bool CreateEventComment(EventComment instance)
        {
            if (instance.ID == 0)
            {
                Db.EventComments.InsertOnSubmit(instance);
                Db.EventComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateEventComment(EventComment instance)
        {
            var cache = Db.EventComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.EventID = instance.EventID;
				cache.CommentID = instance.CommentID;
                Db.EventComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveEventComment(int idEventComment)
        {
            EventComment instance = Db.EventComments.FirstOrDefault(p => p.ID == idEventComment);
            if (instance != null)
            {
                Db.EventComments.DeleteOnSubmit(instance);
                Db.EventComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}