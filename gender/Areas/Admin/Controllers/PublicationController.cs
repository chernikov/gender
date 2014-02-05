using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Models.ViewModels;
using gender.Model;
using gender.Global;
using System.Text;
using System.IO;
using System.Net;
using gender.Tools;


namespace gender.Areas.Admin.Controllers
{
    public class PublicationController : AdminController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index(int page = 1)
        {
            var list = Repository.Publications.OrderByDescending(p => p.ID);
            var data = new PageableData<Publication>();
            data.Init(list, page, "Index");
            return View(data);
        }

        public ActionResult CreateBook()
        {
            var bookPublicationView = new BookPublicationView();
            return View("EditBook", bookPublicationView);
        }

        public ActionResult CreateArticle()
        {
            var articlePublicationView = new ArticlePublicationView();
            return View("EditArticle", articlePublicationView);
        }

        public ActionResult CreateThesis()
        {
            var thesisPublicationView = new ThesisPublicationView();
            return View("EditThesis", thesisPublicationView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);

            if (publication != null)
            {
                switch ((Publication.TypeEnum)publication.Type)
                {
                    case Publication.TypeEnum.Book:
                        var bookPublicationView = (BookPublicationView)ModelMapper.Map(publication, typeof(Publication), typeof(BookPublicationView));
                        return View("EditBook", bookPublicationView);
                    case Publication.TypeEnum.Article:
                        var articlePublicationView = (ArticlePublicationView)ModelMapper.Map(publication, typeof(Publication), typeof(ArticlePublicationView));
                        return View("EditArticle", articlePublicationView);
                    case Publication.TypeEnum.Thesis:
                        var thesisPublicationView = (ThesisPublicationView)ModelMapper.Map(publication, typeof(Publication), typeof(ThesisPublicationView));
                        return View("EditThesis", thesisPublicationView);
                }
            }
            return RedirectToNotFoundPage;
        }

        [HttpPost]
        public ActionResult Edit(PublicationView publicationView)
        {
            LinksVerification(publicationView.Links);
            if (ModelState.IsValid)
            {
                Publication publication = null;
                switch ((Publication.TypeEnum)publicationView.Type)
                {
                    case Publication.TypeEnum.Article:
                        publication = (Publication)ModelMapper.Map(publicationView, typeof(ArticlePublicationView), typeof(Publication));
                        break;
                    case Publication.TypeEnum.Book:
                        publication = (Publication)ModelMapper.Map(publicationView, typeof(BookPublicationView), typeof(Publication));
                        break;
                    case Publication.TypeEnum.Thesis:
                        publication = (Publication)ModelMapper.Map(publicationView, typeof(ThesisPublicationView), typeof(Publication));
                        break;
                }
                publication.UserID = CurrentUser.ID;
                var fillText = false;
                if (publication.ID == 0)
                {
                    Repository.CreatePublication(publication);
                    Repository.CreateUpdateRecord(new UpdateRecord()
                    {
                        ResourceID = publication.ID,
                        MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                        Type = (!string.IsNullOrWhiteSpace(publication.Content)
                            || (publicationView.Files != null && publicationView.Files.Any())
                            || (publicationView.Links != null && publicationView.Links.Any()))
                            ? (int)UpdateRecord.TypeEnum.New : (int)UpdateRecord.TypeEnum.NewWithoutText,
                        AddedDate = DateTime.Now,
                        UserID = CurrentUser.ID
                    });
                }
                else
                {
                    Repository.UpdatePublication(publication, out fillText, CurrentUser.ID);
                }

                Repository.ModeratePublication(publication.ID);
                var newSubjects = Repository.UpdatePublicationSubject(publication.ID, publicationView.SubjectList);
                if (newSubjects != null && newSubjects.Count > 0 && publicationView.ID != 0)
                {
                    Subscription.AddSubject(Repository, newSubjects, publication);
                }
                Repository.UpdatePublicationRegion(publication.ID, publicationView.RegionList);
                Repository.UpdatePublicationPerson(publication.ID, publicationView.PersonList);

                if (publicationView.ID != 0)
                {
                    var countLinks = publicationView.Links != null ? publicationView.Links.Count(p => p.Value.ID == 0) : 0;
                    if (countLinks > 0)
                    {
                        Repository.CreateUpdateRecord(new UpdateRecord()
                        {
                            ResourceID = publication.ID,
                            MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                            Type = countLinks == 1 ? (int)UpdateRecord.TypeEnum.NewLink : (int)UpdateRecord.TypeEnum.NewLinks,
                            AddedDate = DateTime.Now,
                            UserID = CurrentUser.ID
                        });
                    }
                }

                Repository.ClearPublicationLinks(publication.ID);
                if (publicationView.Links != null)
                {
                    foreach (var linkView in publicationView.Links)
                    {
                        var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                        Repository.CreateLink(link);
                        var publicationLink = new PublicationLink()
                        {
                            PublicationID = publication.ID,
                            LinkID = link.ID
                        };
                        Repository.CreatePublicationLink(publicationLink);
                    }
                }

                if (publicationView is BookPublicationView)
                {
                    var bookPublicationView = (BookPublicationView)publicationView;
                    Repository.UpdatePublicationOrganization(publication.ID, bookPublicationView.OrganizationList);
                    if (bookPublicationView.ShopLinks != null)
                    {
                        foreach (var linkView in bookPublicationView.ShopLinks)
                        {
                            var link = (Link)ModelMapper.Map(linkView.Value, typeof(LinkView), typeof(Link));
                            Repository.CreateLink(link);

                            var publicationLink = new PublicationLink()
                            {
                                PublicationID = publication.ID,
                                LinkID = link.ID,
                                IsShop = true
                            };
                            Repository.CreatePublicationLink(publicationLink);
                        }
                    }
                }
                var countFiles = publicationView.Files != null ? publicationView.Files.Count(p => p.Value.ID == 0) : 0;
                if (publicationView.ID != 0 && countFiles > 0)
                {
                    Repository.CreateUpdateRecord(new UpdateRecord()
                    {
                        ResourceID = publication.ID,
                        MaterialType = (int)UpdateRecord.MaterialTypeEnum.Publication,
                        Type = countFiles == 1 ? (int)UpdateRecord.TypeEnum.NewFile : (int)UpdateRecord.TypeEnum.NewFiles,
                        AddedDate = DateTime.Now,
                        UserID = CurrentUser.ID
                    });
                }
                Repository.ClearPublicationFiles(publication.ID);
                if (publicationView.Files != null)
                {
                    foreach (var fileView in publicationView.Files)
                    {
                        var file = (Model.File)ModelMapper.Map(fileView.Value, typeof(FileView), typeof(Model.File));
                        Repository.CreateFile(file);

                        var publicationFile = new PublicationFile()
                        {
                            PublicationID = publication.ID,
                            FileID = file.ID
                        };
                        Repository.CreatePublicationFile(publicationFile);
                    }
                }

                if (publicationView.ID == 0)
                {
                    var authors = Repository.PublicationPersons.Where(p => p.PublicationID == publication.ID && p.PersonID != CurrentUser.Person.ID).Select(p => p.Person).ToList();
                    Subscription.AddMaterial(Repository, newSubjects, publication, authors);
                }
                else
                {
                    if (fillText)
                    {
                        var authors = Repository.PublicationPersons.Where(p => p.PublicationID == publication.ID).Select(p => p.Person).ToList();
                        Subscription.AddMaterialText(Repository, newSubjects, publication, authors);
                    }
                    if (countFiles > 0)
                    {
                        var authors = Repository.PublicationPersons.Where(p => p.PublicationID == publication.ID).Select(p => p.Person).ToList();
                        Subscription.AddMaterialFiles(Repository, newSubjects, publication, authors);
                    }
                }
                return RedirectToAction("Index");
            }
            switch ((Publication.TypeEnum)publicationView.Type)
            {
                case Publication.TypeEnum.Article:
                    return View("EditArticle", publicationView);
                case Publication.TypeEnum.Book:
                    return View("EditBook", publicationView);
                case Publication.TypeEnum.Thesis:
                    return View("EditThesis", publicationView);
            }
            return null;
        }

        public ActionResult Delete(int id)
        {
            var publication = Repository.Publications.FirstOrDefault(p => p.ID == id);
            if (publication != null)
            {
                Repository.RemovePublication(publication.ID);
            }
            return RedirectBack;
        }

        public ActionResult Access(int id)
        {
            var list = Repository.PublicationAccesses.Where(p => p.PublicationID == id).ToList();
            return View(list);
        }

        public ActionResult AddAccess(PublicationAccess publicationAccess)
        {
            if (publicationAccess.UserID != 0 && publicationAccess.PublicationID != 0)
            {
                var exist = Repository.PublicationAccesses.Any(p => p.PublicationID == publicationAccess.PublicationID && p.UserID == publicationAccess.UserID);

                if (!exist)
                {
                    Repository.CreatePublicationAccess(publicationAccess);
                    Subscription.GiveRight(Repository, publicationAccess);
                }
                return Json(new { result = "ok" });
            }
            return Json(new { result = "error" });
        }

        public ActionResult RemoveAccess(int id)
        {
            var exist = Repository.PublicationAccesses.Any(p => p.ID == id);
            if (exist)
            {
                Repository.RemovePublicationAccess(id);
            }
            return Json(new { result = "ok" });
        }

        public ActionResult Redirects(int id)
        {
            var list = Repository.PublicationRecordRedirects.Where(p => p.PublicationID == id).ToList();
            return View(list);
        }

        public ActionResult AddRedirect(PublicationRecordRedirect publicationRecordRedirect)
        {
            Repository.CreatePublicationRecordRedirect(publicationRecordRedirect);
            return Json(new { result = "ok" });
        }

        public ActionResult RemoveRedirect(int id)
        {
            var publicationRecordRedirect = Repository.PublicationRecordRedirects.FirstOrDefault(p => p.ID == id);
            if (publicationRecordRedirect != null)
            {
                Repository.RemoveRecordRedirect(publicationRecordRedirect.RecordRedirectID);
            }
            return Json(new { result = "ok" });
        }

        [HttpGet]
        public ActionResult EditBatch()
        {
            using (var fs = new StreamReader(Server.MapPath("/Content/publication.txt")))
            {
                var batch = fs.ReadToEnd();
                fs.Close();

                var items = batch.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in items)
                {
                    var parts = item.Split(new string[] { "\t" }, StringSplitOptions.None);

                    if (parts.Count() < 13)
                    {
                        logger.Debug("Break item " + item);
                        continue;
                    }
                    var type = parts[0];
                    var authors = parts[1];
                    var header = parts[2];
                    var bio = parts[3];
                    var year = parts[4];
                    var annotation = parts[5];
                    var files = parts[6];
                    var weblinks = parts[7];
                    var cover = parts[8];
                    var organization = parts[9];
                    var subjects = parts[10];
                    var regions = parts[11];
                    var shopWebLinks = parts[12];
                    if (parts.Count() > 13 && !string.IsNullOrWhiteSpace(parts[13]))
                    {
                        logger.Debug("Referer item " + parts[13]);
                    }

                    var typeInt = 0;
                    if (type == "диссер")
                    {
                        typeInt = (int)Publication.TypeEnum.Thesis;
                    }
                    if (type == "книга")
                    {
                        typeInt = (int)Publication.TypeEnum.Book;
                    }
                    if (type == "статья")
                    {
                        typeInt = (int)Publication.TypeEnum.Article;
                    }

                    var coverImage = "";
                    if (!string.IsNullOrWhiteSpace(cover))
                    {
                        try
                        {
                            var webClient = new WebClient();
                            var bytes = webClient.DownloadData(cover);
                            var ms = new MemoryStream(bytes);

                            var uDir = "Content/files/uploads/";
                            var uFile = StringExtension.GenerateNewFile() + Path.GetExtension(cover);
                            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);

                            using (var fsCover = new FileStream(filePath, FileMode.Open))
                            {
                                ms.CopyTo(fsCover);
                                ms.Flush();
                                ms.Close();
                                fsCover.Close();
                            }
                            coverImage = "/" + uDir + uFile;
                        }
                        catch (Exception ex)
                        {
                            logger.Debug("Error Cover: " + cover);
                        }
                    }
                    int? yearInt = null;
                    if (!string.IsNullOrWhiteSpace(year))
                    {
                        int outYear = 0;
                        if (Int32.TryParse(year, out outYear))
                        {
                            yearInt = outYear;
                        };
                    }
                    var publication = new Publication()
                    {
                        Bibliographic = bio,
                        Header = header,
                        Cover = coverImage,
                        Type = typeInt,
                        UserID = CurrentUser.ID,
                        Year = yearInt,
                        Teaser = annotation,
                        Content = "",
                        ParentID = null
                    };

                    Repository.CreatePublication(publication);

                    //Авторы
                    var authorParts = authors.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    var listOfPerson = new List<int>();
                    foreach (var author in authorParts)
                    {
                        var partsNames = author.Trim().Split(new string[] { " ", " " }, StringSplitOptions.RemoveEmptyEntries);

                        var lastName = "";
                        var firstName = "";
                        var patronymic = "";
                        if (partsNames.Count() > 0)
                        {
                            lastName = partsNames[0].Trim();
                        }
                        if (partsNames.Count() > 1)
                        {
                            firstName = partsNames[1].Trim();
                        }
                        if (partsNames.Count() > 2)
                        {
                            patronymic = partsNames[2].Trim();
                        }

                        if (firstName.Contains("."))
                        {
                            var divide = firstName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

                            firstName = divide[0] + ".";
                            if (divide.Count() > 1)
                            {
                                patronymic = divide[1] + ".";
                            }
                        }

                        var common = string.Format("l:{0} f:{1} p:{2}", lastName, firstName, patronymic);
                        var common2 = string.Format("{0} {1} {2}", lastName, firstName, patronymic);
                        Person person = null;
                        var once = Repository.Persons.Count(p => string.Compare(p.LastName, lastName, true) == 0);
                        if (once > 0)
                        {
                            logger.Debug("Найдено больше одного по фамилии: " + common);

                            //по инициалам 
                            person = Repository.Persons.FirstOrDefault(p => string.Compare(p.LastName, lastName, true) == 0 && string.Compare(p.FirstName, firstName, true) == 0 && string.Compare(p.Patronymic, patronymic, true) == 0);

                            if (person == null && firstName.Length > 0)
                            {
                                person = Repository.Persons.FirstOrDefault(p => string.Compare(p.LastName, lastName, true) == 0
                                    && p.FirstName.StartsWith(firstName.Substring(0, 1).ToUpper()));
                                if (person != null)
                                {
                                    logger.Debug("По инициалам 1 : " + common);
                                    if (!string.IsNullOrWhiteSpace(person.Patronymic) && patronymic.Length > 0)
                                    {
                                        person = Repository.Persons.FirstOrDefault(p => string.Compare(p.LastName, lastName, true) == 0
                                       && p.FirstName.StartsWith(firstName.Substring(0, 1).ToUpper())
                                       && p.Patronymic.StartsWith(patronymic.Substring(0, 1).ToUpper()));

                                        if (person != null)
                                        {
                                            logger.Debug("По инициалам 2 : " + common + " OK");
                                        }
                                        else
                                        {
                                            person = null;
                                        }
                                    }
                                }
                            }
                        }
                        if (person != null)
                        {
                            listOfPerson.Add(person.ID);
                            logger.Debug("Персона : " + common2);
                        }
                        else
                        {
                            logger.Debug("Can't found person : " + common2);
                        }
                    }
                    if (listOfPerson.Count > 0)
                    {
                        Repository.UpdatePublicationPerson(publication.ID, listOfPerson);
                    }
                    //Файлы
                    if (!string.IsNullOrWhiteSpace(files))
                    {
                        var fileParts = files.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var file in fileParts)
                        {
                            try
                            {
                                var webClient = new WebClient();
                                webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                                var bytes = webClient.DownloadData(file);
                                var ms = new MemoryStream(bytes);

                                var uDir = "Content/files/attaches/";
                                var extension = Path.GetExtension(file);
                                var uFile = StringExtension.GenerateNewFile() + extension;
                                var fileName = Path.GetFileName(file);
                                var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);

                                var mimeType = Config.MimeTypes.FirstOrDefault(p => string.Compare(p.Extension, extension, true) == 0);
                                if (mimeType != null)
                                {
                                    using (var fsFile = new FileStream(filePath, FileMode.Create))
                                    {
                                        ms.CopyTo(fsFile);
                                        fsFile.Flush();
                                    }
                                    var fileRecord = new Model.File()
                                    {
                                        IsImage = PreviewCreator.SupportMimeType(mimeType.Name),
                                        MimeType = mimeType.Name,
                                        Path = "/" + uDir + uFile,
                                        Name = fileName
                                    };
                                    Repository.CreateFile(fileRecord);

                                    var publicationFile = new PublicationFile()
                                    {
                                        PublicationID = publication.ID,
                                        FileID = fileRecord.ID
                                    };
                                    Repository.CreatePublicationFile(publicationFile);

                                    logger.Debug("Файл: " + fileRecord.Name);
                                }
                                else
                                {
                                    logger.Debug("Нет mime типа " + file);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("Ошибка в " + file);
                            }
                        }
                    }
                    //Ссылки
                    if (!string.IsNullOrWhiteSpace(weblinks))
                    {
                        var linksParts = weblinks.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var link in linksParts)
                        {
                            try
                            {
                                var filePath = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
                                link.GetFavicon(Server.MapPath(filePath));
                                var title = link.Substring(0, link.LastIndexOf(".")).ToUpper() + link.Substring(link.LastIndexOf("."));
                                var linkRecord = new Link()
                                {
                                    Icon = filePath,
                                    Url = link,
                                    Title = title
                                };
                                Repository.CreateLink(linkRecord);

                                var publicationLink = new PublicationLink()
                                {
                                    IsShop = false,
                                    LinkID = linkRecord.ID,
                                    PublicationID = publication.ID
                                };

                                Repository.CreatePublicationLink(publicationLink);
                                logger.Debug("Ссылка: " + linkRecord.Url);
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("Ошибка в " + link);
                            }
                        }
                    }
                    //Магазинные ссылки
                    if (!string.IsNullOrWhiteSpace(weblinks))
                    {
                        var linksParts = shopWebLinks.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var link in linksParts)
                        {
                            try
                            {
                                var filePath = "/Content/files/downloads/" + StringExtension.GenerateNewFile() + ".ico";
                                link.GetFavicon(Server.MapPath(filePath));
                                var title = link.Substring(0, link.LastIndexOf(".")).ToUpper() + link.Substring(link.LastIndexOf("."));
                                var linkRecord = new Link()
                                {
                                    Icon = filePath,
                                    Url = link,
                                    Title = title
                                };
                                Repository.CreateLink(linkRecord);

                                var publicationLink = new PublicationLink()
                                {
                                    IsShop = true,
                                    LinkID = linkRecord.ID,
                                    PublicationID = publication.ID
                                };

                                Repository.CreatePublicationLink(publicationLink);
                                logger.Debug("Ссылка: " + linkRecord.Url);
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("Ошибка в " + link);
                            }
                        }
                    }
                    //Регионы
                    var listOfRegions = new List<int>();
                    if (!string.IsNullOrWhiteSpace(regions))
                    {
                        var regionsParts = regions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var region in regionsParts)
                        {
                            try
                            {
                                var regionRecord = Repository.Regions.FirstOrDefault(p => string.Compare(p.Name, region.Trim(), true) == 0);

                                if (regionRecord != null)
                                {
                                    listOfRegions.Add(regionRecord.ID);
                                    logger.Debug("Регион: " + regionRecord.Name);
                                }
                                else
                                {
                                    logger.Debug("не найден регион " + region);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("Ошибка в " + region);
                            }
                        }
                        Repository.UpdatePublicationRegion(publication.ID, listOfRegions);
                    }

                    //Темы
                    var listOfSubjects = new List<int>();
                    if (!string.IsNullOrWhiteSpace(subjects))
                    {
                        var subjectsParts = subjects.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var subject in subjectsParts)
                        {
                            try
                            {
                                var subjectRecord = Repository.Subjects.FirstOrDefault(p => string.Compare(p.Name, subject.Trim(), true) == 0);

                                if (subjectRecord != null)
                                {
                                    listOfSubjects.Add(subjectRecord.ID);
                                    logger.Debug("Тема: " + subjectRecord.Name);
                                }
                                else
                                {
                                    logger.Debug("не найдена тема " + subject);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Debug("Ошибка в " + subject);
                            }
                        }
                        Repository.UpdatePublicationSubject(publication.ID, listOfSubjects);
                    }
                }
                return View("EditBatch", (object)"OK");
            }
        }

        public ActionResult AttachOrganization()
        {
            using (var fs = new StreamReader(Server.MapPath("/Content/publication.txt")))
            {
                var batch = fs.ReadToEnd();
                fs.Close();

                var items = batch.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in items)
                {
                    var parts = item.Split(new string[] { "\t" }, StringSplitOptions.None);

                    if (parts.Count() < 13)
                    {
                        logger.Debug("Break item " + item);
                        continue;
                    }
                    var type = parts[0];
                    var authors = parts[1];
                    var header = parts[2];
                    var bio = parts[3];
                    var year = parts[4];
                    var annotation = parts[5];
                    var files = parts[6];
                    var weblinks = parts[7];
                    var cover = parts[8];
                    var organization = parts[9];
                    var subjects = parts[10];
                    var regions = parts[11];
                    var shopWebLinks = parts[12];



                    var publication = Repository.Publications.FirstOrDefault(p => string.Compare(p.Header, header, true) == 0);
                    if (publication != null)
                    {
                        if (!string.IsNullOrWhiteSpace(organization))
                        {
                            var listOfOrganization = new List<int>();
                            var organizations = organization.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var org in organizations)
                            {
                                var organizationRecord = Repository.Organizations.FirstOrDefault(p => string.Compare(p.Name, org.Trim(), true) == 0);

                                if (organizationRecord != null)
                                {
                                    listOfOrganization.Add(organizationRecord.ID);
                                }
                                else
                                {
                                    logger.Debug("Can't find " + org + " Item " + item);

                                }
                            }
                            if (listOfOrganization.Count > 0)
                            {
                                Repository.UpdatePublicationOrganization(publication.ID, listOfOrganization);
                            }
                        }
                    }
                }
            }
            return View("EditBatch", (object)"OK");
        }

        public ActionResult Moderate(int id)
        {
            Repository.ModeratePublication(id);
            return RedirectBack;
        }
    }
}