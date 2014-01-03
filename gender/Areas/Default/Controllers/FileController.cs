using gender.Models.ViewModels;
using gender.Tools;
using gender.Tools.FineUploader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class FileController : DefaultController
    {
        [ValidateInput(false)]
        [HttpPost]
        public FineUploaderResult Upload(FineUpload upload)
        {
            var uDir = "Content/files/attaches/";
            var extension = Path.GetExtension(upload.Filename);
            var uFile = StringExtension.GenerateNewFile() + extension;
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);

            var mimeType = Config.MimeTypes.FirstOrDefault(p => string.Compare(p.Extension, extension, true) == 0);
            if (mimeType != null)
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    upload.InputStream.CopyTo(fs);
                    fs.Flush();
                }
                var file = new Model.File()
                {
                    IsImage = PreviewCreator.SupportMimeType(mimeType.Name),
                    MimeType = mimeType.Name,
                    Path = "/" + uDir + uFile,
                    Name = upload.Filename
                };
                Repository.CreateFile(file);
                return new FineUploaderResult(true, new { file, mimeType });
            }
            return new FineUploaderResult(false, error: "Тип файлов не поддерживается");
        }

        public ActionResult Item(int id)
        {
            var file = Repository.Files.FirstOrDefault(p => p.ID == id);
            if (file != null)
            {
                var fileView = (FileView)ModelMapper.Map(file, typeof(Model.File), typeof(FileView));

                return View("FileItem",  new KeyValuePair<string, FileView>(Guid.NewGuid().ToString("N"), fileView));
            }
            return null;
        }

        public ActionResult Download(int id)
        {
            var file = Repository.Files.FirstOrDefault(p => p.ID == id);
            if (file != null)
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", file.Name));
                Response.TransmitFile(Server.MapPath(file.Path));
                Response.End();
            }
            return null;
        }
    }
}
