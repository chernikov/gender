using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gender.Social.Yandex
{
    public class YandexProvider
    {
        
        private static string AuthorizeUri = "https://oauth.yandex.ru/authorize?response_type=code&client_id={0}";

        private static string GetAccessTokenUri = "https://oauth.yandex.ru/token";

        private static string GetProfileUri = "https://login.yandex.ru/info?format=json&oauth_token={0}";

        public IYandexAppConfig Config { get; set; }

        public YandexAccessTokenInfo AccessToken { get; set; }

        public string Authorize()
        {
            return string.Format(AuthorizeUri, Config.AppId);
        }

        public bool GetAccessToken(string code)
        {
            try
            {
                var postData = string.Format("grant_type=authorization_code&code={0}&client_id={1}&client_secret={2}", code, Config.AppId, Config.AppSecret);
                var encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postData);
                var myRequest = (HttpWebRequest)WebRequest.Create(GetAccessTokenUri);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                var newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                var response = myRequest.GetResponse();
                var responseStream = response.GetResponseStream();
                var responseReader = new StreamReader(responseStream);
                var result = responseReader.ReadToEnd();
                var jObj = JObject.Parse(result);
                AccessToken = JsonConvert.DeserializeObject<YandexAccessTokenInfo>(jObj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public JObject GetUserInfo()
        {
            var request = string.Format(GetProfileUri, AccessToken.AccessToken);
            WebClient webClient = new WebClient();

            string response = webClient.DownloadString(request);
            return JObject.Parse(response);
        }
    }
}
