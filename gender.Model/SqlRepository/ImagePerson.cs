using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<ImagePerson> ImagePersons
        {
            get
            {
                return Db.ImagePersons;
            }
        }

        public bool UpdateImagePerson(int idImage, List<int> persons)
        {
            var image = Db.Images.FirstOrDefault(p => p.ID == idImage);
            if (persons == null)
            {
                persons = new List<int>();
            }
            if (image != null)
            {
                //remove others
                var listForDelete = image.ImagePersons.Where(p => !persons.Contains(p.PersonID));
                var existList = image.ImagePersons.Where(p => persons.Contains(p.PersonID)).Select(p => p.PersonID).ToList();
                Db.ImagePersons.DeleteAllOnSubmit(listForDelete);
                Db.ImagePersons.Context.SubmitChanges();
                //new list
                var newPersons = persons.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newPersons)
                {
                    var person = Db.Persons.FirstOrDefault(p => p.ID == id);

                    if (person != null)
                    {
                        var imagePerson = new ImagePerson
                        {
                            ImageID = image.ID,
                            PersonID = person.ID
                        };
                        Db.ImagePersons.InsertOnSubmit(imagePerson);
                        Db.ImagePersons.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }
    }
}