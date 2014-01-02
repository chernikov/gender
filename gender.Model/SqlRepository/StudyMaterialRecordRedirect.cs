using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialRecordRedirect> StudyMaterialRecordRedirects
        {
            get
            {
                return Db.StudyMaterialRecordRedirects;
            }
        }

        public bool CreateStudyMaterialRecordRedirect(StudyMaterialRecordRedirect instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialRecordRedirects.InsertOnSubmit(instance);
                Db.StudyMaterialRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateStudyMaterialRecordRedirect(StudyMaterialRecordRedirect instance)
        {
            var cache = Db.StudyMaterialRecordRedirects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.StudyMaterialID = instance.StudyMaterialID;
				cache.RecordRedirectID = instance.RecordRedirectID;
                Db.StudyMaterialRecordRedirects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterialRecordRedirect(int idStudyMaterialRecordRedirect)
        {
            StudyMaterialRecordRedirect instance = Db.StudyMaterialRecordRedirects.FirstOrDefault(p => p.ID == idStudyMaterialRecordRedirect);
            if (instance != null)
            {
                Db.StudyMaterialRecordRedirects.DeleteOnSubmit(instance);
                Db.StudyMaterialRecordRedirects.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}