using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
     public IQueryable<PersonOrganization> PersonOrganizations
        {
            get
            {
                return Db.PersonOrganizations;
            }
        }

        public bool UpdatePersonOrganization(int idPerson, List<int> organizations)
        {
            var person = Db.Persons.FirstOrDefault(p => p.ID == idPerson);
            if (organizations == null)
            {
                organizations = new List<int>();
            }
            if (person != null)
            {
                //remove others
                var listForDelete = person.PersonOrganizations.Where(p => !organizations.Contains(p.OrganizationID));
                var existList = person.PersonOrganizations.Where(p => organizations.Contains(p.OrganizationID)).Select(p => p.OrganizationID).ToList();
                Db.PersonOrganizations.DeleteAllOnSubmit(listForDelete);
                Db.PersonOrganizations.Context.SubmitChanges();
                //new list
                var newOrganizations = organizations.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newOrganizations)
                {
                    var Organization = Db.Organizations.FirstOrDefault(p => p.ID == id);

                    if (Organization != null)
                    {
                        var PersonOrganization = new PersonOrganization
                        {
                            PersonID = person.ID,
                            OrganizationID = Organization.ID
                        };
                        Db.PersonOrganizations.InsertOnSubmit(PersonOrganization);
                        Db.PersonOrganizations.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}