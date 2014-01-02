using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class Publication : IMaterial
    {
        public enum TypeEnum : int
        {
            Book = 0x01,
            Article = 0x02,
            Thesis = 0x03
            /*,
        Serial = 0x04*/
        }

        public string TypeStr
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.Article:
                        return "стать€";
                    case TypeEnum.Thesis:
                        return "диссертаци€";
                    case TypeEnum.Book:
                        return "книга";
                    /* case TypeEnum.Serial:
                         return "сери€";*/
                }
                return string.Empty;
            }
        }

        public Publication Parent
        {
            get
            {
                return Publication1;
            }
            set
            {
                Publication1 = value;
            }
        }

        public IEnumerable<Person> SubPersons
        {
            get
            {
                return PublicationPersons.Select(p => p.Person).AsEnumerable();
            }
        }

        public string SubPersonsList
        {
            get
            {
                return string.Join(", ", SubPersons.Select(p => string.Concat(p.LastName, " ", p.FirstName, " ", p.Patronymic)));
            }
        }

        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return PublicationSubjects.Select(p => p.Subject).AsEnumerable();
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return PublicationRegions.Select(p => p.Region).AsEnumerable();
            }
        }

        public IEnumerable<Link> SubLinks
        {
            get
            {
                return PublicationLinks.Where(p => !p.IsShop).Select(p => p.Link).AsEnumerable();
            }
        }

        public IEnumerable<Link> SubShopLinks
        {
            get
            {
                return PublicationLinks.Where(p => p.IsShop).Select(p => p.Link).AsEnumerable();
            }
        }

        public IEnumerable<File> SubFiles
        {
            get
            {
                return PublicationFiles.Select(p => p.File).AsEnumerable();
            }
        }

        public string FirstFileMimeType
        {
            get
            {
                var file = PublicationFiles.Select(p => p.File).Where(p => !p.IsImage).FirstOrDefault();

                if (file != null)
                {
                    return file.MimeType;
                }

                return null;
            }
        }

        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return PublicationOrganizations.Select(p => p.Organization).AsEnumerable();
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return PublicationComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return PublicationComments.Count;
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return PublicationLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return PublicationLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", PublicationSubjects.Select(p => p.Subject.Name)));
                if (PublicationSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", PublicationRegions.Select(p => p.Region.Name)));
                if (PublicationRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        string IMaterial.TypeUrl
        {
            get { return "publication"; }
        }

        public string Name
        {
            get { return Header; }
        }

        public string MaterialType
        {
            get { return "ѕубликации"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return PublicationSubscriptions.Select(p => p.User).ToList();
            }
        }
    }
}