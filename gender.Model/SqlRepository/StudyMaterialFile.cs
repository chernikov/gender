using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialFile> StudyMaterialFiles
        {
            get
            {
                return Db.StudyMaterialFiles;
            }
        }

        public bool CreateStudyMaterialFile(StudyMaterialFile instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialFiles.InsertOnSubmit(instance);
                Db.StudyMaterialFiles.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ClearStudyMaterialFiles(int studyMaterialID)
        {
            var listForDelete = Db.StudyMaterialFiles.Where(p => p.StudyMaterialID == studyMaterialID);
            var filesForDelete = listForDelete.Select(p => p.File);
            Db.Files.DeleteAllOnSubmit(filesForDelete);
            Db.StudyMaterialFiles.DeleteAllOnSubmit(listForDelete);
            Db.StudyMaterialFiles.Context.SubmitChanges();
            return true;
        }
    }
}