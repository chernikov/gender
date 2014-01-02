using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Region
    {
        public Region Parent
        {
            get
            {
                return Region1;
            }
        }

        public bool AnySubRegions
        {
            get
            {
                return Regions.Any();
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return Regions.OrderBy(p => p.OrderBy).AsEnumerable();
            }
        }

        public string FullUrl
        {
            get
            {
                var fullUrl = Url;
                var currentItem = Parent;
                while (currentItem != null)
                {
                    fullUrl = currentItem.Url + "/" + fullUrl;
                    currentItem = currentItem.Parent;
                }
                return fullUrl;
            }
        }

        public IEnumerable<BlogPost> SubBlogPosts
        {
            get
            {
                return BlogPostRegions.Select(p => p.BlogPost).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Document> SubDocuments
        {
            get
            {
                return DocumentRegions.Select(p => p.Document).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Event> SubEvents
        {
            get
            {
                return EventRegions.Select(p => p.Event).OrderByDescending(p => p.EventDate.HasValue ? p.EventDate.Value.Year : (p.Year ?? 0));
            }
        }

        public IEnumerable<Image> SubImages
        {
            get
            {
                return ImageRegions.Select(p => p.Image).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return OrganizationRegions.Select(p => p.Organization).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Person> SubPersons
        {
            get
            {
                return PersonRegions.Select(p => p.Person).OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ThenBy(p => p.Patronymic);
            }
        }

        public IEnumerable<Publication> SubPublications
        {
            get
            {
                return PublicationRegions.Select(p => p.Publication).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<StudyMaterial> SubStudyMaterials
        {
            get
            {
                return StudyMaterialRegions.Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<WebLink> SubWebLinks
        {
            get
            {
                return WebLinkRegions.Select(p => p.WebLink).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Document> SubModeratedDocuments
        {
            get
            {
                return DocumentRegions.Where(p => p.Document.ModeratedDate.HasValue).Select(p => p.Document).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Event> SubModeratedEvents
        {
            get
            {
                return EventRegions.Where(p => p.Event.ModeratedDate.HasValue).Select(p => p.Event).OrderByDescending(p => p.EventDate.HasValue ? p.EventDate.Value.Year : (p.Year ?? 0));
            }
        }

        public IEnumerable<Image> SubModeratedImages
        {
            get
            {
                return ImageRegions.Where(p => p.Image.ModeratedDate.HasValue).Select(p => p.Image).OrderBy(p => p.Header);
            }
        }


        public IEnumerable<Person> SubModeratedPersons
        {
            get
            {
                return PersonRegions.Where(p => p.Person.ModeratedDate.HasValue).Select(p => p.Person).OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ThenBy(p => p.Patronymic);
            }
        }

        public IEnumerable<Publication> SubModeratedPublications
        {
            get
            {
                return PublicationRegions.Where(p => p.Publication.ModeratedDate.HasValue).Select(p => p.Publication).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<StudyMaterial> SubModeratedStudyMaterials
        {
            get
            {
                return StudyMaterialRegions.Where(p => p.StudyMaterial.ModeratedDate.HasValue).Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<WebLink> SubModeratedWebLinks
        {
            get
            {
                return WebLinkRegions.Where(p => p.WebLink.ModeratedDate.HasValue).Select(p => p.WebLink).OrderBy(p => p.Name);
            }
        }
	}
}