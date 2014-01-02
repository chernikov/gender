using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<CommentLike> CommentLikes
        {
            get
            {
                return Db.CommentLikes;
            }
        }

        public bool CreateCommentLike(CommentLike instance)
        {
            if (instance.ID == 0)
            {
                Db.CommentLikes.InsertOnSubmit(instance);
                Db.CommentLikes.Context.SubmitChanges();
                RecalculateCommentLikes(instance.CommentID);
                return true;
            }

            return false;
        }

        public bool RemoveCommentLike(int idCommentLike)
        {
            var instance = Db.CommentLikes.FirstOrDefault(p => p.ID == idCommentLike);
            if (instance != null)
            {
                var commentID = instance.CommentID;

                Db.CommentLikes.DeleteOnSubmit(instance);
                Db.CommentLikes.Context.SubmitChanges();
                RecalculateCommentLikes(commentID);

                return true;
            }
            return false;
        }

        private void RecalculateCommentLikes(int commentID)
        {
            var cache = Db.Comments.FirstOrDefault(p => p.ID == commentID);

            if (cache != null)
            {
                var list = Db.CommentLikes.Where(p => p.CommentID == commentID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.CommentLikes.Context.SubmitChanges();
            }
        }
    }
}