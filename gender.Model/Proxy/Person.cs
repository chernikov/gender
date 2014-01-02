using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Person : IMaterial
    {
        public User Author
        {
            get
            {
                return User;
            }
            set
            {
                User = value;
            }
        }

        public User SiteUser
        {
            get
            {
                return User1;
            }
            set
            {
                User1 = value;
            }
        }

        public string FullName
        {
            get
            {
                return string.Concat(LastName, " ", FirstName, " ", Patronymic).Trim();
            }
        }

        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return PersonSubjects.Select(p => p.Subject).AsEnumerable();
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return PersonRegions.Select(p => p.Region).AsEnumerable();
            }
        }


        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return PersonOrganizations.Select(p => p.Organization).AsEnumerable();
            }
        }

        public int PublicationCount
        {
            get
            {
                return PublicationPersons.Count;
            }
        }

        public int StudyMaterialCount
        {
            get
            {
                return StudyMaterialPersons.Count;
            }
        }

        public int ImageCount
        {
            get
            {
                return ImagePersons.Count;
            }
        }

        public int ModeratedPublicationCount
        {
            get
            {
                return PublicationPersons.Count(p => p.Publication.ModeratedDate.HasValue);
            }
        }

        public int ModeratedStudyMaterialCount
        {
            get
            {
                return StudyMaterialPersons.Count(p => p.StudyMaterial.ModeratedDate.HasValue);
            }
        }

        public int ModeratedImageCount
        {
            get
            {
                return ImagePersons.Count(p => p.Image.ModeratedDate.HasValue);
            }
        }

        public int BlogPostCount
        {
            get
            {
                if (SiteUser != null && SiteUser.Blogs.Any())
                {

                    return SiteUser.Blogs.First().BlogPosts.Count;
                }
                return 0;
            }
        }

        public IEnumerable<Publication> SubPublications
        {
            get
            {
                return PublicationPersons.Select(p => p.Publication).OrderByDescending(p => p.Year);
            }
        }

        public IEnumerable<StudyMaterial> SubStudyMaterials
        {
            get
            {
                return StudyMaterialPersons.Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Image> SubImages
        {
            get
            {
                return ImagePersons.Select(p => p.Image).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Publication> SubModeratedPublications
        {
            get
            {
                return PublicationPersons.Where(p => p.Publication.ModeratedDate.HasValue).Select(p => p.Publication).OrderByDescending(p => p.Year);
            }
        }

        public IEnumerable<StudyMaterial> SubModeratedStudyMaterials
        {
            get
            {
                return StudyMaterialPersons.Where(p => p.StudyMaterial.ModeratedDate.HasValue).Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Image> SubModeratedImages
        {
            get
            {
                return ImagePersons.Where(p => p.Image.ModeratedDate.HasValue).Select(p => p.Image).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Link> SubLinks
        {
            get
            {
                return PersonLinks.Select(p => p.Link);
            }
        }

        public IEnumerable<Contact> SubContacts
        {
            get
            {
                return PersonContacts.Select(p => p.Contact);
            }
        }

        public IEnumerable<Event> SubEvents
        {
            get
            {
                return EventPersons.Select(p => p.Event);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", PersonSubjects.Select(p => p.Subject.Name)));
                if (PersonSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", PersonRegions.Select(p => p.Region.Name)));
                if (PersonRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }

        public string TypeUrl
        {
            get { return "persons"; }
        }

        public string Name
        {
            get { return FullName; }
        }

        public string MaterialType
        {
            get { return "Люди"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return null;
            }
        }

        public bool HasBlog
        {
            get
            {
                return SiteUser != null && SiteUser.Blog != null && SiteUser.Blog.BlogPosts.Any();
            }
        }
    }
}