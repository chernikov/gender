using System.ComponentModel.DataAnnotations;
using gender.Attributes.Validation;

namespace gender.Models.ViewModels.User
{
    public class ForgotPasswordView
    {
        [Required(ErrorMessage = "Введите Email")]
        [ValidEmail(ErrorMessage = "Введите корректный Email")]
        public string Email { get; set; }
    }
}