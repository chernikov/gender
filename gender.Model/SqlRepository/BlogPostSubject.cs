using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostSubject> BlogPostSubjects
        {
            get
            {
                return Db.BlogPostSubjects;
            }
        }

        public List<int> UpdateBlogPostSubject(int idBlogPost, List<int> subjects)
        {
            var blogPost = Db.BlogPosts.FirstOrDefault(p => p.ID == idBlogPost);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (blogPost != null)
            {
                //remove others
                var listForDelete = blogPost.BlogPostSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = blogPost.BlogPostSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.BlogPostSubjects.DeleteAllOnSubmit(listForDelete);
                Db.BlogPostSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var blogPostSubject = new BlogPostSubject
                        {
                            BlogPostID = blogPost.ID,
                            SubjectID = subject.ID
                        };
                        Db.BlogPostSubjects.InsertOnSubmit(blogPostSubject);
                        Db.BlogPostSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}