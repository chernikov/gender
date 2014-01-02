using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialPerson> StudyMaterialPersons
        {
            get
            {
                return Db.StudyMaterialPersons;
            }
        }

        public bool UpdateStudyMaterialPerson(int idStudyMaterial, List<int> persons)
        {
            var studyMaterial = Db.StudyMaterials.FirstOrDefault(p => p.ID == idStudyMaterial);
            if (persons == null)
            {
                persons = new List<int>();
            }
            if (studyMaterial != null)
            {
                //remove others
                var listForDelete = studyMaterial.StudyMaterialPersons.Where(p => !persons.Contains(p.PersonID));
                var existList = studyMaterial.StudyMaterialPersons.Where(p => persons.Contains(p.PersonID)).Select(p => p.PersonID).ToList();
                Db.StudyMaterialPersons.DeleteAllOnSubmit(listForDelete);
                Db.StudyMaterialPersons.Context.SubmitChanges();
                //new list
                var newPersons = persons.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newPersons)
                {
                    var person = Db.Persons.FirstOrDefault(p => p.ID == id);

                    if (person != null)
                    {
                        var StudyMaterialPerson = new StudyMaterialPerson
                        {
                            StudyMaterialID = studyMaterial.ID,
                            PersonID = person.ID
                        };
                        Db.StudyMaterialPersons.InsertOnSubmit(StudyMaterialPerson);
                        Db.StudyMaterialPersons.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }

    }
}