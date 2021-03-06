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
    public class EventView
    {
        public int ID { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "�������� ���������")]
        public string Header { get; set; }

        public string Teaser { get; set; }

        public DateTime? EventDate { get; set; }

        public int? Year { get; set; }

        /* �������������� ���� */
        //����
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

        //�������
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

        //�����������
        public List<int> OrganizationList { get; set; }

        public IQueryable<Organization> Organizations
        {
            get
            {
                if (OrganizationList != null)
                {
                    var repository = DependencyResolver.Current.GetService<IRepository>();
                    return repository.Organizations.Where(p => OrganizationList.Contains(p.ID));
                }
                return null;
            }
        }

        public IEnumerable<SelectListItem> SelectListOrganizations
        {
            get
            {
                var list = new List<SelectListItem>();
                if (OrganizationList != null && Organizations != null)
                {
                    foreach (var organization in Organizations)
                    {
                        list.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = organization.Name,
                            Value = organization.ID.ToString()
                        });
                    }
                }
                return list;
            }
        }

        //�������
        public List<int> PersonList { get; set; }

        public IQueryable<Person> Persons
        {
            get
            {
                if (PersonList != null)
                {
                    var repository = DependencyResolver.Current.GetService<IRepository>();
                    return repository.Persons.Where(p => PersonList.Contains(p.ID));
                }
                return null;
            }
        }

        public IEnumerable<SelectListItem> SelectListPersons
        {
            get
            {
                var list = new List<SelectListItem>();
                if (PersonList != null)
                {
                    foreach (var person in Persons)
                    {
                        list.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = string.Format("{0} {1}", person.FirstName, person.LastName),
                            Value = person.ID.ToString()
                        });
                    }
                }
                return list;
            }
        }

        public Dictionary<string, LinkView> Links { get; set; }

        public Dictionary<string, FileView> Files { get; set; }

        public EventView()
        {
            SubjectList = new List<int>();
            RegionList = new List<int>();
            OrganizationList = new List<int>();
            PersonList = new List<int>();
            Links = new Dictionary<string, LinkView>();
        }
    }
}