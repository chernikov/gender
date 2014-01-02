using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostOrganization> BlogPostOrganizations
        {
            get
            {
                return Db.BlogPostOrganizations;
            }
        }


        public bool UpdateBlogPostOrganization(int idBlogPost, List<int> organizations)
        {
            var blogPost = Db.BlogPosts.FirstOrDefault(p => p.ID == idBlogPost);
            if (organizations == null)
            {
                organizations = new List<int>();
            }
            if (blogPost != null)
            {
                //remove others
                var listForDelete = blogPost.BlogPostOrganizations.Where(p => !organizations.Contains(p.OrganizationID));
                var existList = blogPost.BlogPostOrganizations.Where(p => organizations.Contains(p.OrganizationID)).Select(p => p.OrganizationID).ToList();
                Db.BlogPostOrganizations.DeleteAllOnSubmit(listForDelete);
                Db.BlogPostOrganizations.Context.SubmitChanges();
                //new list
                var newOrganizations = organizations.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newOrganizations)
                {
                    var Organization = Db.Organizations.FirstOrDefault(p => p.ID == id);

                    if (Organization != null)
                    {
                        var blogPostOrganization = new BlogPostOrganization
                        {
                            BlogPostID = blogPost.ID,
                            OrganizationID = Organization.ID
                        };
                        Db.BlogPostOrganizations.InsertOnSubmit(blogPostOrganization);
                        Db.BlogPostOrganizations.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }     
    }
}