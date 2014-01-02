using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using gender.Attributes.Validation;

namespace gender.Models.ViewModels.User
{
    public class ChangePasswordView
    {
        public int ID { get; set; }

        [IsUserPassword(ErrorMessage = "Не верный пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль еще раз")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}