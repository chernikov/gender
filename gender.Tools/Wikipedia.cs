using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace gender.Tools
{
    public class Wikipedia
    {
        public static string GetFirstParagraph(string name) 
        {
            var webClient = new WebClient();
            var request = string.Format("http://ru.wikipedia.org/w/api.php?action=parse&page={0}&format=json&prop=text&section=0", name);
            var result = webClient.DownloadString(request);
            
            var jsonObj = JObject.Parse(result);

            var data = jsonObj["parse"]["text"]["*"].Value<string>();

            data = data.Substring(data.IndexOf("<p><b>"));
            data = data.Substring(0, data.IndexOf("</p>"));

            return data;
        }
    }
}
