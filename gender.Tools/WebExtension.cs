using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace gender.Tools
{
    public static class WebExtension
    {
        public static string GetWebPageTitle(this string url)
        {
            // Create a request to the url
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

            
            // If the request wasn't an HTTP request (like a file), ignore it
            if (request == null)
            {
                return null;
            }

            request.Headers["Accept-Language"] = "en-US";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

            // Use the user's credentials
            request.UseDefaultCredentials = true;

            // Obtain a response from the server, if there was an error, return nothing
            HttpWebResponse response = null;
            try 
            { 
                response = request.GetResponse() as HttpWebResponse; 
            }
            catch (WebException) 
            {
                return null;
            }

            // Regular expression for an HTML title
            string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";

            // If the correct HTML header exists for HTML text, continue
            if (new List<string>(response.Headers.AllKeys).Contains("Content-Type"))
            {
                if (response.Headers["Content-Type"].StartsWith("text/html"))
                {
                    // Download the page
                    using (Stream stream = response.GetResponseStream())
                    {
                        var charSet = Encoding.UTF8; 
                        try {
                            charSet = Encoding.GetEncoding(response.CharacterSet);
                        } catch {
                        }
                        StreamReader reader = new StreamReader(stream, charSet);
                        var page = reader.ReadToEnd();

                        // Extract the title
                        Regex ex = new Regex(regex, RegexOptions.IgnoreCase);
                        return ex.Match(page).Value.Trim();
                    }
                }
            }
            // Not a valid HTML page
            return null;
        }

        public static void GetFavicon(this string url, string filename)
        {
            var client = new WebClient();
            var bytes = client.DownloadData(@"http://getfavicon.appspot.com/" + url);

            using (var fs = new FileStream(filename, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }
    }
}
