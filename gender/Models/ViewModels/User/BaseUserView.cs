using System.ComponentModel.DataAnnotations;
using ManageAttribute;
using gender.Attributes.Validation;

namespace gender.Models.ViewModels.User
{
    public class BaseUserView
    {
        public int ID { get; set; }

        [Required(ErrorMessage= "Введите Email")]
        [ValidEmail(ErrorMessage = "Введите корректный Email")]
        [UserEmailValidation(ErrorMessage= "Данный email уже зарегистрирован")]
        public string Email { get; set; }
    }
}