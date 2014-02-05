using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class StudyMaterial : IModerable, IMaterial
    {
        public IEnumerable<Person> SubPersons
        {
            get
            {
                return StudyMaterialPersons.Select(p => p.Person).AsEnumerable();
            }
        }

        public IEnumerable<File> SubFiles
        {
            get
            {
                return StudyMaterialFiles.Select(p => p.File).AsEnumerable();
            }
        }

        public IEnumerable<Link> SubLinks
        {
            get
            {
                return StudyMaterialLinks.Select(p => p.Link).AsEnumerable();
            }
        }

        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return StudyMaterialSubjects.Select(p => p.Subject).AsEnumerable();
            }
        }

        public IEnumerable<Region> SubRegions
        {
            get
            {
                return StudyMaterialRegions.Select(p => p.Region).AsEnumerable();
            }
        }

        public IEnumerable<Organization> SubOrganizations
        {
            get
            {
                return StudyMaterialOrganizations.Select(p => p.Organization).AsEnumerable();
            }
        }

        public IList<Comment> SubComments
        {
            get
            {
                return StudyMaterialComments.Select(p => p.Comment).Where(p => !p.ParentID.HasValue).OrderBy(p => p.ID).ToList();
            }
        }

        public int CommentCount
        {
            get
            {
                return StudyMaterialComments.Count;
            }
        }

        public IEnumerable<User> SubLikers
        {
            get
            {
                return StudyMaterialLikes.Where(p => p.IsLike).Select(p => p.User);
            }
        }

        public int SubUnLikersCount
        {
            get
            {
                return StudyMaterialLikes.Count(p => !p.IsLike);
            }
        }

        public string Keywords
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(string.Join(", ",StudyMaterialSubjects.Select(p => p.Subject.Name)));
                if (StudyMaterialSubjects.Any())
                {
                    sb.Append(", ");
                }
                sb.Append(string.Join(", ", StudyMaterialRegions.Select(p => p.Region.Name)));
                if (StudyMaterialRegions.Any())
                {
                    sb.Append(", ");
                }
                sb.Append("гендер");

                return sb.ToString();
            }
        }


        public string TypeUrl
        {
            get { return "study-material"; }
        }

        public string ClassName
        {
            get { return "StudyMaterial"; }
        }

        public string MaterialType
        {
            get { return "Учебный материал"; }
        }

        public IList<User> CommentSubscribers
        {
            get
            {
                return StudyMaterialSubscriptions.Select(p => p.User).ToList();
            }
        }

        public string DefaultUrl
        {
            get
            {
                return "/study-materials/" + Url;
            }
        }
    }
}