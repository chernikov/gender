using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<Region> Regions
        {
            get
            {
                return Db.Regions;
            }
        }

        public bool CreateRegion(Region instance)
        {
            if (instance.ID == 0)
            {
                var lastPosition =
                   instance.ParentID.HasValue
                   ? Regions.Where(p => p.ParentID == instance.ParentID).OrderByDescending(p => p.OrderBy).Select(p => p.OrderBy).FirstOrDefault()
                   : Regions.Where(p => !p.ParentID.HasValue).OrderByDescending(p => p.OrderBy).Select(p => p.OrderBy).FirstOrDefault();

                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Regions.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Regions.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;

                instance.OrderBy = lastPosition + 1;
                Db.Regions.InsertOnSubmit(instance);
                Db.Regions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateRegion(Region instance)
        {
            var cache = Db.Regions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Name = instance.Name;
                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Regions.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Regions.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
                cache.Map = instance.Map;
                cache.Link = instance.Link;
                cache.Description = instance.Description;
                Db.Regions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRegion(int idRegion)
        {
            Region instance = Db.Regions.FirstOrDefault(p => p.ID == idRegion);
            if (instance != null)
            {
                Db.Regions.DeleteOnSubmit(instance);
                Db.Regions.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool MoveRegion(int id, int placeBefore)
        {
            var region = Db.Regions.FirstOrDefault(p => p.ID == id);
            if (region != null)
            {
                if (region.OrderBy > placeBefore)
                {
                    foreach (var forRegion in Regions.Where(w => w.OrderBy >= placeBefore && w.OrderBy < region.OrderBy && ((w.ParentID == region.ParentID) || (w.ParentID == null && region.ParentID == null))))
                    {
                        forRegion.OrderBy++;
                    }
                }

                if (region.OrderBy < placeBefore)
                {
                    foreach (var forRegion in Regions.Where(w => w.OrderBy > region.OrderBy && w.OrderBy <= placeBefore && ((w.ParentID == region.ParentID) || (w.ParentID == null && region.ParentID == null))))
                    {
                        forRegion.OrderBy--;
                    }
                }
                region.OrderBy = placeBefore;
                Db.Regions.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ChangeParentRegion(int id, int idParent)
        {
            var region = Db.Regions.FirstOrDefault(p => p.ID == id);

            var newParentRegion = Db.Regions.FirstOrDefault(p => p.ID == idParent);
            if (region == null)
            {
                //нету такого - всё пропало
                return false;
            }
            if (newParentRegion == null)
            {
                //нету такого - всё пропало
                return false;
            }
            if (!region.ParentID.HasValue)
            {
                //перемещаем из корня - низзя
                return false;
            }
            if (region.ParentID == idParent)
            {
                //никуда не перемещаем 
                return true;
            }
            else
            {
                //пересортировка в бывшем списке
                var parentRegion = region.Region1;
                if (parentRegion != null)
                {
                    foreach (var regions in parentRegion.Regions.Where(w => w.OrderBy > region.OrderBy))
                    {
                        regions.OrderBy--;
                    }
                }

                //добавить последним 
                int lastOrderBy = newParentRegion.Regions.OrderByDescending(p => p.OrderBy).Select(p => p.OrderBy).FirstOrDefault();
                region.OrderBy = lastOrderBy + 1;
                region.Region1 = newParentRegion;
                //уииии
                Db.Regions.Context.SubmitChanges();
                return true;
            }
        }

        private void UpdateRegionsHasEntry(List<int> regions)
        {
            if (regions == null || regions.Count == 0)
            {
                return;
            }
            foreach (var regionID in regions)
            {
                var region = Db.Regions.FirstOrDefault(p => p.ID == regionID);
                if (region != null)
                {
                    region.HasEntry = region.BlogPostRegions.Any() || region.DocumentRegions.Any() || region.EventRegions.Any()
                        || region.ImageRegions.Any() || region.OrganizationRegions.Any() || region.PersonRegions.Any() ||
                        region.PublicationRegions.Any() || region.StudyMaterialRegions.Any() || region.WebLinkRegions.Any();
                    Db.Regions.Context.SubmitChanges();
                }
            }
        }

        public void UpdateRegionsHasEntry()
        {

            foreach (var region in Db.Regions.ToList())
            {
                region.HasEntry = region.BlogPostRegions.Any() || region.DocumentRegions.Any() || region.EventRegions.Any()
                || region.ImageRegions.Any() || region.OrganizationRegions.Any() || region.PersonRegions.Any() ||
                region.PublicationRegions.Any() || region.StudyMaterialRegions.Any() || region.WebLinkRegions.Any();

                Db.Regions.Context.SubmitChanges();
            }
        }
    }
}