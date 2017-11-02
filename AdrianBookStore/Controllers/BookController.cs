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
        public ActionResult List()
        {
            List<Book> books = new List<Book>();
            books.Add(new Book
            {
                ID = 1,
                Author = "George Orwell",
                Title = "Nineteen Eighty-Four",
                Year = 1949,
                CoverImage = "/Images/1984.jpeg",
                Price = 19.84m
            });
            books.Add(new Book
            {
                ID = 2,
                Author = "Ernest Cline",
                Title = "Ready Player One",
                Year = 2011,
                CoverImage = "/Images/readyplayerone.jpg",
                Price = 9.99m
            });
            books.Add(new Book
            {
                ID = 3,
                Author = "J. R. R. Tolkien",
                Title = "The Hobbit",
                Year = 1937,
                CoverImage = "/Images/the-hobbit.jpg",
                Price = 12.99m
            });
            books.Add(new Book
            {
                ID = 4,
                Author = "Orson Scott Card",
                Title = "Ender's Game",
                Year = 1985,
                CoverImage = "/Images/endersgame.jpg",
                Price = 11.99m
            });
            return View(books);

        }
        // GET: Book
        public ActionResult Index(int id)
        {
            var bookEntry = new Models.Book();
            if(id == 1)
            {
                bookEntry.Author = "George Orwell";
                bookEntry.Title = "Nineteen Eighty-Four";
                bookEntry.Year = 1949;
                bookEntry.CoverImage = "/Images/1984.jpeg";
                bookEntry.Price = 19.84m;
            }
            else if(id == 2)
            {
                bookEntry.Author = "Ernest Cline";
                bookEntry.Title = "Ready Player One";
                bookEntry.Year = 2011;
                bookEntry.CoverImage = "/Images/readyplayerone.jpg";
                bookEntry.Price = 9.99m;
            }
            else if(id == 3)
            {
                bookEntry.Author = "J. R. R. Tolkien";
                bookEntry.Title = "The Hobbit";
                bookEntry.Year = 1937;
                bookEntry.CoverImage = "/Images/the-hobbit.jpg";
                bookEntry.Price = 12.99m;
            }
            else if (id == 4)
            {
                bookEntry.Author = "Orson Scott Card";
                bookEntry.Title = "Ender's Game";
                bookEntry.Year = 1985;
                bookEntry.CoverImage = "/Images/endersgame.jpg";
                bookEntry.Price = 11.99m;
            }
            else
            {
                return HttpNotFound("This Product Doesn't Exist!");
            }
            //Returning View() will just return the CSHTML result without any associated model.  Good for pages with static data.
            //return View();

            return View(bookEntry); //This returns the view with an object tied to it... the view should be "strongly typed" to the same class as the model I'm passing here
            /*
            //Most common thing to do in a controller. Looks for matching CSHTML file in the Views folder
                //In this case the Index.cshtml file in the Turkey folder would be used.

            //return this.RedirectToAction("Index", "Home") //home page. ("google.com") //Issues a 302 redirect,
                //commonly used after login, or during checkout, pointing them to a receipt.

            //return this.HttpNotFound sends 404, good way to temporarily block unlogged or unauthorized users

            //return this.Content use to test, or receive data?

            //return this.Json(Any Object, JsonRequestBehavior.AllowGet) //Takes any object and returns JSON inerpretation of it.

            //Other helpful objects:
            //this.Profile //Data about current user
            //this.Context //access underlying ASP request/response objects associated with transaction
            //this.Request //Data about the HTTP Request - Check cookies, headers, authorizations
            //this.Response // Data in the response - set cookies and headers
            */
        }

        //GET : Book/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST : Book/Create
        
        [HttpPost]
        public ActionResult LoanBook(Book book)
        {
            //TODO: Save the posted information to a database!

            //TODO: Rip out this cookie code later - we're going to use it for now to mock up site functionality.
            return Content("Thanks for the new entry!");
        }
        [HttpPost]
        public ActionResult Index(Book book)
        {
            //ToDo: Save posted information to a database!
            //ToDo: Build up controller Cart!
            Response.AppendCookie(new HttpCookie("bookTitle", book.Title));
            Response.AppendCookie(new HttpCookie("productQuantity", book.Quantity.ToString()));
            Response.AppendCookie(new HttpCookie("bookPrice", book.Price.ToString()));
            return RedirectToAction("Index", "Cart");
        }

        
        public ActionResult Read()
        {
            return Content("KNOWLEDGE IS POWER");
        }
    }
}