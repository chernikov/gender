using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Subject
    {
        public Subject Parent
        {
            get
            {
                return Subject1;
            }
        }

        public Subject Ancestor
        {
            get 
            {
                if (Subject1 != null)
                {
                    return Subject1.Ancestor;
                }
                return this;
            }
            
        }

        public IEnumerable<Subject> Neighboring
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.Subjects.Where(p => p.ID != ID).AsEnumerable();
                }
                return null;
            }
        }

        public bool AnySubSubjects
        {
            get
            {
                return Subjects.Any();
            }
        }

        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return Subjects.OrderBy(p => p.OrderBy).AsEnumerable();
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

        public IEnumerable<Article> SubArticles
        {
            get
            {
                return ArticleSubjects.Select(p => p.Article).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<BlogPost> SubBlogPosts
        {
            get
            {
                return BlogPostSubjects.Select(p => p.BlogPost).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Document> SubDocuments
        {
            get
            {
                return DocumentSubjects.Select(p => p.Document).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Event> SubEvents
        {
            get
            {
                return EventSubjects.Select(p => p.Event).OrderByDescending(p => p.EventDate.HasValue ? p.EventDate.Value.Year : (p.Year ?? 0));
            }
        }

        public IEnumerable<Image> SubImages
        {
            get
            {
                return ImageSubjects.Select(p => p.Image).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return OrganizationSubjects.Select(p => p.Organization).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<Person> SubPersons
        {
            get
            {
                return PersonSubjects.Select(p => p.Person).OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ThenBy(p => p.Patronymic);
            }
        }

        public IEnumerable<Publication> SubPublications
        {
            get
            {
                return PublicationSubjects.Select(p => p.Publication).OrderBy(p => p.Header);
            }
        }

        public IEnumerable<StudyMaterial> SubStudyMaterials
        {
            get
            {
                return StudyMaterialSubjects.Select(p => p.StudyMaterial).OrderBy(p => p.Name);
            }
        }

        public IEnumerable<WebLink> SubWebLinks
        {
            get
            {
                return WebLinkSubjects.Select(p => p.WebLink).OrderBy(p => p.Name);
            }
        }
	}
}