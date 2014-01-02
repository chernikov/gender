using gender.Model;
using gender.Models.ViewModels.User;
using gender.Social.Facebook;
using gender.Social.Google;
using gender.Social.Mailru;
using gender.Social.Twitter;
using gender.Social.Vkontakte;
using gender.Social.Yandex;
using gender.Tools;
using gender.Tools.Mail;
using ImageResizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace gender.Areas.Default.Controllers
{
    public class OAuthController : DefaultController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private FbProvider fbProvider;

        private VkProvider vkProvider;

        private GoogleProvider googleProvider;

        private TwitterProvider twitterProvider;

        private YandexProvider yandexProvider;

        private MailruProvider mailruProvider;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {

            fbProvider = new FbProvider();
            fbProvider.Config = Config.FacebookAppConfig;

            vkProvider = new VkProvider();
            vkProvider.Config = Config.VkAppConfig;

            googleProvider = new GoogleProvider();
            googleProvider.Config = Config.GoogleAppConfig;

            twitterProvider = new TwitterProvider();
            twitterProvider.Config = Config.TwitterAppConfig;

            yandexProvider = new YandexProvider();
            yandexProvider.Config = Config.YandexAppConfig;

            mailruProvider = new MailruProvider();
            mailruProvider.Config = Config.MailruAppConfig;

            base.Initialize(requestContext);
        }

        #region Register

        #region facebook

        public ActionResult FacebookLogin()
        {
            return Redirect(fbProvider.Authorize("http://" + HostName + "/OAuth/SaveFbCode"));
        }

        public ActionResult SaveFbCode()
        {
            if (Request.Params.AllKeys.Contains("code"))
            {
                var code = Request.Params["code"];
                return ProcessFbCode(code);
            }
            return View("CantInitialize");
        }

        protected ActionResult ProcessFbCode(string code)
        {
            if (fbProvider.GetAccessToken(code, "http://" + HostName + "/OAuth/SaveFbCode"))
            {
                var jObj = fbProvider.GetUserInfo();
                var fbUserInfo = JsonConvert.DeserializeObject<FbUserInfo>(jObj.ToString());
                var identifier = fbUserInfo.Identifier;
                if (TryLogin(identifier, fbUserInfo.Email))
                {
                    return RedirectToAction("Index", "User");
                }

                var registerView = new SocialRegisterUserView()
                {
                    Avatar = string.Format("http://graph.facebook.com/{0}/picture", fbUserInfo.Id),
                    Email = fbUserInfo.Email,
                    VerifiedEmail = true,
                    FirstName = fbUserInfo.FirstName,
                    LastName = fbUserInfo.LastName,
                    Login = string.Format("facebook-{0}__{1}", fbUserInfo.UserName, StringExtension.GenerateNewFile()),
                    Identifier = fbUserInfo.Identifier,
                    Provider = Model.UserSocial.ProviderType.facebook,
                    UserInfo = jObj.ToString()
                };
                return RegisterSocial(registerView);
            }
            return View("CantInitialize");
        }
        #endregion

        #region vkontakte

        public ActionResult VkontakteLogin()
        {
            return Redirect(vkProvider.Authorize("http://" + HostName + "/OAuth/SaveVkCode"));
        }

        public ActionResult SaveVkCode()
        {
            if (Request.Params.AllKeys.Contains("code"))
            {
                var code = Request.Params["code"];
                return ProcessVkCode(code);
            }
            return View("CantInitialize");
        }

        protected ActionResult ProcessVkCode(string code)
        {
            if (vkProvider.GetAccessToken(code, "http://" + HostName + "/OAuth/SaveVkCode"))
            {
                var jObj = vkProvider.GetUserInfo();
                logger.Debug("Vk Info : " + jObj.ToString());
                var vkUserInfoResonse = JsonConvert.DeserializeObject<VkUserInfoResponse>(jObj.ToString());
                var vkUserInfo = vkUserInfoResonse.Response[0];
                var identifier = vkUserInfo.Identifier;
                if (TryLogin(identifier, string.Empty))
                {
                    return RedirectToAction("Index", "User");
                }

                var vkLogin = !string.IsNullOrWhiteSpace(vkUserInfo.Nickname) ? vkUserInfo.Nickname : !string.IsNullOrWhiteSpace(vkUserInfo.Domain) ? vkUserInfo.Domain : string.Empty;
                var registerView = new SocialRegisterUserView()
                {
                    Avatar = vkUserInfo.Photo,
                    Email = string.Empty,
                    VerifiedEmail = false,
                    FirstName = vkUserInfo.FirstName,
                    LastName = vkUserInfo.LastName,
                    Login = string.Format("vk-{0}__{1}", vkLogin, StringExtension.GenerateNewFile()),
                    Identifier = vkUserInfo.Identifier,
                    Provider = Model.UserSocial.ProviderType.vk,
                    UserInfo = jObj.ToString()
                };
                return RegisterSocial(registerView);

            }
            return View("CantInitialize");
        }

        #endregion

        #region google

        public ActionResult GoogleLogin()
        {
            var sessionState = Guid.NewGuid().ToString("N");

            Session["GoogleSecretState"] = sessionState;
            return Redirect(googleProvider.Authorize("http://" + HostName + "/Oauth/SaveGoogleCode", sessionState));
        }

        public ActionResult SaveGoogleCode()
        {
            if (Request.Params.AllKeys.Contains("state"))
            {
                var state = Request.Params["state"];

                if (state != Session["GoogleSecretState"] as string)
                {
                    Response.StatusCode = 401;
                    return Content("Invalid state parameter");
                }
            }

            if (Request.Params.AllKeys.Contains("code"))
            {
                var code = Request.Params["code"];
                return ProcessGoogleCode(code);
            }
            return View("CantInitialize");
        }

        protected ActionResult ProcessGoogleCode(string code)
        {
            if (googleProvider.GetAccessToken(code, "http://" + HostName + "/Oauth/SaveGoogleCode"))
            {
                var jObj = googleProvider.GetUserInfo();
                var googleUserInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(jObj.ToString());
                var identifier = googleUserInfo.Identifier;

                if (TryLogin(identifier, googleUserInfo.Email))
                {
                    return RedirectToAction("Index", "User");
                }
                var registerView = new SocialRegisterUserView()
                {
                    Avatar = googleUserInfo.Picture,
                    Email = googleUserInfo.Email,
                    VerifiedEmail = googleUserInfo.Verified,
                    FirstName = googleUserInfo.FirstName,
                    LastName = googleUserInfo.LastName,
                    Login = string.Format("vk-{0}__{1}", googleUserInfo.Name, StringExtension.GenerateNewFile()),
                    Identifier = googleUserInfo.Identifier,
                    Provider = Model.UserSocial.ProviderType.google,
                    UserInfo = jObj.ToString()
                };
                return RegisterSocial(registerView);
            }
            return View("CantInitialize");
        }

        #endregion

        #region twitter

        public ActionResult TwitterLogin()
        {
            return Redirect(twitterProvider.Authorize("http://" + HostName + "/OAuth/SaveTwitterCode"));
        }

        public ActionResult SaveTwitterCode()
        {
            if (Request.Params.AllKeys.Contains("oauth_token") &&
                Request.Params.AllKeys.Contains("oauth_verifier"))
            {
                var token = Request.Params["oauth_token"];
                var verifier = Request.Params["oauth_verifier"];

                var accessToken = twitterProvider.GetAuthToken(token, verifier);
                return ProcessTwCode(accessToken);
            }
            return View("CantInitialize");
        }

        protected ActionResult ProcessTwCode(TwitterAccessToken accessToken)
        {
            twitterProvider.AccessToken = accessToken;
            var jObj = twitterProvider.GetUserInfo(accessToken.UserId);
            var twUserInfo = JsonConvert.DeserializeObject<TwitterUserInfo>(jObj.ToString());

            var identifier = twUserInfo.Identifier;

            if (TryLogin(identifier, null))
            {
                return RedirectToAction("Index", "User");
            }
            var strName = twUserInfo.Name.Split(' ');
            var name = string.Empty;
            var lastName = string.Empty;
            if (strName.Count() == 2)
            {
                name = strName[0];
                lastName = strName[1];
            }
            else
            {
                name = twUserInfo.Name;
            }

            var registerView = new SocialRegisterUserView()
            {
                Avatar = twUserInfo.Picture,
                Email = string.Empty,
                VerifiedEmail = false,
                FirstName = name,
                LastName = lastName,
                Login = string.Format("{0}__{1}", identifier, StringExtension.GenerateNewFile()),
                Identifier = identifier,
                Provider = Model.UserSocial.ProviderType.twitter,
                UserInfo = jObj.ToString()
            };
            return RegisterSocial(registerView);
        }

        #endregion

        #region yandex

        public ActionResult YandexLogin()
        {
            return Redirect(yandexProvider.Authorize());
        }

        public ActionResult SaveYandexCode()
        {
            if (Request.Params.AllKeys.Contains("code"))
            {
                var code = Request.Params["code"];
                return ProcessYandexCode(code);
            }
            return View("CantInitialize");
        }

        protected ActionResult ProcessYandexCode(string code)
        {
            if (yandexProvider.GetAccessToken(code))
            {
                var jObj = yandexProvider.GetUserInfo();
                var yandexUserInfo = JsonConvert.DeserializeObject<YandexUserInfo>(jObj.ToString());
                var identifier = yandexUserInfo.Identifier;
                if (TryLogin(identifier, yandexUserInfo.Email))
                {
                    return RedirectToAction("Index", "User");
                }

                var strName = yandexUserInfo.Name.Split(' ');
                var name = string.Empty;
                var lastName = string.Empty;
                if (strName.Count() == 2)
                {
                    name = strName[0];
                    lastName = strName[1];
                }
                else
                {
                    name = yandexUserInfo.Name;
                }
                var registerView = new SocialRegisterUserView()
                {
                    Avatar = "",
                    Email = yandexUserInfo.Email,
                    VerifiedEmail = true,
                    FirstName = name,
                    LastName = lastName,
                    Login = string.Format("yandex-{0}__{1}", yandexUserInfo.Identifier, StringExtension.GenerateNewFile()),
                    Identifier = yandexUserInfo.Identifier,
                    Provider = Model.UserSocial.ProviderType.yandex,
                    UserInfo = jObj.ToString()
                };
                return RegisterSocial(registerView);
            }
            return View("CantInitialize");
        }

        #endregion

        #region mailru

        public ActionResult MailruLogin()
        {
            return Redirect(mailruProvider.Authorize("http://" + HostName + "/OAuth/SaveMailruCode") );
        }

        public ActionResult SaveMailruCode()
        {
            if (Request.Params.AllKeys.Contains("code"))
            {
                var code = Request.Params["code"];
                return ProcessMailruCode(code);
            }
            return View("CantInitialize");
        }

        protected ActionResult ProcessMailruCode(string code)
        {
            if (mailruProvider.GetAccessToken(code, "http://" + HostName + "/OAuth/SaveMailruCode"))
            {
                var jObj = mailruProvider.GetUserInfo();
                var mailruUserInfo = JsonConvert.DeserializeObject<MailruUserInfo>(jObj.ToString());
                var identifier =  mailruUserInfo.Identifier;
                if (TryLogin(identifier, mailruUserInfo.Email))
                {
                    return RedirectToAction("Index", "User");
                }

              
                var registerView = new SocialRegisterUserView()
                {
                    Avatar = mailruUserInfo.Picture,
                    Email = mailruUserInfo.Email,
                    VerifiedEmail = true,
                    FirstName = mailruUserInfo.FirstName,
                    LastName = mailruUserInfo.LastName,
                    Login = string.Format("{0}__{1}", mailruUserInfo.Identifier, StringExtension.GenerateNewFile()),
                    Identifier = mailruUserInfo.Identifier,
                    Provider = Model.UserSocial.ProviderType.mailru,
                    UserInfo = jObj.ToString()
                };
                return RegisterSocial(registerView);
            }
            return View("CantInitialize");
        }
        #endregion

        private bool TryLogin(string identifier, string email)
        {
            var social = Repository.UserSocials.FirstOrDefault(p => p.Identified == identifier);

            if (social != null)
            {
                Auth.Login(social.User.Login);
                return true;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                var userEmail = Repository.UserEmails.FirstOrDefault(p => string.Compare(p.Email, email, true) == 0);
                if (userEmail != null)
                {
                    Auth.Login(userEmail.User.Login);
                    return true;
                }
            }
            return false;
        }

        public ActionResult RegisterSuccess()
        {
            ViewBag.EmailExist = !string.IsNullOrWhiteSpace(CurrentUser.Email);
            return View();
        }

        private ActionResult RegisterSocial(SocialRegisterUserView registerView)
        {
            try
            {
                //проверить логин (при необходимости - изменить)
                registerView.Login = Translit.Translate(registerView.Login).ToLower();
                registerView.Login = registerView.Login.Replace("-", "_");
                bool exist = Repository.Users.Any(p => string.Compare(p.Login, registerView.Login, true) == 0);
                var baseLogin = registerView.Login;
                if (exist)
                {
                    registerView.Login = registerView.Login + "_" + registerView.Provider.ToString();
                }
                exist = Repository.Users.Any(p => string.Compare(p.Login, registerView.Login, true) == 0);
                if (exist)
                {
                    while (true)
                    {
                        registerView.Login = baseLogin + "_" + Translit.Predicate();
                        exist = Repository.Users.Any(p => string.Compare(p.Login, registerView.Login, true) == 0);
                        if (!exist)
                        {
                            break;
                        }
                    }
                }
                //создать аккаунт 
                var user = (User)ModelMapper.Map(registerView, typeof(SocialRegisterUserView), typeof(User));
                var person = (Person)ModelMapper.Map(registerView, typeof(SocialRegisterUserView), typeof(Person));
                user.Password = StringExtension.GenerateNewFile();
                Repository.CreateUser(user);
                

                person.AuthorID = user.ID;
                person.UserID = user.ID;

                Repository.CreatePerson(person);

                //создать Social
                var social = new UserSocial()
                {
                    Identified = registerView.Identifier,
                    Provider = (int)registerView.Provider,
                    UserInfo = registerView.UserInfo,
                    JsonResource = string.Empty,
                    UserID = user.ID
                };
                Repository.CreateUserSocial(social);

                if (!string.IsNullOrWhiteSpace(registerView.Email))
                {
                    var userEmail = new UserEmail()
                    {
                        UserID = user.ID,
                        Email = registerView.Email,
                    };
                    Repository.CreateUserEmail(userEmail);
                    userEmail.ActivateDate = registerView.VerifiedEmail ? (DateTime?)DateTime.Now : null;
                    Repository.UpdateUserEmail(userEmail);

                    if (!userEmail.Activated)
                    {
                        NotifyMail.SendNotify("Register", userEmail.Email,
                                          format => string.Format(format, HostName),
                                          format => string.Format(format, userEmail.ActivateLink, HostName));
                    }
                    else
                    {
                        Repository.ActivateUser(user);
                    }
                }
                //скачать картинку (если есть)
                if (!string.IsNullOrWhiteSpace(registerView.Avatar))
                {
                    CreateAvatar(person, registerView.Avatar);
                }
                Auth.Login(user.Login);
                Subscription.NewUser(Repository, user.ID);
                return RedirectToAction("RegisterSuccess");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Debug("Can't initialize : " + registerView.UserInfo);
                return View("CantInitialize");
            }
        }

        public void CreateAvatar(Person person, string pictureUrl)
        {
            var webClient = new WebClient();
            var bytes = webClient.DownloadData(pictureUrl);
            var ms = new MemoryStream(bytes);

            var uDir = "Content/files/uploads/";
            var uFile = StringExtension.GenerateNewFile() + ".jpg";
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);
            try
            {
                ImageBuilder.Current.Build(ms, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));
                person.Photo = "/" + uDir + uFile;
                Repository.UpdatePerson(person);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        #endregion
    }
}