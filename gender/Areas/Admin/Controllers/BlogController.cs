using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{ 
    public class BlogController : AdminController
    {
		public ActionResult Index(int page = 1)
        {
			var list = Repository.Blogs.OrderByDescending(p => p.LastUpdate);
			var data = new PageableData<Blog>();
			data.Init(list, page, "Index");
			return View(data);
		}

        public ActionResult Posts(int page = 1)
        {
            var list = Repository.BlogPosts.OrderByDescending(p => p.AddedDate);
            var data = new PageableData<BlogPost>();
            data.Init(list, page, "Index");
            return View(data);
        }

        [HttpGet]
        public ActionResult BlogTags(int id)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);

            if (blogPost != null)
            {
                var blogPostTagsView = (BlogPostTagsView)ModelMapper.Map(blogPost, typeof(BlogPost), typeof(BlogPostTagsView));

                return View(blogPostTagsView);
            }

            return null;
        }

        [HttpPost]
        public ActionResult BlogTags(BlogPostTagsView blogPostTagsView)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == blogPostTagsView.ID);

            var newSubjects = Repository.UpdateBlogPostSubject(blogPost.ID, blogPostTagsView.SubjectList);
            if (newSubjects != null && newSubjects.Count > 0 && blogPostTagsView.ID != 0)
            {
                Subscription.AddSubject(Repository, newSubjects, blogPost);
            }
            Repository.UpdateBlogPostRegion(blogPost.ID, blogPostTagsView.RegionList);
            return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }
	}
}