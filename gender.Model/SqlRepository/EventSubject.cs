using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventSubject> EventSubjects
        {
            get
            {
                return Db.EventSubjects;
            }
        }

        public List<int> UpdateEventSubject(int idEvent, List<int> subjects)
        {
            var @event = Db.Events.FirstOrDefault(p => p.ID == idEvent);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (@event != null)
            {
                //remove others
                var listForDelete = @event.EventSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = @event.EventSubjects.Where(p =>  subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.EventSubjects.DeleteAllOnSubmit(listForDelete);
                Db.EventSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var EventSubject = new EventSubject
                        {
                            EventID = @event.ID,
                            SubjectID = subject.ID
                        };
                        Db.EventSubjects.InsertOnSubmit(EventSubject);
                        Db.EventSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}