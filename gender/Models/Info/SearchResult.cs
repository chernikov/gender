using gender.Global;
using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Models.Info
{
    public class SearchResult
    {
        public string SearchString { get; set; }

        public bool Any
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SearchString);
            }
        }

        public IEnumerable<Subject> Subjects { get; set; }

        public IEnumerable<Region> Regions { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Document> Documents { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public IEnumerable<Organization> Organizations { get; set; }

        public IEnumerable<Person> Persons { get; set; }

        public IEnumerable<Publication> Publications { get; set; }

        public IEnumerable<StudyMaterial> StudyMaterials { get; set; }

        public IEnumerable<WebLink> WebLinks { get; set; }

        public void Fill(IRepository repository, string searchString)
        {
            SearchString = searchString;

            FillSubjects(repository, searchString);
            FillRegions(repository, searchString);
            FillArticles(repository, searchString);
            FillBlogs(repository, searchString);
            FillDocuments(repository, searchString);
            FillEvents(repository, searchString);
            FillImages(repository, searchString);
            FillOrganizations(repository, searchString);
            FillPersons(repository, searchString);
            FillPublications(repository, searchString);
            FillStudyMaterials(repository, searchString);
            FillWebLinks(repository, searchString);
        }

        private void FillSubjects(IRepository repository, string searchString)
        {
            Subjects = SearchEngine.Search(searchString, repository.Subjects);
        }

        private void FillRegions(IRepository repository, string searchString)
        {
            Regions = SearchEngine.Search(searchString, repository.Regions);
        }

        private void FillArticles(IRepository repository, string searchString)
        {
            Articles = SearchEngine.Search(searchString, repository.Articles);
        }

        private void FillBlogs(IRepository repository, string searchString)
        {
            BlogPosts = SearchEngine.Search(searchString, repository.BlogPosts);
        }

        private void FillDocuments(IRepository repository, string searchString)
        {
            Documents = SearchEngine.Search(searchString, repository.Documents);
        }

        private void FillEvents(IRepository repository, string searchString)
        {
            Events = SearchEngine.Search(searchString, repository.Events);
        }

        private void FillImages(IRepository repository, string searchString)
        {
            Images = SearchEngine.Search(searchString, repository.Images);
        }

        private void FillOrganizations(IRepository repository, string searchString)
        {
            Organizations = SearchEngine.Search(searchString, repository.Organizations);
        }

        private void FillPersons(IRepository repository, string searchString)
        {
            Persons = SearchEngine.Search(searchString, repository.Persons);
        }

        private void FillPublications(IRepository repository, string searchString)
        {
            Publications = SearchEngine.Search(searchString, repository.Publications);
        }

        private void FillStudyMaterials(IRepository repository, string searchString)
        {
            StudyMaterials = SearchEngine.Search(searchString, repository.StudyMaterials);
        }

        private void FillWebLinks(IRepository repository, string searchString)
        {
            WebLinks = SearchEngine.Search(searchString, repository.WebLinks);
        }
    }
}