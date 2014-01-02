using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventOrganization> EventOrganizations
        {
            get
            {
                return Db.EventOrganizations;
            }
        }

        public bool UpdateEventOrganization(int idEvent, List<int> organizations)
        {
            var @event = Db.Events.FirstOrDefault(p => p.ID == idEvent);
            if (organizations == null)
            {
                organizations = new List<int>();
            }
            if (@event != null)
            {
                //remove others
                var listForDelete = @event.EventOrganizations.Where(p => !organizations.Contains(p.OrganizationID));
                var existList = @event.EventOrganizations.Where(p => organizations.Contains(p.OrganizationID)).Select(p => p.OrganizationID).ToList();
                Db.EventOrganizations.DeleteAllOnSubmit(listForDelete);
                Db.EventOrganizations.Context.SubmitChanges();
                //new list
                var newOrganizations = organizations.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newOrganizations)
                {
                    var Organization = Db.Organizations.FirstOrDefault(p => p.ID == id);

                    if (Organization != null)
                    {
                        var eventOrganization = new EventOrganization
                        {
                            EventID = @event.ID,
                            OrganizationID = Organization.ID
                        };
                        Db.EventOrganizations.InsertOnSubmit(eventOrganization);
                        Db.EventOrganizations.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }


    }
}