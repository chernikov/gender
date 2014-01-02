using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialSubject> StudyMaterialSubjects
        {
            get
            {
                return Db.StudyMaterialSubjects;
            }
        }

        public List<int> UpdateStudyMaterialSubject(int idStudyMaterial, List<int> subjects)
        {
            var studyMaterial = Db.StudyMaterials.FirstOrDefault(p => p.ID == idStudyMaterial);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (studyMaterial != null)
            {
                //remove others
                var listForDelete = studyMaterial.StudyMaterialSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = studyMaterial.StudyMaterialSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.StudyMaterialSubjects.DeleteAllOnSubmit(listForDelete);
                Db.StudyMaterialSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var StudyMaterialSubject = new StudyMaterialSubject
                        {
                            StudyMaterialID = studyMaterial.ID,
                            SubjectID = subject.ID
                        };
                        Db.StudyMaterialSubjects.InsertOnSubmit(StudyMaterialSubject);
                        Db.StudyMaterialSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}