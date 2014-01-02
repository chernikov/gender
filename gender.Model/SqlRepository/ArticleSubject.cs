using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
    public partial class SqlRepository
    {
        public IQueryable<ArticleSubject> ArticleSubjects
        {
            get
            {
                return Db.ArticleSubjects;
            }
        }

        public bool UpdateArticleSubject(int idArticle, List<int> subjects)
        {
            var article = Db.Articles.FirstOrDefault(p => p.ID == idArticle);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (article != null)
            {
                //remove others
                var listForDelete = article.ArticleSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = article.ArticleSubjects.Where(p => p.ArticleID == article.ID && subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.ArticleSubjects.DeleteAllOnSubmit(listForDelete);
                Db.ArticleSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var articleSubject = new ArticleSubject
                        {
                            ArticleID = article.ID,
                            SubjectID = subject.ID
                        };
                        Db.ArticleSubjects.InsertOnSubmit(articleSubject);
                        Db.ArticleSubjects.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}