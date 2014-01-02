using gender.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Models.ViewModels
{
    public class BookPublicationView : PublicationView
    {
        public override int Type
        {
            get
            {
                return (int)Publication.TypeEnum.Book;
            }
        }

        public string Cover { get; set; }

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
        public Dictionary<string, LinkView> ShopLinks { get; set; }

        public BookPublicationView()
        {
            OrganizationList = new List<int>();
        }
    }
}