using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialLike> StudyMaterialLikes
        {
            get
            {
                return Db.StudyMaterialLikes;
            }
        }

        public bool CreateStudyMaterialLike(StudyMaterialLike instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialLikes.InsertOnSubmit(instance);
                Db.StudyMaterialLikes.Context.SubmitChanges();
                RecalculateStudyMaterialLikes(instance.StudyMaterialID);
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterialLike(int idStudyMaterialLike)
        {
            var instance = Db.StudyMaterialLikes.FirstOrDefault(p => p.ID == idStudyMaterialLike);
            if (instance != null)
            {
                var studyMaterialID = instance.StudyMaterialID;

                Db.StudyMaterialLikes.DeleteOnSubmit(instance);
                Db.StudyMaterialLikes.Context.SubmitChanges();
                RecalculateStudyMaterialLikes(studyMaterialID);

                return true;
            }
            return false;
        }

        private void RecalculateStudyMaterialLikes(int studyMaterialID)
        {
            var cache = Db.StudyMaterials.FirstOrDefault(p => p.ID == studyMaterialID);

            if (cache != null)
            {
                var list = Db.StudyMaterialLikes.Where(p => p.StudyMaterialID == studyMaterialID);
                if (list.Any())
                {
                    cache.TotalLikes = list.Sum(p => p.IsLike ? 1 : -1);
                }
                else
                {
                    cache.TotalLikes = 0;
                }
                Db.StudyMaterialLikes.Context.SubmitChanges();
            }
        }
    }
}