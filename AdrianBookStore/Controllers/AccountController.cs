using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    public class AccountController : Controller
    {
        PAAPaymentService paaPaymentService = new PAAPaymentService();
        // GET: Account
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var customer = await paaPaymentService.GetCustomerAsync(User.Identity.Name);
            return View(customer);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Index(string firstName, string lastName, string id)
        {
            Braintree.Customer customer = await paaPaymentService.UpdateCustomerAsync(firstName, lastName, id);

            ViewBag.Message = "Updated Successfully";
            return View(customer);
        }
        [Authorize]
        public async Task<ActionResult> Addresses()
        {
            var customer = await paaPaymentService.GetCustomerAsync(User.Identity.Name);
            return View(customer.Addresses);
        }

        [Authorize]
        public async Task<ActionResult> DeleteAddress(string id)
        {
            await paaPaymentService.DeleteAddressAsync(User.Identity.Name, id);
            TempData["SucessMessage"] = "Address deleted successfully";
            return RedirectToAction("Addresses");
        }

        [Authorize]
        public async Task<ActionResult> AddAddress(string firstName, string lastName, string company, string streetAddress, string extendedAddress, string locality, string region, string postalCode, string countryName)
        {
            await paaPaymentService.AddAddressAsync(User.Identity.Name, firstName, lastName, company, streetAddress, extendedAddress, locality, region, postalCode, countryName);

            TempData["SucessMessage"] = "Address added successfully";
            return RedirectToAction("Addresses");
        }

        // GET: Account/Register/
        public ActionResult Register()
        {
            return View();
        }

        //POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(string username, string password)
        { 
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser { Email = username, UserName = username };

            IdentityResult result = await userManager.CreateAsync(user, password);
	        if (result.Succeeded)
	        {
		        var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
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

        public ActionResult SignIn()
        {
            return View();
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
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if(user != null)
            {
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword?email=" + email + "&token=" + resetToken;
                string message = string.Format( "<a href=\"{0}\">Reset your password</a>", resetUrl);
                await userManager.SendEmailAsync(user.Id, "your password reset token", resetToken);
            }

            return RedirectToAction("ForgotPasswordSent");
        }

        public ActionResult ForgotPasswordSent()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                IdentityResult result = await userManager.ResetPasswordAsync(user.Id, token, newPassword);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Your password has been successfully updated";
                    return RedirectToAction("SignIn", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

//Create Login/Logout Page