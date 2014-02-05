using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using System.Xml;
using gender.Models;
using gender.Models.Temp;
using System.Text;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{
    public class BlogPostController : AdminController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index(int id, int page = 1)
        {
            var blog = Repository.Blogs.FirstOrDefault(p => p.ID == id);
            if (blog != null)
            {
                var list = Repository.BlogPosts.Where(p => p.BlogID == blog.ID).OrderByDescending(p => p.AddedDate);
                var data = new PageableData<BlogPost>();
                data.Init(list, page, "Index");
                ViewBag.Blog = blog;
                return View(data);
            }
            return RedirectToNotFoundPage;
        }

        public ActionResult Create(int id)
        {
            var blogpostView = new BlogPostView()
            {
                BlogID = id
            };
            return View("Edit", blogpostView);
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
        public ActionResult Edit(BlogPostView blogpostView)
        {
            if (ModelState.IsValid)
            {
                var blogPost = (BlogPost)ModelMapper.Map(blogpostView, typeof(BlogPostView), typeof(BlogPost));
                if (blogPost.ID == 0)
                {
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

                if (blogpostView.ID == 0)
                {
                    var subscribeAuthors =  new List<Person>();
                    if (blogPost.Blog.User != CurrentUser) 
                    {
                        subscribeAuthors.Add(blogPost.Blog.User.Person);
                    }
                    Subscription.AddMaterial(Repository, newSubjects, blogPost, subscribeAuthors);
                }
                return RedirectToAction("Index", new { id = blogPost.BlogID });
            }
            return View(blogpostView);
        }

        public ActionResult Delete(int id)
        {
            var blogpost = Repository.BlogPosts.FirstOrDefault(p => p.ID == id);
            if (blogpost != null)
            {
                var blogId = blogpost.BlogID;
                Repository.RemoveBlogPost(blogpost.ID);
            }
            return RedirectBack;
        }

        private Blog MakeBlog()
        {
            var blog = new Blog()
            {
                UserID = CurrentUser.ID,
                LastUpdate = DateTime.Now
            };
            Repository.CreateBlog(blog);

            return blog;
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.BlogPostRecordRedirects.Where(p => p.BlogPostID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(BlogPostRecordRedirect blogPostRecordRedirect)
        {
            Repository.CreateBlogPostRecordRedirect(blogPostRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var blogPostRecordRedirect = Repository.BlogPostRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (blogPostRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(blogPostRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Ballaeva()
        {
            var xmlDoc = new XmlDocument();

            var comments = new List<gender.Models.Temp.Comment>();
            var nodes = new List<gender.Models.Temp.Node>();
            var nodeRevisions = new List<gender.Models.Temp.NodeRevision>();

            xmlDoc.Load("D://u381884_genforum.xml");

            var root = xmlDoc.GetElementsByTagName("pma_xml_export")[0];


            var db = root.ChildNodes[1];

            var elements = db.ChildNodes;

            foreach (var element in elements)
            {
                if (element is XmlComment)
                {
                    continue;
                }

                if (element is XmlElement)
                {
                    
                    var xmlElement = (XmlElement)element;

                    if (xmlElement.Name == "table")
                    {
                        var tableName = xmlElement.Attributes["name"].Value;

                        if (tableName == "comments")
                        {
                            ParseComment(comments, xmlElement);
                        }

                        if (tableName == "node")
                        {
                            ParseNode(nodes, xmlElement);
                        }

                        if (tableName == "node_revisions")
                        {
                            ParseNodeRevision(nodeRevisions, xmlElement);
                        }
                    }
                }
            }
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Count Comments {0}<br/>", comments.Count));
            sb.AppendLine(string.Format("Count Nodes {0}<br/>", nodes.Count));
            sb.AppendLine(string.Format("Count Node Revisions {0}<br/>", nodeRevisions.Count));

            foreach (var node in nodes)
            {
                if (node.Uid != 4)
                {
                    continue;
                }

                var nodeRevision = nodeRevisions.Where(p => p.Nid == node.Nid).FirstOrDefault();

                if (nodeRevision != null)
                {
                    if (nodeRevision.Uid != 4)
                    {
                        logger.Debug("� ��� �����? " + nodeRevision.Nid.ToString());
                    }

                    var blogPost = new BlogPost()
                    {
                        BlogID = 2,
                        Header = node.Title,
                        Content = nodeRevision.Body.HttpToHref().NlToPBr(),
                        AddedDate = node.Created,
                        ChangedDate = node.Changed
                    };

                    Repository.CreateBlogPost(blogPost);

                    var redirect = new RecordRedirect()
                    {
                        IsForum = true,
                        Url = "/node/" + node.Nid.ToString()
                    };

                    Repository.CreateRecordRedirect(redirect);

                    var blogPostRedirect = new BlogPostRecordRedirect()
                    {
                        BlogPostID = blogPost.ID,
                        RecordRedirectID = redirect.ID
                    };

                    Repository.CreateBlogPostRecordRedirect(blogPostRedirect);

                    var nodeComments = comments.Where(p => p.Nid == node.Nid).OrderBy(p => p.Cid).ToList();

                    foreach (var comment in nodeComments)
                    {
                        if (comment.Uid == 4) 
                        {
                            var commentRecord = new Model.Comment()
                            {
                                UserID = 5,
                                Text = comment.Text,
                                AddedDate = comment.Timestamp,
                            };
                            Repository.CreateComment(commentRecord);
                            comment.RealID = commentRecord.ID;

                            var blogPostComment = new BlogPostComment()
                            {
                                CommentID = commentRecord.ID,
                                BlogPostID = blogPost.ID
                            };

                            Repository.CreateBlogPostComment(blogPostComment);
                        }
                    }

                    foreach (var comment in nodeComments)
                    {
                        if (comment.Pid > 0)
                        {
                            var parent = comments.FirstOrDefault(p => p.Cid == comment.Pid);

                            if (parent != null)
                            {
                                if (parent.Uid != 4 && comment.RealID.HasValue)
                                {
                                    Repository.RemoveComment(comment.RealID.Value);
                                    continue;
                                }

                                var parentRealID = nodeComments.FirstOrDefault(p => p.Cid == parent.Cid).RealID;

                                if (parentRealID.HasValue && comment.RealID.HasValue)
                                {
                                    var commentRecord = Repository.Comments.FirstOrDefault(p => p.ID == comment.RealID.Value);
                                    if (commentRecord != null)
                                    {
                                        commentRecord.ParentID = parentRealID.Value;
                                        Repository.UpdateComment(commentRecord);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Content(sb.ToString());
        }

        private static void ParseNodeRevision(List<NodeRevision> nodeRevisions, XmlElement xmlElement)
        {
            var nodeRevision = new NodeRevision();

            foreach (XmlElement column in xmlElement.ChildNodes)
            {
                if (column.Attributes["name"].Value == "nid")
                {
                    nodeRevision.Nid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "vid")
                {
                    nodeRevision.Vid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "uid")
                {
                    nodeRevision.Uid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "title")
                {
                    nodeRevision.Title = column.InnerText;
                }
                if (column.Attributes["name"].Value == "body")
                {
                    nodeRevision.Body = column.InnerText;
                }
                if (column.Attributes["name"].Value == "teaser")
                {
                    nodeRevision.Teaser = column.InnerText;
                }
                if (column.Attributes["name"].Value == "log")
                {
                    nodeRevision.Log = column.InnerText;
                }
                if (column.Attributes["name"].Value == "timestamp")
                {
                    nodeRevision.Timestamp = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "format")
                {
                    nodeRevision.Format = Int32.Parse(column.InnerText ?? "-1");
                }
            }
            nodeRevisions.Add(nodeRevision);
        }

        private static void ParseNode(List<Node> nodes, XmlElement xmlElement)
        {
            var node = new Node();

            foreach (XmlElement column in xmlElement.ChildNodes)
            {
                if (column.Attributes["name"].Value == "nid")
                {
                    node.Nid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "vid")
                {
                    node.Vid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "type")
                {
                    node.Type = column.InnerText;
                }
                if (column.Attributes["name"].Value == "language")
                {
                    node.Language = column.InnerText;
                }
                if (column.Attributes["name"].Value == "title")
                {
                    node.Title = column.InnerText;
                }
                if (column.Attributes["name"].Value == "uid")
                {
                    node.Uid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "status")
                {
                    node.Status = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "created")
                {
                    node.Created = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "changed")
                {
                    node.Changed = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "comment")
                {
                    node.Comment = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "promote")
                {
                    node.Promote = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "moderate")
                {
                    node.Moderate = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "sticky")
                {
                    node.Sticky = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "tnid")
                {
                    node.Tnid = Int32.Parse(column.InnerText);
                }
                if (column.Attributes["name"].Value == "translate")
                {
                    node.Translate = Int32.Parse(column.InnerText);
                }
            }
            nodes.Add(node);
        }

        private static void ParseComment(List<Models.Temp.Comment> comments, XmlElement xmlElement)
        {
            var comment = new gender.Models.Temp.Comment();

            foreach (XmlElement column in xmlElement.ChildNodes)
            {
                if (column.Attributes["name"].Value == "cid")
                {
                    comment.Cid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "pid")
                {
                    comment.Pid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "nid")
                {
                    comment.Nid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "uid")
                {
                    comment.Uid = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "subject")
                {
                    comment.Subject = column.InnerText;
                }
                if (column.Attributes["name"].Value == "comment")
                {
                    comment.Text = column.InnerText;
                }
                if (column.Attributes["name"].Value == "hostname")
                {
                    comment.HostName = column.InnerText;
                }
                if (column.Attributes["name"].Value == "timestamp")
                {
                    comment.Timestamp = UnixTimeStampToDateTime(column.InnerText);
                }
                if (column.Attributes["name"].Value == "status")
                {
                    comment.Status = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "format")
                {
                    comment.Format = Int32.Parse(column.InnerText ?? "-1");
                }
                if (column.Attributes["name"].Value == "thread")
                {
                    comment.Thread = column.InnerText;
                }
                if (column.Attributes["name"].Value == "name")
                {
                    comment.Name = column.InnerText;
                }
                if (column.Attributes["name"].Value == "mail")
                {
                    comment.Mail = column.InnerText;
                }
                if (column.Attributes["name"].Value == "homepage")
                {
                    comment.HomePage = column.InnerText;
                }
            }
            comments.Add(comment);
        }

        public static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Double.Parse(unixTimeStamp)).ToLocalTime();
            return dtDateTime;
        }

        public ActionResult UpdateComments()
        {
            foreach (var comment in Repository.Comments.ToList())
            {
                comment.Text = comment.Text.StripTags();
                Repository.UpdateComment(comment);
            }
            return null;
        }

        public ActionResult Test()
        {
            var source = @"��� �����, ��� � ����� ������������� �� ����� ��������� �����, �������� � ������, �������� �������. ������� ���������� - ������� ���������, ������� �������� - ������� ��������������� � ����������� ��������. ���� ��� ���� ��������� �� ����� ��� � 2007 ����, ����� ������� ����� �� � �� ����������, �� ������ ��������� �� ����� ��� �� �� ���� ���������. � ������� �� ���� �� ����� ���������� �������� - �������� ��������� - ���� ���� ������� �������: ����� ��������. 

��� �� �� ������� �� ������� �������� � �������� ���������? 

������� ����������, �� ��������� - ��������. ������� ���������� ��������� ������������� � ������ ����� (������� - ����������, ������������), � ��������� �����: �������-��������� �������, ���������, ����������� ��������� ���� �� �������, �� ������ � ���������� �����. ���� ������������ ���������� ����������� - �������� �������� �����������, ����� ����������. ��� ���� ��� �������� ��� ����� - � � �������������, � � �������. ���� ������, � ������, ����� � ��������� ��� ������ ����������� (��� ��� ����� ������ ��������� �� ������), ����������� ���� ���������� ������. � ��� �������-��������� �������� ��������, �� ������� ������ ��������, ������������������. � ��������� ����������� ��� ����������: ������� �� ������ - ��� ��� ��������, �������� - ��� ��� ��������.
 
���������� - �������� ���������������� ���� � ������ �������. �� ��� - ������� ���������, ������ ������ ����� ���������, ����������� ������� �������� �������������� �����������. ��� - �������. 
������ ���� ���������� �������� � �������� ������ (���-��������� ���������), �� ���� �������� ������ ���������� �����. ������ ��� ��� ��������� � ����� ���������: �������� ����� ����������, ������������� �������, �����������. 
� ������� �� ������� ����-������� � ��� ������ ���������� ��������� ������� ������� �� �������� ��� �����������. �� ������ �� ������. ������ ������, ��������� ������ �������, ������ ��� �����, ������� ���������. �� �������� ������. �������, ����� ����������� ������� � �������. �� �����������. ���-���� ������������ ��������� - ���� �� ���������.
������� - ��������� � �������
����������� ����������������� ����������� ������� ��������, ������� ��������������� � �����������. ������� ��� � ������ ���������� ������ �� ���� ����� ��������� ����. �� ������, ����������� �� ������ ��������� ������, ������ ������� � ����� ����� ������������� � �� ������ ��������� ������ � ����������� ��������, ������� ���������� � �������� � ������� ������� �������. ����� ����� ��������� ������ ������� �� ���������� ���� � ���������. �� ����� � ��� � � ������ ���. � �� �������� ����� �������� �������� ��� �������� �� ���������� ������, ��������� �������� �� ��������, �, �������, ������� � ��� ��� ����� ������������ ��������. 
��������, �������, �� ����� �����������������. ����� � ����� �� ������� �������� ���������� ��������� � ����� ������� �����, ������� ������������. 
� ����������� ����� �������� ����������� ��������� � ���������� ��������� ��������, �� �������� ��������� � ��������, - ���������������. ��, ��� ��� ������� ������, ��� ������ � 2007 ���� ��� ���� ������ ������� ���� ��, ��� ��� ������� ���������� ��� ������ ����������������� ���������� ������, ����� �� ����������, ��� ������ ������. �� � � ������ ��������� ������ ���� �� ������� ���. ������ ��������, �������, ������, �������, �� ����� ����������� �������� �������� � ������ �����������. 
 
������� �������� - ������ ��������� �������� ��������� � c��������������������. �������� �� ��������� �������. ������� ����� ����� �������� ������������ ����������������� � ��������. ��� �� ���������. ��������� ��� ����� �������� ����� ����� �� �������� �������������� ������� ���������. �� ����� ������ ����� ��� ������ ���� ��� ������. ���������, ���� ����� �����, ��� �����. � � �� ���� ����� � ����-���������, ��������� �� �����������. ����� ���� ����������. �� ������� ���������, � ������� �������� ������ � ��������� �������������, ����� ������� ������� ����� ��������� ���� ���������� ������. 

����� �������� (������� ������� ��������) - ������ � ������� ������ ������� - �������� �������. �� ����������� ���������. ��� ������� �� ����� ������ ���� �����, ��� �� ����������, ������, ����, ��� ���� �������� ��������� � �������������� ���������. ���� ������� ����� ������� �������� ������ � ������� - �� ���������� ��������� ��� ���������� ��������� ��������������, ������������ ���� �������. ������������, ������������, �������. �� � � ����������� ��� ����� ��������, ��� �������� ������������� ���������� - ��������� ������ �������������. ���������������� ��������. 
�������� �������� � ����������, ������ ����� � ������� �������������� ���������. ��� ������ ������� � �������� ��������� ���������, ������������ ���� �� �� �������. � ��� ������ �� �������� � �������� ������ � ���-������� ��������� ��������� �������, � ������� � ��� ��� ����� ��������� � ��������� ������� �� �������. � ����� �� �������� ��� �������, ��� ����� �� ������ - ����� ���� �������, � �������� ������ �������. ��������, ��� ��� ��� ����.
 
�������� �� ��������� ����. ��� ��������� ������ �� ����������, �������, �������, �������� (������ ��������, �������� ������) ������������ ������ �������. ��� ������������� � ����������� ��������� �������������� �������� ���������� ��������, ����� ������ ��������� � ����������. ����� ������ - ������� �������, ���������� ������ ����������� ������, ��������. 
���������, ��� �������, ��������� ��� ������ ����, ����� �������� �� ���� ������� �� ����������. ��� ��� ����� ��������. � ����� �������� � ����������� ��������� - �� ����� � ����� � �����. ������ ��� ������� ���-��� ������. ���� �������� ������ �������� - ������������ ��������������� - ��������� ���������� � ����-��������� ��������� ����������� ������������ (��, ���� �����, ��������� ���� ����������). 
����� �������� ������� (��� �� �� ������ ����, �������� - ������� ��� ������������� ����). � ������� � ����� ��������� �������� ��������� - ������� � �������. 
������ ���������
��������� ������, ��������: ���������� � ������ 
- �������, ��� ����� ����������� ������-��������� � �������� ������������� ������������� ����� ����� ������ ������� ���������. �������� ��������� - ��� ����� ��������� �������, � ������� ������ - ������ ���������� � ����������. ���������� ����� � ��������������� - ��� ������� ������, ������ ��������. �� � ����� ������� �����, ������ ��������, ������ ����� ������� � �������� ������������.
�������� ������� ����������, � ������� ������ ���� � ���������� ����, � ������������, � �����������, � ������������� ��������� � ��. ������� �� ������ ������ �������� �� ���� ������������, �������������� ��������� ������ �������� ������� � ��������� ������ � �������, ��� ��� �� ������� �������� ����� ������� �����������. � ������� ������� ���� ���������� ������ �������. ������ �������-���������������� �������� ���������� ������������� ����� ���������� - ��������� ������, ���������, ������� � �������, ���������, ��� ��������� ������������� ����� ����������� ���� ���� ������-���������. 
������ ��������
����� ��������, � 1997 ���� - �������, ����� ����������� �� ��������� ������ �������: 
����� ��-�������
- ��� ��� �������-�������� ����� ���������� ����� �����������, �����������������, ����������� �������������� �������. ������ � ����� ������� ���������� ���� ������-�����������, ������ �� ������ � ����� ���������� �� ������. ������� ����� ������������, ����� ����������, �� ������� �� ������ �����, ���� �� ��� ���� - ��������, ����������, ��������. � ����� ��-�������. ������� - �������� �����������. ���� �� ��� ������� �� ������ ������������ ����, ������� ����������� �� ��������� �������� ����������. ��� �� ���� �� ����������� �������� ������������ ����. ����� �� � ��� ������ ��� ���� �����. 
������ � ����: 
����� ��� ����� �������� ���� - ���������� � �������. �� ���� ��� ����� ���-�� ������� - ���������� � �������. 
(�������� ������, ���-������� ��������������.) 


http://www.kp.ru/daily/24267.3/462904/print/


";

            var result = source.HttpToHref().NlToPBr();

            return Content(result);
        }
    }

    
}