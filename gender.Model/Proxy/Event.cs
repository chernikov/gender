using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class Event : IModerable, IMaterial
    {
        public enum Type
        {
            All = 0x01,
            Russia = 0x02,
            Other = 0x03
        }

        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return EventSubjects.Select(p => p.Subject);
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return EventRegions.Select(p => p.Region);
            }
        }


        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return EventOrganizations.Select(p => p.Organization);
            }
        }

        public IEnumerable<Person> SubPersons
        {
            get
            {
                return EventPersons.Select(p => p.Person);
            }
        }

        public IEnumerable<Document> SubDocuments
        {
            get
            {
                return Documents.AsEnumerable();
            }
        }


        public IEnumerable<Link> SubLinks
        {
            get
            {
                return EventLinks.Select(p => p.Link);
            }
        }

        public IEnumerable<File> SubFiles
        {
            get
            {
                return EventFiles.Select(p => p.File);
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return EventComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return EventComments.Count;
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return EventLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return EventLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", EventSubjects.Select(p => p.Subject.Name)));
                if (EventSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", EventRegions.Select(p => p.Region.Name)));
                if (EventRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "event"; }
        }

        public string Name
        {
            get { return Header; }
        }

        public string ClassName
        {
            get { return "Event"; }
        }

        public string MaterialType
        {
            get { return "События"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return EventSubscriptions.Select(p => p.User).ToList();
            }
        }

        public string DefaultUrl
        {
            get
            {
                return "/events/" + Url;
            }
        }
    }
}