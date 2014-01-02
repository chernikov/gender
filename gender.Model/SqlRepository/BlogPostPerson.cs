using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostPerson> BlogPostPersons
        {
            get
            {
                return Db.BlogPostPersons;
            }
        }

        public bool UpdateBlogPostPerson(int idBlogPost, List<int> persons)
        {
            var blogPost = Db.BlogPosts.FirstOrDefault(p => p.ID == idBlogPost);
            if (persons == null)
            {
                persons = new List<int>();
            }
            if (blogPost != null)
            {
                //remove others
                var listForDelete = blogPost.BlogPostPersons.Where(p => !persons.Contains(p.PersonID));
                var existList = blogPost.BlogPostPersons.Where(p => persons.Contains(p.PersonID)).Select(p => p.PersonID).ToList();
                Db.BlogPostPersons.DeleteAllOnSubmit(listForDelete);
                Db.BlogPostPersons.Context.SubmitChanges();
                //new list
                var newPersons = persons.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newPersons)
                {
                    var person = Db.Persons.FirstOrDefault(p => p.ID == id);

                    if (person != null)
                    {
                        var BlogPostPerson = new BlogPostPerson
                        {
                            BlogPostID = blogPost.ID,
                            PersonID = person.ID
                        };
                        Db.BlogPostPersons.InsertOnSubmit(BlogPostPerson);
                        Db.BlogPostPersons.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }

    }
}