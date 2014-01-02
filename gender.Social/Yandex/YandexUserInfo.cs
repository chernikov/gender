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
    public class YandexUserInfo
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("default_email")]
        public string Email { get; set; }

        [JsonProperty("real_name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        public string Identifier
        {
            get
            {
                return string.Format("yandex-{0}", ID);
            }
        }
    }
}
