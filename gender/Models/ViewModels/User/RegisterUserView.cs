using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gender.Models.ViewModels.User
{
    public class RegisterUserView : BaseUserView
    {
        public int? InviteID { get; set; }

        [Required(ErrorMessage="Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль еще раз")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Captcha { get; set; }

        public bool Agreement { get; set; }
    }
}