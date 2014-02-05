using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class WebLink : IModerable, IMaterial
    {
        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return WebLinkSubjects.Select(p => p.Subject);
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return WebLinkRegions.Select(p => p.Region);
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return WebLinkComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return WebLinkComments.Count;
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return WebLinkLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return WebLinkLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", WebLinkSubjects.Select(p => p.Subject.Name)));
                if (WebLinkSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", WebLinkRegions.Select(p => p.Region.Name)));
                if (WebLinkRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "web-link"; }
        }

        public string ClassName
        {
            get { return "WebLink"; }
        }

        public string MaterialType
        {
            get { return "Веб-ресурсы"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return WebLinkSubscriptions.Select(p => p.User).ToList();
            }
        }

        public string DefaultUrl
        {
            get
            {
                return "/web-links/" + Url;
            }
        }
    }
}