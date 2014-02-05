using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<Organization> Organizations
        {
            get
            {
                return Db.Organizations;
            }
        }

        public bool CreateOrganization(Organization instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;

                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Organizations.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Organizations.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;

                Db.Organizations.InsertOnSubmit(instance);
                Db.Organizations.Context.SubmitChanges();

                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.Organization,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    UserID = instance.UserID,
                    AddedDate = DateTime.Now,
                });
                return true;
            }

            return false;
        }

        public bool UpdateOrganization(Organization instance)
        {
            var cache = Db.Organizations.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.UserID = instance.UserID;
                cache.Name = instance.Name;
                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Organizations.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Organizations.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
                cache.Logo = instance.Logo;
                cache.Info = instance.Info;
                cache.ChangedDate = DateTime.Now;
                Db.Organizations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveOrganization(int idOrganization)
        {
            Organization instance = Db.Organizations.FirstOrDefault(p => p.ID == idOrganization);
            if (instance != null)
            {
                Db.Organizations.DeleteOnSubmit(instance);
                Db.Organizations.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModerateOrganization(int idOrganization)
        {
            var instance = Db.Organizations.FirstOrDefault(p => p.ID == idOrganization);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.Organizations.Context.SubmitChanges();
                return true;
            }
            return false;
        }

    }
}