using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace gender.Tools
{
    public static class WebScreenshoter
    {
        public static Stream GetScreenshot(string url, int widthScreen = 1280, int heightScreen = 1024, int width = 1024, string type = "PNG")
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute)) 
            {
                var request = string.Format("http://mini.s-shot.ru/{0}x{1}/{2}/{3}/?{4}", widthScreen, heightScreen, width, type, url);
                try
                {
                    var webClient = new WebClient();
                    var bytes = webClient.DownloadData(request);

                    var memoryStream = new MemoryStream(bytes);

                    return memoryStream;
                }
                catch 
                {
                    return null;
                }
            }
            return null;
            
        }
    }
}
