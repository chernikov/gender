using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Image : IMaterial
    {
        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return ImageSubjects.Select(p => p.Subject);
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return ImageRegions.Select(p => p.Region);
            }
        }

        public IEnumerable<Person> SubPersons
        {
            get
            {
                return ImagePersons.Select(p => p.Person);
            }
        }

        public IEnumerable<Link> SubLinks
        {
            get
            {
                return ImageLinks.Select(p => p.Link);
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return ImageComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return ImageComments.Count;
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return ImageLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return ImageLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", ImageSubjects.Select(p => p.Subject.Name)));
                if (ImageSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", ImageRegions.Select(p => p.Region.Name)));
                if (ImageRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "image"; }
        }

        public string Name
        {
            get { return Header; }
        }

        public string MaterialType
        {
            get { return "Изображения"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return ImageSubscriptions.Select(p => p.User).ToList();
            }
        }
    }
}