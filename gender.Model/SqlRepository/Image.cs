using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<Image> Images
        {
            get
            {
                return Db.Images;
            }
        }

        public bool CreateImage(Image instance, int? idUser = null)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;

                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Images.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Images.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;

                Db.Images.InsertOnSubmit(instance);
                Db.Images.Context.SubmitChanges();

                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.Image,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    AddedDate = DateTime.Now,
                    UserID = idUser
                });
                return true;
            }

            return false;
        }

        public bool UpdateImage(Image instance)
        {
            var cache = Db.Images.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Path = instance.Path;
                cache.Header = instance.Header;
                var baseUrl = Translit.Translate(instance.Header);
                var url = baseUrl;
                var i = 1;
                var exist = Db.Images.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.Images.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;

                cache.Description = instance.Description;
                cache.ChangedDate = DateTime.Now;
                Db.Images.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveImage(int idImage)
        {
            var instance = Db.Images.FirstOrDefault(p => p.ID == idImage);
            if (instance != null)
            {
                Db.Images.DeleteOnSubmit(instance);
                Db.Images.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModerateImage(int idImage)
        {
            var instance = Db.Images.FirstOrDefault(p => p.ID == idImage);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.Images.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}