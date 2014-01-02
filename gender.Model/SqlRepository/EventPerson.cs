using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<EventPerson> EventPersons
        {
            get
            {
                return Db.EventPersons;
            }
        }

        public bool UpdateEventPerson(int idEvent, List<int> persons)
        {
            var @event = Db.Events.FirstOrDefault(p => p.ID == idEvent);
            if (persons == null)
            {
                persons = new List<int>();
            }
            if (@event != null)
            {
                //remove others
                var listForDelete = @event.EventPersons.Where(p => !persons.Contains(p.PersonID));
                var existList = @event.EventPersons.Where(p => persons.Contains(p.PersonID)).Select(p => p.PersonID).ToList();
                Db.EventPersons.DeleteAllOnSubmit(listForDelete);
                Db.EventPersons.Context.SubmitChanges();
                //new list
                var newPersons = persons.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newPersons)
                {
                    var person = Db.Persons.FirstOrDefault(p => p.ID == id);

                    if (person != null)
                    {
                        var eventPerson = new EventPerson
                        {
                            EventID = @event.ID,
                            PersonID = person.ID
                        };
                        Db.EventPersons.InsertOnSubmit(eventPerson);
                        Db.EventPersons.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}