using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{

    public partial class SqlRepository
    {
        public IQueryable<OrganizationSubject> OrganizationSubjects
        {
            get
            {
                return Db.OrganizationSubjects;
            }
        }

        public List<int> UpdateOrganizationSubject(int idOrganization, List<int> subjects)
        {
            var organization = Db.Organizations.FirstOrDefault(p => p.ID == idOrganization);
            if (subjects == null)
            {
                subjects = new List<int>();
            }
            if (organization != null)
            {
                //remove others
                var listForDelete = organization.OrganizationSubjects.Where(p => !subjects.Contains(p.SubjectID));
                var existList = organization.OrganizationSubjects.Where(p => subjects.Contains(p.SubjectID)).Select(p => p.SubjectID).ToList();
                Db.OrganizationSubjects.DeleteAllOnSubmit(listForDelete);
                Db.OrganizationSubjects.Context.SubmitChanges();
                //new list
                var newSubjects = subjects.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newSubjects)
                {
                    var subject = Db.Subjects.FirstOrDefault(p => p.ID == id);

                    if (subject != null)
                    {
                        var OrganizationSubject = new OrganizationSubject
                        {
                            OrganizationID = organization.ID,
                            SubjectID = subject.ID
                        };
                        Db.OrganizationSubjects.InsertOnSubmit(OrganizationSubject);
                        Db.OrganizationSubjects.Context.SubmitChanges();
                    }
                }
                return newSubjects.ToList();
            }
            return null;
        }
    }
}