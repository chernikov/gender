using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<PublicationOrganization> PublicationOrganizations
        {
            get
            {
                return Db.PublicationOrganizations;
            }
        }

        public bool UpdatePublicationOrganization(int idPublication, List<int> organizations)
        {
            var publication = Db.Publications.FirstOrDefault(p => p.ID == idPublication);
            if (organizations == null)
            {
                organizations = new List<int>();
            }
            if (publication != null)
            {
                //remove others
                var listForDelete = publication.PublicationOrganizations.Where(p => !organizations.Contains(p.OrganizationID));
                var existList = publication.PublicationOrganizations.Where(p => organizations.Contains(p.OrganizationID)).Select(p => p.OrganizationID).ToList();
                Db.PublicationOrganizations.DeleteAllOnSubmit(listForDelete);
                Db.PublicationOrganizations.Context.SubmitChanges();
                //new list
                var newOrganizations = organizations.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newOrganizations)
                {
                    var Organization = Db.Organizations.FirstOrDefault(p => p.ID == id);

                    if (Organization != null)
                    {
                        var PublicationOrganization = new PublicationOrganization
                        {
                            PublicationID = publication.ID,
                            OrganizationID = Organization.ID
                        };
                        Db.PublicationOrganizations.InsertOnSubmit(PublicationOrganization);
                        Db.PublicationOrganizations.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }

    }
}