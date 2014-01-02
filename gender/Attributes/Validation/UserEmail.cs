using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gender.Models.ViewModels;
using gender.Models.ViewModels.User;


namespace gender.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UserEmailValidationAttribute : UniqueValidationAttribute
    {
        protected override bool CheckProperty(object obj)
        {
            if (obj is BaseUserView)
            {
                var userView = obj as BaseUserView;
                return !repository.Users.Any(p =>
                    p.UserEmails.Any(r => string.Compare(r.Email, userView.Email, true) == 0)
                    && p.ID != userView.ID);
            }

            if (obj is UserEmailView)
            {
                var userView = obj as UserEmailView;
                return !repository.UserEmails.Any(p => string.Compare(p.Email, userView.Email, true) == 0 && p.UserID != userView.UserID);
            }
            return true;
        }
    }


}