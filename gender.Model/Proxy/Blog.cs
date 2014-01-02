using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Blog 
    {
        public IEnumerable<BlogPost> SubBlogPosts
        {
            get
            {
                return BlogPosts.OrderByDescending(p => p.ID);
            }
        }

        public IEnumerable<BlogPost> SubPageBlogPosts(int page, int itemPerPage = 20)
        {
            return BlogPosts.OrderByDescending(p => p.ID).Skip((page - 1) * itemPerPage).Take(itemPerPage);
        }

        public int BlogPostsCount
        {
            get { return BlogPosts.Count; }
        }

        public int BlogPostCountPage(int itemPerPage = 20)
        {
            return BlogPostsCount / itemPerPage + (BlogPostsCount % itemPerPage != 0 ? 1 : 0);
        }

        public IEnumerable<BlogParser> SubBlogParsers
        {
            get
            {
                return BlogParsers.AsEnumerable();
            }
        }
    }
}