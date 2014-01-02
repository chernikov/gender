using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostRegion> BlogPostRegions
        {
            get
            {
                return Db.BlogPostRegions;
            }
        }

        public bool UpdateBlogPostRegion(int idBlogPost, List<int> regions)
        {
            var blogPost = Db.BlogPosts.FirstOrDefault(p => p.ID == idBlogPost);
            if (regions == null)
            {
                regions = new List<int>();
            }
            if (blogPost != null)
            {
                //remove others
                var listForDelete = blogPost.BlogPostRegions.Where(p => !regions.Contains(p.RegionID));
                var existList = blogPost.BlogPostRegions.Where(p => regions.Contains(p.RegionID)).Select(p => p.RegionID).ToList();
                UpdateRegionsHasEntry(listForDelete.Select(p => p.ID).ToList());
                Db.BlogPostRegions.DeleteAllOnSubmit(listForDelete);
                Db.BlogPostRegions.Context.SubmitChanges();
                //new list
                var newRegions = regions.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newRegions)
                {
                    var region = Db.Regions.FirstOrDefault(p => p.ID == id);

                    if (region != null)
                    {
                        var blogPostRegion = new BlogPostRegion
                        {
                            BlogPostID = blogPost.ID,
                            RegionID = region.ID
                        };
                        Db.BlogPostRegions.InsertOnSubmit(blogPostRegion);
                        Db.BlogPostRegions.Context.SubmitChanges();
                    }
                }
                UpdateRegionsHasEntry(newRegions.ToList());
                return true;
            }
            return false;
        }
    }
}