using gender.Model;
using gender.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace gender.Global
{
    public class SearchEngine
    {
        #region Get
        public static IEnumerable<Subject> Get(string SearchString, IQueryable<Subject> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Region> Get(string SearchString, IQueryable<Region> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Organization> Get(string SearchString, IQueryable<Organization> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Person> Get(string SearchString, IQueryable<Person> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.FirstName != null)
                {
                    rank += Regex.Matches(entry.FirstName.ToLowerInvariant(), regex).Count;
                }
                if (entry.LastName != null)
                {
                    rank += Regex.Matches(entry.LastName.ToLowerInvariant(), regex).Count;
                }
                if (entry.Patronymic != null)
                {
                    rank += Regex.Matches(entry.Patronymic.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Publication> Get(string SearchString, IQueryable<Publication> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));
            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Event> Get(string SearchString, IQueryable<Event> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        #endregion

        #region Search

        public static IEnumerable<Subject> Search(string SearchString, IQueryable<Subject> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(, false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Region> Search(string SearchString, IQueryable<Region> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Article> Search(string SearchString, IQueryable<Article> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (entry.Text != null)
                {
                    rank += Regex.Matches(entry.Text.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<BlogPost> Search(string SearchString, IQueryable<BlogPost> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (entry.Content != null)
                {
                    rank += Regex.Matches(entry.Content.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Source != null)
                {
                    rank += Regex.Matches(entry.Source.ToLowerInvariant(), regex).Count;
                }
                if (entry.Link != null) 
                {
                    if (entry.Link.Url != null)
                    {
                        rank += Regex.Matches(entry.Link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Document> Search(string SearchString, IQueryable<Document> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (entry.Content != null)
                {
                    rank += Regex.Matches(entry.Content.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Teaser != null)
                {
                    rank += Regex.Matches(entry.Teaser.ToLowerInvariant(), regex).Count;
                }

                foreach (var link in entry.DocumentLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Event> Search(string SearchString, IQueryable<Event> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (entry.Year != null)
                {
                    rank += Regex.Matches(entry.Year.ToString().ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Teaser != null)
                {
                    rank += Regex.Matches(entry.Teaser.ToLowerInvariant(), regex).Count;
                }
                foreach (var link in entry.EventLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Image> Search(string SearchString, IQueryable<Image> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Description != null)
                {
                    rank += Regex.Matches(entry.Description.ToLowerInvariant(), regex).Count;
                }
                foreach (var link in entry.ImageLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Organization> Search(string SearchString, IQueryable<Organization> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Info != null)
                {
                    rank += Regex.Matches(entry.Info.ToLowerInvariant(), regex).Count;
                }
                foreach(var link in entry.OrganizationLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                foreach (var contact in entry.OrganizationContacts.Select(p => p.Contact))
                {
                    if (contact.Value != null)
                    {
                        rank += Regex.Matches(contact.Value.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Person> Search(string SearchString, IQueryable<Person> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                rank += Regex.Matches(entry.FullName.ToLowerInvariant(), regex).Count;

                if (entry.FirstName != null)
                {
                    rank += Regex.Matches(entry.FirstName.ToLowerInvariant(), regex).Count;
                }
                if (entry.LastName != null)
                {
                    rank += Regex.Matches(entry.LastName.ToLowerInvariant(), regex).Count;
                }
                if (entry.Patronymic != null)
                {
                    rank += Regex.Matches(entry.Patronymic.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Bio != null)
                {
                    rank += Regex.Matches(entry.Bio.ToLowerInvariant(), regex).Count;
                }
                foreach (var link in entry.PersonLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                foreach (var contact in entry.PersonContacts.Select(p => p.Contact))
                {
                    if (contact.Value != null)
                    {
                        rank += Regex.Matches(contact.Value.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<Publication> Search(string SearchString, IQueryable<Publication> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Header != null)
                {
                    rank += Regex.Matches(entry.Header.ToLowerInvariant(), regex).Count;
                }
                if (entry.Bibliographic != null)
                {
                    rank += Regex.Matches(entry.Bibliographic.ToLowerInvariant(), regex).Count;
                }
                if (entry.Teaser != null)
                {
                    rank += Regex.Matches(entry.Teaser.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Content != null)
                {
                    rank += Regex.Matches(entry.Content.ToLowerInvariant(), regex).Count;
                }
                foreach (var link in entry.PublicationLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                foreach (var file in entry.PublicationFiles.Select(p => p.File))
                {
                    if (file.Name != null)
                    {
                        rank += Regex.Matches(file.Name.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<StudyMaterial> Search(string SearchString, IQueryable<StudyMaterial> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (entry.Teaser != null)
                {
                    rank += Regex.Matches(entry.Teaser.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.Content != null)
                {
                    rank += Regex.Matches(entry.Content.ToLowerInvariant(), regex).Count;
                }
                foreach (var link in entry.StudyMaterialLinks.Select(p => p.Link))
                {
                    if (link.Url != null)
                    {
                        rank += Regex.Matches(link.Url.ToLowerInvariant(), regex).Count;
                    }
                }
                foreach (var file in entry.StudyMaterialFiles.Select(p => p.File))
                {
                    if (file.Name != null)
                    {
                        rank += Regex.Matches(file.Name.ToLowerInvariant(), regex).Count;
                    }
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<WebLink> Search(string SearchString, IQueryable<WebLink> source)
        {
            var term = SearchString.ToLowerInvariant().Trim();//StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Trim();//.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (entry.Description != null)
                {
                    rank += Regex.Matches(entry.Description.ToLowerInvariant(), regex).Count;
                }
                if (entry.Url != null)
                {
                    rank += Regex.Matches(entry.Url.ToLowerInvariant(), regex).Count;
                }
                if (entry.SiteUrl != null)
                {
                    rank += Regex.Matches(entry.SiteUrl.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }
        
        #endregion
    }
}