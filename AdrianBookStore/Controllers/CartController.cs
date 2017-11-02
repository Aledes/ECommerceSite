using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            Models.Cart cart = new Models.Cart();

            //Getting product data from cookies!
            //TODO: Pull out of database.
            if (Request.Cookies.AllKeys.Contains("bookTitle"))
            {
                cart.Books = new Models.Book[1];
                cart.Books[0] = new Models.Book();
                cart.Books[0].Title = Request.Cookies["bookTitle"].Value;
                cart.Books[0].Quantity = int.Parse(Request.Cookies["productQuantity"].Value);
                cart.Books[0].Price = decimal.Parse(Request.Cookies["bookPrice"].Value);
            }
            else
            {
                cart.Books = new Models.Book[0];
            }
            cart.SubTotal = cart.Books.Sum(x => x.Price * x.Quantity);
            cart.Tax = cart.SubTotal * .1025m;
            cart.Total = cart.SubTotal + cart.Tax + cart.ShippingAndHandling;


            return View(cart);
        }
    }
}