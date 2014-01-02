using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<BlogPostEvent> BlogPostEvents
        {
            get
            {
                return Db.BlogPostEvents;
            }
        }

        public bool UpdateBlogPostEvent(int idBlogPost, List<int> events)
        {
            var blogPost = Db.BlogPosts.FirstOrDefault(p => p.ID == idBlogPost);
            if (events == null)
            {
                events = new List<int>();
            }
            if (blogPost != null)
            {
                //remove others
                var listForDelete = blogPost.BlogPostEvents.Where(p => !events.Contains(p.EventID));
                var existList = blogPost.BlogPostEvents.Where(p => events.Contains(p.EventID)).Select(p => p.EventID).ToList();
                Db.BlogPostEvents.DeleteAllOnSubmit(listForDelete);
                Db.BlogPostEvents.Context.SubmitChanges();
                //new list
                var newEvents = events.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newEvents)
                {
                    var @event = Db.Events.FirstOrDefault(p => p.ID == id);

                    if (@event != null)
                    {
                        var blogPostEvent = new BlogPostEvent
                        {
                            BlogPostID = blogPost.ID,
                            EventID = @event.ID
                        };
                        Db.BlogPostEvents.InsertOnSubmit(blogPostEvent);
                        Db.BlogPostEvents.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}