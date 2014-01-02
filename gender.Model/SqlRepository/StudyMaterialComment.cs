using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialComment> StudyMaterialComments
        {
            get
            {
                return Db.StudyMaterialComments;
            }
        }

        public bool CreateStudyMaterialComment(StudyMaterialComment instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialComments.InsertOnSubmit(instance);
                Db.StudyMaterialComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateStudyMaterialComment(StudyMaterialComment instance)
        {
            var cache = Db.StudyMaterialComments.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.StudyMaterialID = instance.StudyMaterialID;
				cache.CommentID = instance.CommentID;
                Db.StudyMaterialComments.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterialComment(int idStudyMaterialComment)
        {
            StudyMaterialComment instance = Db.StudyMaterialComments.FirstOrDefault(p => p.ID == idStudyMaterialComment);
            if (instance != null)
            {
                Db.StudyMaterialComments.DeleteOnSubmit(instance);
                Db.StudyMaterialComments.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}