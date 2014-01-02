using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Models.ViewModels.User
{
    public class SocialRegisterUserView
    {
        public Model.UserSocial.ProviderType Provider { get; set; }

        public string Identifier { get; set; }

        public string UserInfo { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public bool VerifiedEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Avatar { get; set; }
    }
}