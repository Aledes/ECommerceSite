using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdrianBookStore.Models;

namespace AdrianBookStore.Controllers
{
    //Need to set Admin roles in Database
    //[Authorize(Roles = "Administrator")]
    public class BooksAdminController : Controller
    {
        private BookStoreDBEntities db = new BookStoreDBEntities();

        // GET: BooksAdmin
        public async Task<ActionResult> Index()
        {
            return View(await db.Books.ToListAsync());
        }

        // GET: BooksAdmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: BooksAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookID,Title,Year,Price,Quantity,CoverImage,Description,DateCreated,rowguid,NewColumn")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: BooksAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BooksAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Book book, HttpPostedFileBase imagefile)
        {
            if (ModelState.IsValid)
            {
                book.rowguid = Guid.NewGuid();
                //Replace local storage with Blob storage to update images universally
                //App configuration settings
                //CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"];
                //var blobClient = account.CreateCloudBlobClient();
                //var rootContainer = blobClient.GetRootContainerReference();
                //await rootContainer.CreateIfNotExistsAsync();
                //await rootContainer.SetPermissionsAsync(new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessTy...}
                //var blob = rootContainer.GetBlockBlobReference(imagefile.FileName);
                //await blob.UploadFromStreamAsync....

                imagefile.SaveAs(Server.MapPath("/Images/" + imagefile.FileName));
                book.CoverImage = "/Images/" + imagefile.FileName;
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: BooksAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BooksAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
