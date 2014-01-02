using gender.Model;
using gender.Models.ViewModels;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class BlogController : DefaultController
    {

        public ActionResult Index(int page = 1)
        {
            var list = Repository.BlogPosts.OrderByDescending(p => p.AddedDate);
            var data = new PageableData<BlogPost>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult Author(string url, int page = 1)
        {
            ViewBag.Page = page;
            var person = Repository.Persons.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);
            if (person != null)
            {
                var user = Repository.Users.FirstOrDefault(p => p.ID == person.UserID);
                if (user != null)
                {
                    var blog = user.Blogs.FirstOrDefault();
                    if (blog == null)
                    {
                        var newBlog = new Blog()
                        {
                            UserID = user.ID
                        };
                        Repository.CreateBlog(newBlog);
                        blog = newBlog;
                    }
                    return View(blog);
                }
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Item(string url)
        {
            var item = Repository.BlogPosts.FirstOrDefault(p => string.Compare(p.Url, url, true) == 0);

            if (item != null)
            {
                return View(item);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Comments(int id)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            if (blogPost != null)
            {
                return View(blogPost);
            }
            return null;
        }

        public ActionResult AlsoSubject(int id, int? idSubject = null)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);

            if (blogPost != null)
            {
                BlogPostSubject blogPostSubject = null;
                if (idSubject.HasValue)
                {
                    blogPostSubject = blogPost.BlogPostSubjects.FirstOrDefault(p => p.SubjectID == idSubject);
                    var nextBlogPost = blogPost.BlogPostSubjects.FirstOrDefault(p => p.ID > blogPostSubject.ID);
                    if (nextBlogPost != null)
                    {
                        blogPostSubject = nextBlogPost;
                    }
                    else
                    {
                        blogPostSubject = blogPost.BlogPostSubjects.FirstOrDefault();
                    }
                }
                else
                {
                    blogPostSubject = blogPost.BlogPostSubjects.FirstOrDefault();
                }
                if (blogPostSubject != null)
                {
                    return View(blogPostSubject.Subject);
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult CreateComment(CommentView commentView)
        {
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
                {
                    var comment = (Comment)ModelMapper.Map(commentView, typeof(CommentView), typeof(Comment));
                    comment.UserID = CurrentUser.ID;
                    comment.ID = 0;
                    Repository.CreateComment(comment);
                    var blogPostComment = new BlogPostComment
                    {
                        BlogPostID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateBlogPostComment(blogPostComment);

                    Subscription.NewComment(Repository, blogPostComment);
                    return Json(new { result = "ok" });
                }
                return Json(new { result = "error" });
            }
            return null;
        }

        [Authorize]
        public ActionResult Create()
        {
            if (CurrentUser.CanCreateBlog())
            {
                if (!CurrentUser.Blogs.Any())
                {
                    var blog = new Blog()
                    {
                        UserID = CurrentUser.ID
                    };
                    Repository.CreateBlog(blog);
                }

                
                var blogPostView = new BlogPostView();
                return View("Edit", blogPostView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blogPost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            if (blogPost != null)
            {
                var blogpostView = (BlogPostView)ModelMapper.Map(blogPost, typeof(BlogPost), typeof(BlogPostView));
                return View(blogpostView);
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BlogPostView blogpostView)
        {
            
            if (ModelState.IsValid)
            {
                var blog = CurrentUser.Blogs.FirstOrDefault();
                if (blog == null)
                {
                    blog = new Blog()
                    {
                        UserID = CurrentUser.ID
                    };
                    Repository.CreateBlog(blog);
                }
                var blogPost = (BlogPost)ModelMapper.Map(blogpostView, typeof(BlogPostView), typeof(BlogPost));
                
                if (blogPost.ID == 0)
                {
                    blogPost.BlogID = blog.ID;
                    Repository.CreateBlogPost(blogPost);
                }
                else
                {
                    Repository.UpdateBlogPost(blogPost);
                }
                var newSubjects = Repository.UpdateBlogPostSubject(blogPost.ID, blogpostView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && blogpostView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, blogPost);
                }

                Repository.UpdateBlogPostRegion(blogPost.ID, blogpostView.RegionList);
                Repository.UpdateBlogPostOrganization(blogPost.ID, blogpostView.OrganizationList);
                Repository.UpdateBlogPostPerson(blogPost.ID, blogpostView.PersonList);
                Repository.UpdateBlogPostEvent(blogPost.ID, blogpostView.EventList);

                var blogReturn = Repository.Blogs.FirstOrDefault(p => p.ID == blogPost.BlogID);
                if (blogReturn != null)
                {
                    return RedirectToAction("Author", new { url = blogReturn.User.Person.Url });
                } else {
                    RedirectToAction("Author", new { url = CurrentUser.Person.Url });
                }
                    
                
            }
            return View(blogpostView);
        }

        public ActionResult Delete(int id)
        {
            var blogpost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            if (blogpost != null)
            {
                var personUrl = blogpost.Blog.User.Person.Url;
                Repository.RemoveBlogPost(blogpost.ID);
                return RedirectToAction("Author", new { url = personUrl });
            }
            return RedirectBack;
        }

    }
}
