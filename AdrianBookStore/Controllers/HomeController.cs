using AdrianBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Poor Adrian's Almanac!";
            return View();
        }
        
        public ActionResult Store()
        {
            ViewBag.Message = "The Collection";

            return View();
        }

        protected BookStoreDBEntities db = new BookStoreDBEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string searchString)
        {
            var matchedBooks = db.Books.Where(x => x.Title.Contains(searchString) || x.Authors.Select(y => y.firstName).Contains(searchString) || x.Authors.Select(y => y.lastName).Contains(searchString));
            return View(matchedBooks);
        }
    }
}