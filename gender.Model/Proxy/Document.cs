using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class Document : IModerable, IMaterial
    {
        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return DocumentSubjects.Select(p => p.Subject);
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return DocumentRegions.Select(p => p.Region);
            }
        }

        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return DocumentOrganizations.Select(p => p.Organization);
            }
        }

        public IEnumerable<Link> SubLinks
        {
            get
            {
                return DocumentLinks.Select(p => p.Link);
            }
        }

        public IEnumerable<File> SubFiles
        {
            get
            {
                return DocumentFiles.Select(p => p.File);
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return DocumentComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return DocumentComments.Count;
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return DocumentLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return DocumentLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", DocumentSubjects.Select(p => p.Subject.Name)));
                if (DocumentSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", DocumentRegions.Select(p => p.Region.Name)));
                if (DocumentRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "document"; }
        }

        public string Name
        {
            get { return Header; }
        }

        public string MaterialType
        {
            get { return "Документы"; }
        }

        public string ClassName
        {
            get { return "Document"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return DocumentSubscriptions.Select(p => p.User).ToList();
            }
        }

        public string DefaultUrl
        {
            get
            {
                return "/documents/" + Url;
            }
        }
    }
}