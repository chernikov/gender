using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<StudyMaterialOrganization> StudyMaterialOrganizations
        {
            get
            {
                return Db.StudyMaterialOrganizations;
            }
        }

        public bool UpdateStudyMaterialOrganization(int idStudyMaterial, List<int> organizations)
        {
            var studyMaterial = Db.StudyMaterials.FirstOrDefault(p => p.ID == idStudyMaterial);
            if (organizations == null)
            {
                organizations = new List<int>();
            }
            if (studyMaterial != null)
            {
                //remove others
                var listForDelete = studyMaterial.StudyMaterialOrganizations.Where(p => !organizations.Contains(p.OrganizationID));
                var existList = studyMaterial.StudyMaterialOrganizations.Where(p => organizations.Contains(p.OrganizationID)).Select(p => p.OrganizationID).ToList();
                Db.StudyMaterialOrganizations.DeleteAllOnSubmit(listForDelete);
                Db.StudyMaterialOrganizations.Context.SubmitChanges();
                //new list
                var newOrganizations = organizations.Where(p => !existList.Contains(p)).Select(p => p);
                foreach (var id in newOrganizations)
                {
                    var Organization = Db.Organizations.FirstOrDefault(p => p.ID == id);

                    if (Organization != null)
                    {
                        var studyMaterialOrganization = new StudyMaterialOrganization
                        {
                            StudyMaterialID = studyMaterial.ID,
                            OrganizationID = Organization.ID
                        };
                        Db.StudyMaterialOrganizations.InsertOnSubmit(studyMaterialOrganization);
                        Db.StudyMaterialOrganizations.Context.SubmitChanges();
                    }
                }
                return true;
            }
            return false;
        }


        public bool CreateStudyMaterialOrganization(StudyMaterialOrganization instance)
        {
            if (instance.ID == 0)
            {
                Db.StudyMaterialOrganizations.InsertOnSubmit(instance);
                Db.StudyMaterialOrganizations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateStudyMaterialOrganization(StudyMaterialOrganization instance)
        {
            var cache = Db.StudyMaterialOrganizations.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.StudyMaterialID = instance.StudyMaterialID;
				cache.OrganizationID = instance.OrganizationID;
                Db.StudyMaterialOrganizations.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveStudyMaterialOrganization(int idStudyMaterialOrganization)
        {
            StudyMaterialOrganization instance = Db.StudyMaterialOrganizations.FirstOrDefault(p => p.ID == idStudyMaterialOrganization);
            if (instance != null)
            {
                Db.StudyMaterialOrganizations.DeleteOnSubmit(instance);
                Db.StudyMaterialOrganizations.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}