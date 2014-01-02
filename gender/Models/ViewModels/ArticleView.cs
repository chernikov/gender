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
    public class ArticleView
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "¬ведите заголовок")]
        public string Header { get; set; }

        [Required(ErrorMessage = "¬ведите url")]
        public string Url { get; set; }

        [Required(ErrorMessage = "¬ведите текст")]
        [AllowHtml]
        public string Text { get; set; }

        /* дополнительные пол€ */
        //темы
        public List<int> SubjectList { get; set; }

        public IQueryable<Subject> Subjects
        {
            get
            {
                var repository = DependencyResolver.Current.GetService<IRepository>();
                return repository.ArticleSubjects.Where(p => p.ArticleID == ID).Select(p => p.Subject);

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

        public ArticleView()
        {
            SubjectList = new List<int>();
        }
    }
}