using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<WebLinkSubject> WebLinkSubjects
        {
            get
            {
                return Db.WebLinkSubjects;
            }
        }

        public List<int> UpdateWebLinkSubject(int idWebLink, List<int> subjects)
        {
            var webLink = Db.WebLinks.FirstOrDefault(p => p.ID == idWebLink);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (webLink != null)
            {
                //remove others
                var listForDelete = webLink.WebLinkSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = webLink.WebLinkSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.WebLinkSubjects.DeleteAllOnSubmit(listForDelete);
                Db.WebLinkSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var WebLinkSubject = new WebLinkSubject
                        {
                            WebLinkID = webLink.ID,
                            SubjectID = subject.ID
                        };
                        Db.WebLinkSubjects.InsertOnSubmit(WebLinkSubject);
                        Db.WebLinkSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}