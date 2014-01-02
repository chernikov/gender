using System;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using Ninject;
using gender.Model;

namespace gender.Global.Auth
{
    public class CustomAuthentication : IAuthentication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private const string cookieName = "__AUTH_COOKIE";

        public IAuthCookieProvider AuthCookieProvider { get; set; }

        [Inject]
        public IRepository Repository { get; set; }

        #region IAuthentication Members

        public User Login(string email, string Password, bool isPersistent)
        {
            User retUser = Repository.Login(email, Password);
            if (retUser != null)
            {
                CreateCookie(retUser.Login, isPersistent);
            }
            return retUser;
        }

        public User Login(string login)
        {
            User retUser = Repository.Users.FirstOrDefault(p => string.Compare(p.Login, login, true) == 0);
            if (retUser != null)
            {
                CreateCookie(login);
            }
            return retUser;
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            AuthCookieProvider.SetCookie(AuthCookie);
        }

        public void LogOut()
        {
            var httpCookie = new HttpCookie(cookieName)
            {
                Value = string.Empty,
                Expires = DateTime.Now
            };
            AuthCookieProvider.SetCookie(httpCookie);
        }

        private IPrincipal _currentUser;

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = AuthCookieProvider.GetCookie(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, Repository);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Failed authentication: " + ex.Message);
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }
        #endregion

       
    }
}