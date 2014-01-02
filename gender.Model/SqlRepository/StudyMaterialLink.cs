using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialLink> StudyMaterialLinks
        {
            get
            {
                return Db.StudyMaterialLinks;
            }
        }

        public bool CreateStudyMaterialLink(StudyMaterialLink instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialLinks.InsertOnSubmit(instance);
                Db.StudyMaterialLinks.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool ClearStudyMaterialLinks(int studyMaterialID)
        {
            var listForDelete = Db.StudyMaterialLinks.Where(p => p.StudyMaterialID == studyMaterialID);
            var linksForDelete = listForDelete.Select(p => p.Link);

            Db.Links.DeleteAllOnSubmit(linksForDelete);
            Db.StudyMaterialLinks.DeleteAllOnSubmit(listForDelete);
            Db.StudyMaterialLinks.Context.SubmitChanges();
            return true;
        }

    }
}