using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace gender.Tools
{
    public class FileUploader
    {
        private string UploadTo;
        private string BaseTo;

        public FileUploader(string basePath, string uploadTo = "Content/files/uploads/")
        {
            this.UploadTo = uploadTo;
            this.BaseTo = basePath;
        }

        public string Upload(HttpPostedFileBase file)
        {
            var path = Path.Combine(UploadTo, StringExtension.GenerateNewFile() + Path.GetExtension(file.FileName));
            using (var fs = System.IO.File.Create(Path.Combine(BaseTo, path)))
            {
                file.InputStream.CopyTo(fs);
            }
            return "/" + path;
        }
    }
}