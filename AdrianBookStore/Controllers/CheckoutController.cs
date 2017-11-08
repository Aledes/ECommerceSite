using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            Models.CheckoutDetails details = new Models.CheckoutDetails();
            details.CurrentCart = new Models.Cart();

            ////Getting product data from cookies!
            ////TODO: Pull out of database.
            //CurrentCart.Books = new Models.Cart[1];
            //CurrentCart.Books[0] = new Models.Book();
            //CurrentCart.Books[0].Title = Request.Cookies["bookTitle"].Value;
            //CurrentCart.Books[0].Quantity = int.Parse(Request.Cookies["productQuantity"].Value);
            //CurrentCart.Books[0].Price = decimal.Parse(Request.Cookies["bookPrice"].Value);
            //cart.SubTotal = cart.Books.Sum(x => x.Price * x.Quantity);
            //cart.Tax = cart.SubTotal * .1025m;
            //cart.Total = cart.SubTotal + cart.Tax + cart.ShippingAndHandling;

            return View(details);
        }

        // POST: Checkout
        public ActionResult Index(Models.CheckoutDetails model)
        {

            return View(model);
        }
    }
}