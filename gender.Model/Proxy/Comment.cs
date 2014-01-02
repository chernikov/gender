using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Comment
    {
        public Comment Parent
        {
            get
            {
                return Comment1;
            }
            set
            {
                Comment1 = value;
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return Comments.OrderBy(p => p.ID).ToList();
            }
        }

        public int Level 
        {
            get
            {
                if (ParentID == null)
                {
                    return 0;
                }

                return Parent.Level + 1;
            }
        }

        public void Init()
        {
            if (BlogPostComments.Any())
            {
                Icon = "comment-alt";
                LinkType = "Blog";
                ShortDesc = "к сообщению в блоге";
                Url = BlogPostComments.First().BlogPost.Url;
                NameLink = BlogPostComments.First().BlogPost.Header;
            }

            if (DocumentComments.Any())
            {
                Icon = "file";
                LinkType = "Document";
                ShortDesc = "к документу";
                Url = DocumentComments.First().Document.Url;
                NameLink = DocumentComments.First().Document.Header;
            }

            if (EventComments.Any())
            {
                Icon = "flag";
                LinkType = "Event";
                ShortDesc = "к событию";
                Url = EventComments.First().Event.Url;
                NameLink = EventComments.First().Event.Header;
            }

            if (ImageComments.Any())
            {
                Icon = "picture";
                LinkType = "Image";
                ShortDesc = "к изображению";
                Url = ImageComments.First().Image.Url;
                NameLink = ImageComments.First().Image.Header;
            }

            if (PublicationComments.Any())
            {
                Icon = "book";
                LinkType = "Publication";
                ShortDesc = "к публикации";
                Url = PublicationComments.First().Publication.Url;
                NameLink = PublicationComments.First().Publication.Header;
            }
            
            if (StudyMaterialComments.Any())
            {
                Icon = "briefcase";
                LinkType = "StudyMaterial";
                ShortDesc = "к учебному материалу";
                Url = StudyMaterialComments.First().StudyMaterial.Url;
                NameLink = StudyMaterialComments.First().StudyMaterial.Name;
            }

            if (WebLinkComments.Any())
            {
                Icon = "globe";
                LinkType = "WebLink";
                ShortDesc = "к веб-ресурсу";
                Url = WebLinkComments.First().WebLink.Url;
                NameLink = WebLinkComments.First().WebLink.Name;
            }
        }

        public string ShortDesc { get; set; }

        public string Icon { get; set; }

        public string Url { get; set; }

        public string LinkType { get; set; }

        public string NameLink { get; set; }

        public bool Any
        {
            get
            {
                return BlogPostComments.Any() || DocumentComments.Any() || EventComments.Any() || WebLinkComments.Any() || StudyMaterialComments.Any() || PublicationComments.Any() || ImageComments.Any();
            }
        }
	}
}