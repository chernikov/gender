using gender.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using gender.Tools;
using System.Web.Mvc;

namespace gender.Models.ViewModels
{
    public class ContactView : IValidatableObject
    {
        public int Type { get; set; }

        public IEnumerable<SelectListItem> SelectListTypes
        {
            get
            {
                yield return new SelectListItem() { Value = ((int)Contact.TypeEnum.Email).ToString(), Text = "Email", Selected = Type == (int)Contact.TypeEnum.Email };
                yield return new SelectListItem() { Value = ((int)Contact.TypeEnum.Phone).ToString(), Text = "Телефон", Selected = Type == (int)Contact.TypeEnum.Phone };
                yield return new SelectListItem() { Value = ((int)Contact.TypeEnum.Skype).ToString(), Text = "Скайп", Selected = Type == (int)Contact.TypeEnum.Skype };
                yield return new SelectListItem() { Value = ((int)Contact.TypeEnum.Address).ToString(), Text = "Адрес", Selected = Type == (int)Contact.TypeEnum.Address };
                yield return new SelectListItem() { Value = ((int)Contact.TypeEnum.Other).ToString(), Text = "Другое", Selected = Type == (int)Contact.TypeEnum.Other };
            }
        }

        public string Value { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch ((Contact.TypeEnum)Type)
            {
                case Contact.TypeEnum.Email :
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        yield return new ValidationResult("Введите email", new string[] { "Value" });
                    } else if (!Value.IsEmail())
                    {
                        yield return new ValidationResult("Введите корректный email", new string[] { "Value" });
                    }
                    break;
                case Contact.TypeEnum.Phone :
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        yield return new ValidationResult("Введите телефон", new string[] { "Value" });
                    } else if (!Value.IsPhone())
                    {
                        yield return new ValidationResult("Введите корректный телефон", new string[] { "Value" });
                    }
                    break;
                case Contact.TypeEnum.Skype:
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        yield return new ValidationResult("Введите скайп", new string[] { "Value" });
                    }
                    else if (!Value.IsSkype())
                    {
                        yield return new ValidationResult("Введите корректное имя скайпа", new string[] { "Value" });
                    }
                    break;
                case Contact.TypeEnum.Address:
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        yield return new ValidationResult("Введите адрес", new string[] { "Value" });
                    }
                    break;
                case Contact.TypeEnum.Other :
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        yield return new ValidationResult("Введите контакт", new string[] { "Value" });
                    }
                    break;
            }

        }
    }
}