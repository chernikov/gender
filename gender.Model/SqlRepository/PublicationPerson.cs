using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<PublicationPerson> PublicationPersons
        {
            get
            {
                return Db.PublicationPersons;
            }
        }

        public bool UpdatePublicationPerson(int idPublication, List<int> persons)
        {
            var publication = Db.Publications.FirstOrDefault(p => p.ID == idPublication);
            if (persons == null)
            {
                persons = new List<int>();
            }
            if (publication != null)
            {
                //remove others
                var listForDelete = publication.PublicationPersons.Where(p => !persons.Contains(p.PersonID));
                var existList = publication.PublicationPersons.Where(p => persons.Contains(p.PersonID)).Select(p => p.PersonID).ToList();
                Db.PublicationPersons.DeleteAllOnSubmit(listForDelete);
                Db.PublicationPersons.Context.SubmitChanges();
                //new list
                var newPersons = persons.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newPersons)
                {
                    var person = Db.Persons.FirstOrDefault(p => p.ID == id);

                    if (person != null)
                    {
                        var publicationPerson = new PublicationPerson
                        {
                            PublicationID = publication.ID,
                            PersonID = person.ID
                        };
                        Db.PublicationPersons.InsertOnSubmit(publicationPerson);
                        Db.PublicationPersons.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}