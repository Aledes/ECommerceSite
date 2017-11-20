using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using System.Net;

namespace AdrianBookStore.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            }
        }
        // GET: Account/Register/
        public ActionResult Register()
        {
            return View();
        }


        //POST: Account/Register
        [HttpPost]
        public ActionResult Register(string username, string password)
        { 
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser { Email = username, UserName = username };

            IdentityResult result = userManager.Create(user, password);
	        if (result.Succeeded)
	        {
		        var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
		        {
			        IsPersistent = true,
			        ExpiresUtc = DateTime.UtcNow.AddDays(7)
		        }, userIdentity);

		        return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = result.Errors;
	        return View();
        }

        //Logout Logic
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return View();
            //Return to homepage in 5s in viewpage logic?
        }

        [HttpPost]
        public ActionResult SignIn(string userName, string password, bool? staySignedIn)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByName(userName);
            if (user != null)
            {
                bool isPasswordValid = userManager.CheckPassword(user, password);
                if (isPasswordValid)
                {
                    var claimsIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                    {
                        IsPersistent = staySignedIn ?? false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }, claimsIdentity);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = new string[] { "Unable to sign in " };
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if(user != null)
            {
                string resetToken = userManager.GeneratePasswordResetToken(user.Id);
                userManager.SendEmail(user.Id, "your password reset token", resetToken);
            }

            return RedirectToAction("ForgotPasswordSent");
        }

        public ActionResult ForgotPasswordSent()
        {
            return View();
        }
    }
}

//Create Login/Logout Page