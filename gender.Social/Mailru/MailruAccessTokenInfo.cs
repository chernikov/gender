using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gender.Social.Mailru
{
    [JsonObject]
    public class MailruAccessTokenInfo
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        
        [JsonProperty("x_mailru_vid")]
        public string XMailruVid { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiredIn { get; set; }
    }
}
