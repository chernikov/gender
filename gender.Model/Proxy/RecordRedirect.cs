using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class RecordRedirect
    {
        public string Name
        {
            get
            {
                if (ArticleRecordRedirects.Any())
                {
                    var article = ArticleRecordRedirects.First().Article;
                    return "������ ���������: " + article.Header;
                }
                if (BlogPostRecordRedirects.Any())
                {
                    var blog = BlogPostRecordRedirects.First().BlogPost;
                    throw new NotImplementedException();
                }
                if (DocumentRecordRedirects.Any())
                {
                    var document = DocumentRecordRedirects.First().Document;
                    return "��������: " + document.Header;
                }
                if (EventRecordRedirects.Any())
                {
                    var @event = EventRecordRedirects.First().Event;
                    return "�������: " + @event.Header;
                }
                if (ImageRecordRedirects.Any())
                {
                    var image = ImageRecordRedirects.First().Image;
                    return "�����������: " + image.Header;
                }
                if (OrganizationRecordRedirects.Any())
                {
                    var organization = OrganizationRecordRedirects.First().Organization;
                    return "�����������: " + organization.Name;
                }

                if (PersonRecordRedirects.Any())
                {
                    var person = PersonRecordRedirects.First().Person;
                    return "�������: " + person.FullName;
                }

                if (PublicationRecordRedirects.Any())
                {
                    var publication = PublicationRecordRedirects.First().Publication;
                    return "����������: " + publication.Header;
                }

                if (StudyMaterialRecordRedirects.Any())
                {
                    var studyMaterial = StudyMaterialRecordRedirects.First().StudyMaterial;
                    return "������� ��������: " + studyMaterial.Name;
                }

                if (WebLinkRecordRedirects.Any())
                {
                    var webLink = WebLinkRecordRedirects.First().WebLink;
                    return "���-������: " + webLink.Name;
                }
                return string.Empty;
            }
        }

        public string NewUrl
        {
            get
            {
                if (ArticleRecordRedirects.Any())
                {
                    var article = ArticleRecordRedirects.First().Article;
                    return "/glossary/" + article.Url;
                }
                if (BlogPostRecordRedirects.Any())
                {
                    var blog = BlogPostRecordRedirects.First().BlogPost;
                    throw new NotImplementedException();
                }
                if (DocumentRecordRedirects.Any())
                {
                    var document = DocumentRecordRedirects.First().Document;
                    return "/documents/" + document.Url;
                }
                if (EventRecordRedirects.Any())
                {
                    var @event = EventRecordRedirects.First().Event;
                    return "/events/" + @event.Url;
                }
                if (ImageRecordRedirects.Any())
                {
                    var image = ImageRecordRedirects.First().Image;
                    return "/images/" + image.Url;
                }
                if (OrganizationRecordRedirects.Any())
                {
                    var organization = OrganizationRecordRedirects.First().Organization;
                    return "/organizations/" + organization.Url;
                }

                if (PersonRecordRedirects.Any())
                {
                    var person = PersonRecordRedirects.First().Person;
                    return "/persons/" + person.Url;
                }

                if (PublicationRecordRedirects.Any())
                {
                    var publication = PublicationRecordRedirects.First().Publication;
                    return "/publications/" + publication.Url;
                }

                if (StudyMaterialRecordRedirects.Any())
                {
                    var studyMaterial = StudyMaterialRecordRedirects.First().StudyMaterial;
                    return "/study-materials/" + studyMaterial.Url;
                }

                if (WebLinkRecordRedirects.Any())
                {
                    var webLink = WebLinkRecordRedirects.First().WebLink;
                    return "/web-links/" + webLink.Url;
                }
                return string.Empty;
            }
        }
	}
}