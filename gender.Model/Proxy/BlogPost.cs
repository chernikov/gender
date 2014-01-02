using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gender.Tools;

namespace gender.Model
{
    public partial class BlogPost : IMaterial
    {
        
        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return BlogPostSubjects.Select(p => p.Subject);
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return BlogPostRegions.Select(p => p.Region);
            }
        }


        public string Teaser
        {
            get
            {
                var content = Content.PBrToNl().StripTags();
                if (content.Length > 1000)
                {
                    var indexOf = content.IndexOf(" ", 1000);
                    if (indexOf != -1)
                    {
                        return content.Substring(0, indexOf);
                    }
                }
                return content;
            }
        }


        public IList<Comment> SubComments
        {
            get
            {
                return BlogPostComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return BlogPostComments.Count;
            }
        }


        public IEnumerable<User> SubLikers
        {
            get
            {
                return BlogPostLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return BlogPostLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", BlogPostSubjects.Select(p => p.Subject.Name)));
                if (BlogPostSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", BlogPostRegions.Select(p => p.Region.Name)));
                if (BlogPostRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "blog"; }
        }

        public string Name
        {
            get { return Header; }
        }

        public string MaterialType
        {
            get { return "Блог"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return BlogPostSubscriptions.Select(p => p.User).ToList(); 
            }
        }
    }
}