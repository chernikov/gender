using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;
using System.ComponentModel.DataAnnotations;


namespace gender.Models.ViewModels
{
    public class OrganizationView
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Введите наименование")]
        public string Name { get; set; }

        public string Logo { get; set; }

        [Required(ErrorMessage = "Добавьте информацию")]
        [AllowHtml]
        public string Info { get; set; }

        /* дополнительные поля */
        public List<int> SubjectList { get; set; }

        public IQueryable<Subject> Subjects
        {
            get
            {
                if (SubjectList != null)
                {
                    var repository = DependencyResolver.Current.GetService<IRepository>();
                    return repository.Subjects.Where(p => SubjectList.Contains(p.ID));
                }
                return null;
            }
        }

        public IEnumerable<SelectListItem> SelectListSubjects
        {
            get
            {
                var list = new List<SelectListItem>();
                if (SubjectList != null)
                {
                    foreach (var subject in Subjects)
                    {
                        list.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = subject.Name,
                            Value = subject.ID.ToString()
                        });
                    }
                }
                return list;
            }
        }

        public List<int> RegionList { get; set; }

        public IQueryable<Region> Regions
        {
            get
            {
                if (RegionList != null)
                {
                    var repository = DependencyResolver.Current.GetService<IRepository>();
                    return repository.Regions.Where(p => RegionList.Contains(p.ID));
                }
                return null;
            }
        }

        public IEnumerable<SelectListItem> SelectListRegions
        {
            get
            {
                var list = new List<SelectListItem>();
                if (RegionList != null)
                {
                    foreach (var region in Regions)
                    {
                        list.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = region.Name,
                            Value = region.ID.ToString()
                        });
                    }
                }
                return list;
            }
        }

        public Dictionary<string, ContactView> Contacts { get; set; }

        public Dictionary<string, LinkView> Links { get; set; }

        public OrganizationView()
        {
            SubjectList = new List<int>();
            RegionList = new List<int>();
            Links = new Dictionary<string, LinkView>();
        }
    }
}