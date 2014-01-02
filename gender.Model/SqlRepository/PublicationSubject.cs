using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationSubject> PublicationSubjects
        {
            get
            {
                return Db.PublicationSubjects;
            }
        }

        public List<int> UpdatePublicationSubject(int idPublication, List<int> subjects)
        {
            var publication = Db.Publications.FirstOrDefault(p => p.ID == idPublication);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (publication != null)
            {
                //remove others
                var listForDelete = publication.PublicationSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = publication.PublicationSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.PublicationSubjects.DeleteAllOnSubmit(listForDelete);
                Db.PublicationSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var publicationSubject = new PublicationSubject
                        {
                            PublicationID = publication.ID,
                            SubjectID = subject.ID
                        };
                        Db.PublicationSubjects.InsertOnSubmit(publicationSubject);
                        Db.PublicationSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}