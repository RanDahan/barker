using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using barker.data;
using barker.repository.Interfaces;
using barker.repository.LinqToSql;
using barker.repository.Repositories;

namespace barker.Controllers
{
    public class SignupController : Controller
    {
        private IUserRepository accountRepository;

        public SignupController()
        {
            accountRepository = new UserRepository(new DataContext());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                accountRepository.InsertUser(model);
                accountRepository.Save();
            }

            return RedirectToAction("", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            var returnUrl = "";

            var user = accountRepository.GetUserByName(userName);
            if (user != null && user.Password == password)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            // If we got this far, something failed, redisplay form
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Signup");
        }
    }
}
