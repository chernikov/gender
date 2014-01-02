using gender.Global.Config;
using gender.Tools;
using Ninject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gender.IntegrationTest.Tools
{
    public class Imaginarium : Filerarium
    {
        public static string GetRandomSourceImage()
        {
            return GetRandomSourceFile("D:\\test\\sandbox\\images\\", "*.jpg");
        }

        public static string SaveRandomImage(string folder)
        {
            var imageUrl = string.Format("{0}{1}.jpg", folder, StringExtension.GenerateNewFile());
            var absFile = MakeAbsFolder(imageUrl);
            using (var fileSource = new FileStream(GetRandomSourceImage(), FileMode.Open))
            {
                using (var fs = new FileStream(absFile, FileMode.CreateNew))
                {
                    fileSource.CopyTo(fs);
                    fs.Flush();
                }
            }
            return imageUrl;
        }
    }
}
