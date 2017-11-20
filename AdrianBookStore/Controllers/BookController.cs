using AdrianBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    
    public class BookController : Controller
    {
        protected BookStoreDBEntities db = new BookStoreDBEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult List()
        {
            return View(db.Books);

        }
        // GET: Book
        public ActionResult Index(int id)
        {
            return View(db.Books.Find(id)); 
        }

        [HttpPost]
        public ActionResult Index(Book model)
        {
            //Save posted information to a database!
            Guid cartID;
            Cart cart = null;
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                cartID = Guid.Parse(Request.Cookies["cartID"].Value);
                cart = db.Carts.Find(cartID);
            }
            if (cart == null)
            {
                cartID = Guid.NewGuid();
                cart = new Cart
                {
                    ID = cartID,
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow
                };
                db.Carts.Add(cart);
                Response.AppendCookie(new HttpCookie("cartID", cartID.ToString()));
            }

            Cart_Books book = cart.Cart_Books.FirstOrDefault(x => x.BookID == model.BookID);
            if (book == null)
            {
                book = new Cart_Books
                {
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow,
                    BookID = model.BookID,
                    Quantity = 0
                };
                cart.Cart_Books.Add(book);
            }

            book.Quantity += model.Quantity ?? 1;
            book.DateLastModified = DateTime.UtcNow;
            cart.DateLastModified = DateTime.UtcNow;

            db.SaveChanges();


            TempData.Add("NewItem", model.Title);

            //TODO: build up the cart controller!
            return RedirectToAction("Index", "Cart");

        }
    }
}