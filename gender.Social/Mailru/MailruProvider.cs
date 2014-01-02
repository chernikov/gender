using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace gender.Social.Mailru
{
    public class MailruProvider
    {
        
        private static string AuthorizeUri = "https://connect.mail.ru/oauth/authorize?client_id={0}&response_type=code&redirect_uri={1}";

        private static string GetAccessTokenUri = "https://connect.mail.ru/oauth/token";

        private static string GetProfileUri = "http://www.appsmail.ru/platform/api?method=users.getInfo&secure=1&app_id={0}&session_key={1}&sig={2}";

        public IMailruAppConfig Config { get; set; }

        public MailruAccessTokenInfo AccessToken { get; set; }

        public string Authorize(string redirectTo)
        {
            return string.Format(AuthorizeUri, Config.AppId, redirectTo);
        }

        public bool GetAccessToken(string code, string redirectTo)
        {
            try
            {
                var postData = string.Format("client_id={1}&client_secret={2}&grant_type=authorization_code&code={0}&redirect_uri={3}", code, Config.AppId, Config.AppSecret, redirectTo);
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
                AccessToken = JsonConvert.DeserializeObject<MailruAccessTokenInfo>(jObj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public JObject GetUserInfo()
        {
            var sig = Md5(string.Format("app_id={0}method=users.getInfosecure=1session_key={1}{2}", Config.AppId, AccessToken.AccessToken, Config.AppSecret));
            var request = string.Format(GetProfileUri, Config.AppId, AccessToken.AccessToken, sig);
            WebClient webClient = new WebClient();

            var bytes = webClient.DownloadData(request);
            string response = Encoding.UTF8.GetString(bytes);
            return JObject.Parse(response.Substring(1, response.Length - 2));
        }

        private static string Md5(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            // Conver the original password to bytes; then create the hash
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            // Bytes to string
            return System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(encodedBytes), "-", "").ToLower();
        }
    }
}
