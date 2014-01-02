using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterial> StudyMaterials
        {
            get
            {
                return Db.StudyMaterials;
            }
        }

        public bool CreateStudyMaterial(StudyMaterial instance)
        {
            if (instance.ID == 0)
            {
                instance.AddedDate = DateTime.Now;
                instance.ChangedDate = DateTime.Now;
                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.StudyMaterials.Any(p => string.Compare(p.Url, url, true) == 0);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.StudyMaterials.Any(p => string.Compare(p.Url, url, true) == 0);
                    i++;
                }
                instance.Url = url;
                Db.StudyMaterials.InsertOnSubmit(instance);
                Db.StudyMaterials.Context.SubmitChanges();

                CreateUpdateRecord(new UpdateRecord()
                {
                    ResourceID = instance.ID,
                    MaterialType = (int)UpdateRecord.MaterialTypeEnum.StudyMaterial,
                    Type = (int)UpdateRecord.TypeEnum.New,
                    AddedDate = DateTime.Now,
                    UserID = instance.UserID,
                });
                return true;
            }

            return false;
        }

        public bool UpdateStudyMaterial(StudyMaterial instance)
        {
            var cache = Db.StudyMaterials.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.Name = instance.Name;

                var baseUrl = Translit.Translate(instance.Name);
                var url = baseUrl;
                var i = 1;
                var exist = Db.StudyMaterials.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                while (exist)
                {
                    url = string.Format("{0}-{1}", baseUrl, i);
                    exist = Db.StudyMaterials.Any(p => string.Compare(p.Url, url, true) == 0 && p.ID != instance.ID);
                    i++;
                }
                cache.Url = url;
                cache.Teaser = instance.Teaser;
				cache.Content = instance.Content;
                cache.ChangedDate = DateTime.Now;
                Db.StudyMaterials.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterial(int idStudyMaterial)
        {
            StudyMaterial instance = Db.StudyMaterials.FirstOrDefault(p => p.ID == idStudyMaterial);
            if (instance != null)
            {
                Db.StudyMaterials.DeleteOnSubmit(instance);
                Db.StudyMaterials.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ModerateStudyMaterial(int idStudyMaterial)
        {
            var instance = Db.StudyMaterials.FirstOrDefault(p => p.ID == idStudyMaterial);
            if (instance != null)
            {
                instance.ModeratedDate = DateTime.Now;
                Db.StudyMaterials.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}