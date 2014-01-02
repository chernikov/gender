using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
    public partial class SqlRepository
    {
        public IQueryable<Subject> Subjects
        {
            get
            {
                return Db.Subjects;
            }
        }

        public bool CreateSubject(Subject instance)
        {
            if (instance.ID == 0)
            {
                var lastPosition =
                   instance.ParentID.HasValue
                   ? Subjects.Where(p => p.ParentID == instance.ParentID).OrderByDescending(p => p.OrderBy).Select(p => p.OrderBy).FirstOrDefault()
                   : Subjects.Where(p => !p.ParentID.HasValue).OrderByDescending(p => p.OrderBy).Select(p => p.OrderBy).FirstOrDefault();

                instance.OrderBy = lastPosition + 1;
                instance.Url = Translit.Translate(instance.Name);
                Db.Subjects.InsertOnSubmit(instance);
                Db.Subjects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateSubject(Subject instance)
        {
            var cache = Db.Subjects.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
                cache.Name = instance.Name;
                cache.Url = Translit.Translate(instance.Name);
                cache.MainShow = instance.MainShow;
                Db.Subjects.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveSubject(int idSubject)
        {
            Subject instance = Db.Subjects.FirstOrDefault(p => p.ID == idSubject);
            if (instance != null)
            {
                if (instance.ParentID.HasValue)
                {
                    var parentSubject = Subjects.FirstOrDefault(p => p.ID == instance.ParentID);
                    if (parentSubject != null)
                    {
                        foreach (var forSubject in Subjects.Where(p => p.ParentID == parentSubject.ID && p.OrderBy > instance.OrderBy))
                        {
                            forSubject.OrderBy--;
                        }
                    }
                }
                else
                {
                    foreach (var forSubject in Subjects.Where(p => p.ParentID == null && p.OrderBy > instance.OrderBy))
                    {
                        forSubject.OrderBy--;
                    }
                }
                var list = instance.SubSubjects.ToList();
                foreach (var subject in list)
                {
                    RemoveSubject(subject.ID);
                }
                Db.Subjects.DeleteOnSubmit(instance);
                Db.Subjects.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool MoveSubject(int id, int placeBefore)
        {
            var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);
            if (subject != null)
            {
                if (subject.OrderBy > placeBefore)
                {
                    foreach (var forSubject in Subjects.Where(w => w.OrderBy >= placeBefore && w.OrderBy < subject.OrderBy && ((w.ParentID == subject.ParentID) || (w.ParentID == null && subject.ParentID == null))))
                    {
                        forSubject.OrderBy++;
                    }
                }

                if (subject.OrderBy < placeBefore)
                {
                    foreach (var forSubject in Subjects.Where(w => w.OrderBy > subject.OrderBy && w.OrderBy <= placeBefore && ((w.ParentID == subject.ParentID) || (w.ParentID == null && subject.ParentID == null))))
                    {
                        forSubject.OrderBy--;
                    }
                }
                subject.OrderBy = placeBefore;
                Db.Subjects.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool ChangeParentSubject(int id, int idParent)
        {
            var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

            var newParentSubject = Db.Subjects.FirstOrDefault(p => p.ID == idParent);
            if (subject == null)
            {
                //нету такого - всё пропало
                return false;
            }
            if (newParentSubject == null)
            {
                //нету такого - всё пропало
                return false;
            }
            if (!subject.ParentID.HasValue)
            {
                //перемещаем из корня - низзя
                return false;
            }
            if (subject.ParentID == idParent)
            {
                //никуда не перемещаем 
                return true;
            }
            else
            {
                //пересортировка в бывшем списке
                var parentSubject = subject.Subject1;
                if (parentSubject != null)
                {
                    foreach (var subjects in parentSubject.Subjects.Where(w => w.OrderBy > subject.OrderBy))
                    {
                        subjects.OrderBy--;
                    }
                }

                //добавить последним 
                int lastOrderBy = newParentSubject.Subjects.OrderByDescending(p => p.OrderBy).Select(p => p.OrderBy).FirstOrDefault();
                subject.OrderBy = lastOrderBy + 1;
                subject.Subject1 = newParentSubject;
                //уииии
                Db.Subjects.Context.SubmitChanges();
                return true;
            }
        }
    }
}