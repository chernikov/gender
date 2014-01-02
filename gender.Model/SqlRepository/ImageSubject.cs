using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<ImageSubject> ImageSubjects
        {
            get
            {
                return Db.ImageSubjects;
            }
        }

        public List<int> UpdateImageSubject(int idImage, List<int> subjects)
        {
            var image = Db.Images.FirstOrDefault(p => p.ID == idImage);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (image != null)
            {
                //remove others
                var listForDelete = image.ImageSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = image.ImageSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.ImageSubjects.DeleteAllOnSubmit(listForDelete);
                Db.ImageSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var imageSubject = new ImageSubject
                        {
                            ImageID = image.ID,
                            SubjectID = subject.ID
                        };
                        Db.ImageSubjects.InsertOnSubmit(imageSubject);
                        Db.ImageSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}