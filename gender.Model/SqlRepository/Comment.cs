using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Comment> Comments
        {
            get
            {
                return Db.Comments;
            }
        }

        public bool CreateComment(Comment instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                Db.Comments.InsertOnSubmit(instance);
                Db.Comments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateComment(Comment instance)
        {
            var cache = Db.Comments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.ParentID = instance.ParentID;
				cache.UserID = instance.UserID;
				cache.Text = instance.Text;
				cache.ModeratedDate = instance.ModeratedDate;
				cache.IsBanned = instance.IsBanned;
                Db.Comments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveComment(int idComment)
        {
            Comment instance = Db.Comments.FirstOrDefault(p => p.ID == idComment);
            if (instance != null)
            {
                Db.Comments.DeleteOnSubmit(instance);
                Db.Comments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}