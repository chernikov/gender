using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<OrganizationRecordRedirect> OrganizationRecordRedirects
        {
            get
            {
                return Db.OrganizationRecordRedirects;
            }
        }

        public bool CreateOrganizationRecordRedirect(OrganizationRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.OrganizationRecordRedirects.InsertOnSubmit(instance);
                Db.OrganizationRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateOrganizationRecordRedirect(OrganizationRecordRedirect instance)
        {
            var cache = Db.OrganizationRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.OrganizationID = instance.OrganizationID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.OrganizationRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveOrganizationRecordRedirect(int idOrganizationRecordRedirect)
        {
            OrganizationRecordRedirect instance = Db.OrganizationRecordRedirects.FirstOrDefault(p => p.ID == idOrganizationRecordRedirect);
            if (instance != null)
            {
                Db.OrganizationRecordRedirects.DeleteOnSubmit(instance);
                Db.OrganizationRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}