using System;
using System.Collections.Generic;
using System.Linq;
using AdrianBookStore.Models;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    public class CartController : Controller
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

        // GET: Cart
        public ActionResult Index()
        {
            //Guid cartID = Guid.Parse(Request.Cookies["cartID"].Value);
            Guid? cartID = this.GetCartID();
            return View(db.Carts.Find(cartID));
        }



        // POST: Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Cart model)
        {
            var cart = db.Carts.Find(model.ID);
            for (int i = 0; i < model.Cart_Books.Count; i++)
            {
                cart.Cart_Books.ElementAt(i).Quantity = model.Cart_Books.ElementAt(i).Quantity;
            }

            db.Cart_Books.RemoveRange(cart.Cart_Books.Where(x => x.Quantity == 0));
            db.SaveChanges();
            return View(cart);
        }
    }
}