using System.Linq;
using System.Web.Mvc;
using gender.Helpers;
using gender.Models.ViewModels;
using gender.Controllers;
using gender.Models.ViewModels.User;
using gender.Tools.Mail;
using System.Collections.Generic;
using gender.Model;
using gender.Tools;

namespace gender.Areas.Default.Controllers
{
    public class LoginController : DefaultController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                var user = Auth.Login(loginView.Login, loginView.Password, loginView.IsPersistent);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState["Password"].Errors.Add(new ModelError("Пароль не верный"));
            }
            return View(loginView);
        }

        public ActionResult Logout()
        {
            Auth.LogOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordView());
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordView forgotPasswordView)
        {
            if (ModelState.IsValid)
            {
                var userEmail =
                    Repository.UserEmails.FirstOrDefault(p => string.Compare(p.Email, forgotPasswordView.Email, true) == 0);

                if (userEmail != null)
                {
                    NotifyMail.SendNotify("ForgotPassword", userEmail.Email,
                                                format => string.Format(format, HostName),
                                                format => string.Format(format, userEmail.Email, userEmail.User.Password, HostName));
                    return View("ForgotPasswordSuccess");
                }
                ModelState.AddModelError("Email", "Пользователь с данным Email не найден");
            }
            return View(forgotPasswordView);
        }
    }

}
