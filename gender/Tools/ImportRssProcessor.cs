using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Xml;

namespace gender.Tools
{
    public class ImportRssProcessor
    {

        public static void Process(IRepository repository)
        {
            foreach (var blogParser in repository.BlogParsers.ToList())
            {
                var rss = XmlReader.Create(blogParser.Link);
                var posts = SyndicationFeed.Load(rss);
                rss.Close();
                foreach (SyndicationItem item in posts.Items)
                {
                    if (blogParser.LastUpdate == null || item.PublishDate.DateTime > blogParser.LastUpdate)
                    {
                        var blogPost = new BlogPost()
                        {
                            Header = item.Title != null  ? item.Title.Text : string.Empty,
                            AddedDate = item.PublishDate.DateTime,
                            Content = item.Summary.Text,
                            BlogID = blogParser.BlogID,
                        };
                        var list = item.Categories.Select(p => p.Name);

                        if (blogPost.Content.Contains("<genderru>") || list.Contains("genderru") || list.Contains("gender.ru"))
                        {
                            if (string.IsNullOrWhiteSpace(blogPost.Header))
                            {
                                blogPost.Header = blogPost.Content.StripTags().Teaser(30, "");
                            }
                            if (item.Links.Count > 0)
                            {
                                blogPost.Source = item.Links[0].Uri.ToString();
                            }
                            var exist = repository.BlogPosts.Any(p => string.Compare(p.Source, blogPost.Source, true) == 0);
                            if (!exist)
                            {
                                repository.CreateBlogPost(blogPost, blogPost.AddedDate);
                            }
                        }
                    }
                }
                    blogParser.LastUpdate = DateTime.Now;
                    repository.UpdateBlogParser(blogParser);
            }

        }
    }
}