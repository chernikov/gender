using gender.Attributes.Validation;
using gender.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Models.ViewModels.User
{
    public class UserView : BaseUserView
    {
        public bool ActivatedEmail { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Photo { get; set; }

        [AllowHtml]
        public string Bio { get; set; }

        public int NoticeCommentPeriod { get; set; }

        public int NoticeUpdatePeriod { get; set; }

        public IEnumerable<SelectListItem> SelectListNoticeCommentPeriod
        {
            get
            {
                yield return new SelectListItem() { Value = ((int)Model.User.NoticePeriodType.Immediately).ToString(), Text = "Мгновенно", Selected = NoticeCommentPeriod == (int)Model.User.NoticePeriodType.Immediately };
                yield return new SelectListItem() { Value = ((int)Model.User.NoticePeriodType.OnceDay).ToString(), Text = "Раз в сутки", Selected = NoticeCommentPeriod == (int)Model.User.NoticePeriodType.OnceDay };
                yield return new SelectListItem() { Value = ((int)Model.User.NoticePeriodType.TwiceWeek).ToString(), Text = "Два раза в неделю (в понедельник и четверг)", Selected = NoticeCommentPeriod == (int)Model.User.NoticePeriodType.TwiceWeek };
            }
        }

        public IEnumerable<SelectListItem> SelectListNoticeUpdatePeriod
        {
            get
            {
                yield return new SelectListItem() { Value = ((int)Model.User.NoticePeriodType.Immediately).ToString(), Text = "Мгновенно", Selected = NoticeUpdatePeriod == (int)Model.User.NoticePeriodType.Immediately };
                yield return new SelectListItem() { Value = ((int)Model.User.NoticePeriodType.OnceDay).ToString(), Text = "Раз в сутки", Selected = NoticeUpdatePeriod == (int)Model.User.NoticePeriodType.OnceDay };
                yield return new SelectListItem() { Value = ((int)Model.User.NoticePeriodType.TwiceWeek).ToString(), Text = "Два раза в неделю (в понедельник и четверг)", Selected = NoticeUpdatePeriod == (int)Model.User.NoticePeriodType.TwiceWeek };
            }
        }

        /* дополнительные поля */
        //темы
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

        //регионы
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

        //организация
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
                if (OrganizationList != null)
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

        public Dictionary<string, ContactView> Contacts { get; set; }

        public Dictionary<string, LinkView> Links { get; set; }

        public UserView()
        {
            SubjectList = new List<int>();
            RegionList = new List<int>();
            OrganizationList = new List<int>();
        }

        public string FullName
        {
            get
            {
                return string.Concat(LastName, " ", FirstName, " ", Patronymic).Trim();
            }
        }
        public bool HasEmail { get; set; }
    }
}