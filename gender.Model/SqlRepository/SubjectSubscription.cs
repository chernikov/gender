using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<SubjectSubscription> SubjectSubscriptions
        {
            get
            {
                return Db.SubjectSubscriptions;
            }
        }

        public bool CreateSubjectSubscription(SubjectSubscription instance)
        {
            if (instance.ID == 0)
            {
                Db.SubjectSubscriptions.InsertOnSubmit(instance);
                Db.SubjectSubscriptions.Context.SubmitChanges();
                return true;
            }

            return false;
        }

       
        public bool RemoveSubjectSubscription(int idSubjectSubscription)
        {
            SubjectSubscription instance = Db.SubjectSubscriptions.FirstOrDefault(p => p.ID == idSubjectSubscription);
            if (instance != null)
            {
                Db.SubjectSubscriptions.DeleteOnSubmit(instance);
                Db.SubjectSubscriptions.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool UpdateSubjectSubscription(int idUser, List<int> subjects)
        {
            var user = Db.Users.FirstOrDefault(p => p.ID == idUser);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (user != null)
            {
                //remove others
                var listForDelete = user.SubjectSubscriptions.Where(p => !subjects.Contains(p.SubjectID));
                var existList = user.SubjectSubscriptions.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.SubjectSubscriptions.DeleteAllOnSubmit(listForDelete);
                Db.SubjectSubscriptions.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var subjectSubscription = new SubjectSubscription
                        {
                            UserID = user.ID,
                            SubjectID = subject.ID
                        };
                        Db.SubjectSubscriptions.InsertOnSubmit(subjectSubscription);
                        Db.SubjectSubscriptions.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}