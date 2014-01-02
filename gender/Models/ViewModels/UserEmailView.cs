using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;
using gender.Attributes.Validation;
using System.ComponentModel.DataAnnotations;


namespace gender.Models.ViewModels
{ 
	public class UserEmailView
    {
        public int ID { get; set; }

		public int UserID {get; set; }

        [Required(ErrorMessage="Введите Email")]
        [ValidEmail(ErrorMessage = "Введите корректный Email")]
        [UserEmailValidation(ErrorMessage="Данный адрес уже зарегистрирован")]
		public string Email {get; set; }

		public DateTime? ActivateDate {get; set; }

		public bool IsPrimary {get; set; }

    }
}