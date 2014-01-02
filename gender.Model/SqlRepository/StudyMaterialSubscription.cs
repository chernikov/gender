using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialSubscription> StudyMaterialSubscriptions
        {
            get
            {
                return Db.StudyMaterialSubscriptions;
            }
        }

        public bool CreateStudyMaterialSubscription(StudyMaterialSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialSubscriptions.InsertOnSubmit(instance);
                Db.StudyMaterialSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateStudyMaterialSubscription(StudyMaterialSubscription instance)
        {
            var cache = Db.StudyMaterialSubscriptions.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.StudyMaterialID = instance.StudyMaterialID;
				cache.UserID = instance.UserID;
                Db.StudyMaterialSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterialSubscription(int idStudyMaterialSubscription)
        {
            StudyMaterialSubscription instance = Db.StudyMaterialSubscriptions.FirstOrDefault(p => p.ID == idStudyMaterialSubscription);
            if (instance != null)
            {
                Db.StudyMaterialSubscriptions.DeleteOnSubmit(instance);
                Db.StudyMaterialSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}