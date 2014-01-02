using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gender.Social.Mailru
{
    [JsonObject]
    public class MailruUserInfo
    {
        [JsonProperty("uid")]
        public string ID { get; set; }

         [JsonProperty("first_name")]
        public string FirstName { get; set; }

         [JsonProperty("pic_big")]
         public string Picture { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }

        public string Identifier
        {
            get
            {
                return string.Format("mailru-{0}", ID);
            }
        }
    }
}
