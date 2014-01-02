using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialAccess> StudyMaterialAccesses
        {
            get
            {
                return Db.StudyMaterialAccesses;
            }
        }

        public bool CreateStudyMaterialAccess(StudyMaterialAccess instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialAccesses.InsertOnSubmit(instance);
                Db.StudyMaterialAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateStudyMaterialAccess(StudyMaterialAccess instance)
        {
            var cache = Db.StudyMaterialAccesses.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.StudyMaterialID = instance.StudyMaterialID;
				cache.UserID = instance.UserID;
                Db.StudyMaterialAccesses.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterialAccess(int idStudyMaterialAccess)
        {
            StudyMaterialAccess instance = Db.StudyMaterialAccesses.FirstOrDefault(p => p.ID == idStudyMaterialAccess);
            if (instance != null)
            {
                Db.StudyMaterialAccesses.DeleteOnSubmit(instance);
                Db.StudyMaterialAccesses.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}