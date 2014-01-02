using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Models.ViewModels.User
{
    public class CreateAdminUserView : BaseUserView
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Photo { get; set; }

        public int Category { get; set; }

        public int StartRating { get; set; }

        public bool Invited { get; set; }

        public IEnumerable<SelectListItem> SelectListCategory
        {
            get
            {
                yield return new SelectListItem() { Value = ((int)Model.User.CategoryType.Usual).ToString(), Text = "Обычный", Selected = Category == (int)Model.User.CategoryType.Usual };
                yield return new SelectListItem() { Value = ((int)Model.User.CategoryType.Privilege).ToString(), Text = "Привилегерованный", Selected = Category == (int)Model.User.CategoryType.Privilege };
            }
        }
    }
}