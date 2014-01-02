using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PersonSubject> PersonSubjects
        {
            get
            {
                return Db.PersonSubjects;
            }
        }

        public List<int> UpdatePersonSubject(int idPerson, List<int> subjects)
        {
            var person = Db.Persons.FirstOrDefault(p => p.ID == idPerson);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (person != null)
            {
                //remove others
                var listForDelete = person.PersonSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = person.PersonSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.PersonSubjects.DeleteAllOnSubmit(listForDelete);
                Db.PersonSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var personSubject = new PersonSubject
                        {
                            PersonID = person.ID,
                            SubjectID = subject.ID
                        };
                        Db.PersonSubjects.InsertOnSubmit(personSubject);
                        Db.PersonSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }

    }
}