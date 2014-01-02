using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentOrganization> DocumentOrganizations
        {
            get
            {
                return Db.DocumentOrganizations;
            }
        }

        public bool UpdateDocumentOrganization(int idDocument, List<int> organizations)
        {
            var document = Db.Documents.FirstOrDefault(p => p.ID == idDocument);
            if (organizations == null)
            {
                organizations = new List<int>();
            }
            if (document != null)
            {
                //remove others
                var listForDelete = document.DocumentOrganizations.Where(p => !organizations.Contains(p.OrganizationID));
                var existList = document.DocumentOrganizations.Where(p => organizations.Contains(p.OrganizationID)).Select(p => p.OrganizationID).ToList();
                Db.DocumentOrganizations.DeleteAllOnSubmit(listForDelete);
                Db.DocumentOrganizations.Context.SubmitChanges();
                //new list
                var newOrganizations = organizations.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newOrganizations)
                {
                    var Organization = Db.Organizations.FirstOrDefault(p => p.ID == id);

                    if (Organization != null)
                    {
                        var documentOrganization = new DocumentOrganization
                        {
                            DocumentID = document.ID,
                            OrganizationID = Organization.ID
                        };
                        Db.DocumentOrganizations.InsertOnSubmit(documentOrganization);
                        Db.DocumentOrganizations.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}