using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<DocumentSubject> DocumentSubjects
        {
            get
            {
                return Db.DocumentSubjects;
            }
        }

        public List<int> UpdateDocumentSubject(int idDocument, List<int> subjects)
        {
            var document = Db.Documents.FirstOrDefault(p => p.ID == idDocument);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (document != null)
            {
                //remove others
                var listForDelete = document.DocumentSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = document.DocumentSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.DocumentSubjects.DeleteAllOnSubmit(listForDelete);
                Db.DocumentSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var DocumentSubject = new DocumentSubject
                        {
                            DocumentID = document.ID,
                            SubjectID = subject.ID
                        };
                        Db.DocumentSubjects.InsertOnSubmit(DocumentSubject);
                        Db.DocumentSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}