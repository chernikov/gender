using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class UserSocial
    {
        public enum ProviderType
        {
            facebook = 0x01,
            google = 0x02,
            vk = 0x03,
            twitter = 0x04,
            yandex = 0x05, 
            mailru = 0x06
        }
	}
}