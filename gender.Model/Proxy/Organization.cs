using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Organization : IMaterial
    {
        public enum Type : int
        {
            World = 0x01,
            Russia = 0x02,
            Other = 0x03
        }

        public int PersonsCount
        {
            get
            {
                return PersonOrganizations.Where(p => p.Person.ModeratedDate.HasValue).Count();
            }
        }

        public int EventsCount
        {
            get
            {
                return EventOrganizations.Where(p => p.Event.ModeratedDate.HasValue).Count();
            }
        }

        public int PublicationsCount
        {
            get
            {
                return PublicationOrganizations.Where(p => p.Publication.ModeratedDate.HasValue).Count();
            }
        }

        public int StudyMaterialsCount
        {
            get
            {
                return StudyMaterialOrganizations.Where(p => p.StudyMaterial.ModeratedDate.HasValue).Count();
            }
        }

        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return OrganizationSubjects.Select(p => p.Subject);
            }
        }

       
        public IEnumerable<Region> SubRegions
        {
            get
            {
                return OrganizationRegions.Select(p => p.Region);
            }
        }

        public IEnumerable<Document> SubDocuments
        {
            get
            {
                return DocumentOrganizations.OrderBy(p => p.Document.Header).Select(p => p.Document);
            }
        }

        public IEnumerable<Document> SubModeratedDocuments
        {
            get
            {
                return DocumentOrganizations.Where(p => p.Document.ModeratedDate.HasValue).OrderBy(p => p.Document.Header).Select(p => p.Document);
            }
        }

        public IEnumerable<Contact> SubContacts
        {
            get
            {
                return OrganizationContacts.Select(p => p.Contact);
            }
        }

        public IEnumerable<Link> SubLinks
        {
            get
            {
                return OrganizationLinks.Select(p => p.Link);
            }
        }

        public IEnumerable<Person> SubPersons
        {
            get
            {
                return PersonOrganizations.Select(p => p.Person);
            }
        }

        public IEnumerable<Person> SubModeratedPersons
        {
            get
            {
                return PersonOrganizations.Where(p => p.Person.ModeratedDate.HasValue).Select(p => p.Person);
            }
        }

        public IEnumerable<Event> SubEvents
        {
            get
            {
                return EventOrganizations.Select(p => p.Event).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Event> SubModeratedEvents
        {
            get
            {
                return EventOrganizations.Where(p => p.Event.ModeratedDate.HasValue).Select(p => p.Event).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Publication> SubPublications
        {
            get
            {
                return PublicationOrganizations.Select(p => p.Publication).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Publication> SubModeratedPublications
        {
            get
            {
                return PublicationOrganizations.Where(p => p.Publication.ModeratedDate.HasValue).Select(p => p.Publication).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<StudyMaterial> SubStudyMaterials
        {
            get
            {
                return StudyMaterialOrganizations.Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<StudyMaterial> SubModeratedStudyMaterials
        {
            get
            {
                return StudyMaterialOrganizations.Where(p => p.StudyMaterial.ModeratedDate.HasValue).Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return OrganizationLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return OrganizationLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ", OrganizationSubjects.Select(p => p.Subject.Name)));
                if (OrganizationSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", OrganizationRegions.Select(p => p.Region.Name)));
                if (OrganizationRegions.Any())
                {

                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "organization"; }
        }

        public string MaterialType
        {
            get { return "Организации"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return null;
            }
        }
    }
}