using gender.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Models.ViewModels.User
{
    public class AdminUserView 
    {
        public int ID { get; set; }

        public int PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Photo { get; set; }

        public bool Invited { get; set; }

        public int Category { get; set; }

        public int StartRating { get; set; }

        public IEnumerable<SelectListItem> SelectListCategory
        {
            get
            {
                yield return new SelectListItem() { Value = ((int)Model.User.CategoryType.Usual).ToString(), Text = "Обычный", Selected = Category == (int)Model.User.CategoryType.Usual };
                yield return new SelectListItem() { Value = ((int)Model.User.CategoryType.Privilege).ToString(), Text = "Привилегерованный", Selected = Category == (int)Model.User.CategoryType.Privilege };
            }
        }

        public bool IsModerator { get; set; }

        public bool IsAdmin { get; set; }

        public Dictionary<string, UserEmailView> UserEmails { get; set; }
    }
}