using Braintree;
using AdrianBookStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmartyStreets;

namespace AdrianBookStore.Controllers
{
    public class CheckoutController : Controller
    {
        private PAAPaymentService paaPaymentService = new PAAPaymentService();
        protected BookStoreDBEntities db = new BookStoreDBEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<Address[]> InitializeAddressesAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                Customer customer = await paaPaymentService.GetCustomerAsync(User.Identity.Name);
                return customer.Addresses;
            }
            return new Address[0];
        }

        private async Task<Cart> InitializeCartAsync()
        {
            Guid? cartID = this.GetCartID(); //????
            return await db.Carts.FindAsync(cartID);
        }

        // GET: Checkout
        public async Task<ActionResult> Index()
        {
            CheckoutDetails details = new CheckoutDetails
            {
                Addresses = await InitializeAddressesAsync(),
                CurrentCart = await InitializeCartAsync()
            };
            if ((details.CurrentCart == null) || (!details.CurrentCart.Cart_Books.Any()))
            {
                return RedirectToAction("Index", "Cart");
            }
            return View(details);
        }

        //POST : Checkout
        [HttpPost]
        public async Task<ActionResult> Index(Models.CheckoutDetails model, string addressId)
        {
            Guid cartID = Guid.Parse(Request.Cookies["CartID"].Value);

            model.CurrentCart = db.Carts.Find(cartID);
            model.Addresses = new Braintree.Address[0];

            if (ModelState.IsValid)
            {
                string trackingNumber = Guid.NewGuid().ToString().Substring(0, 8);
                decimal tax = (model.CurrentCart.Cart_Books.Sum(x => x.Book.Price * x.Quantity) ?? 0) * .1025m;
                decimal subtotal = model.CurrentCart.Cart_Books.Sum(x => x.Book.Price * x.Quantity) ?? 0;
                decimal shipping = model.CurrentCart.Cart_Books.Sum(x => x.Quantity);
                decimal total = subtotal + tax + shipping;

                #region pay for order
                PAAPaymentService payments = new PAAPaymentService();
                string email = User.Identity.IsAuthenticated ? User.Identity.Name : model.ContactEmail;
                string message = await payments.AuthorizeCard(email, total, tax, trackingNumber, addressId, model.CardholderName, model.CVV, model.CreditCardNumber, model.ExpirationMonth, model.ExpirationYear);
                #endregion 

                #region save order
                if (string.IsNullOrEmpty(message))
                {
                    Order o = new Order
                    {
                        DateCreated = DateTime.UtcNow,
                        DateLastModified = DateTime.UtcNow,
                        TrackingNumber = trackingNumber,
                        ShippingAndHandling = shipping,
                        Tax = tax,
                        SubTotal = subtotal,
                        Email = model.ContactEmail,
                        PurchaserName = model.ContactName,
                        ShippingAddress1 = model.ShippingAddress,
                        ShippingCity = model.ShippingCity,
                        ShippingPostalCode = model.ShippingPostalCode,
                        ShippingState = model.ShippingState
                    };
                    db.Orders.Add(o);

                    await db.SaveChangesAsync();
                    #endregion

                    #region send email

                    PAAEmailService emailService = new PAAEmailService();
                    await emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                    {
                        Subject = "Your receipt for order " + trackingNumber,
                        Destination = model.ContactEmail,
                        Body = "Thank you for shopping"
                    });
                    #endregion

                    #region Reset Cart
                    Response.SetCookie(new HttpCookie("cartID") { Expires = DateTime.UtcNow });

                    db.Cart_Books.RemoveRange(model.CurrentCart.Cart_Books);
                    db.Carts.Remove(model.CurrentCart);
                    db.SaveChanges();

                    #endregion 
                    return RedirectToAction("Index", "Receipt", new { id = trackingNumber });
                }
                ModelState.AddModelError("CreditCardNumber", message);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> ValidateAddress(string street, string city, string state, string zip)
        {
            string authId = ConfigurationManager.AppSettings["SmartyStreets.AuthID"];
            string authToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];
            SmartyStreets.ClientBuilder clientBuilder = new SmartyStreets.ClientBuilder(authId, authToken);
            var client = clientBuilder.BuildUsStreetApiClient();
            SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
            lookup.City = city;
            lookup.ZipCode = zip;
            lookup.Street = street;
            lookup.State = state;
            client.Send(lookup);

            return Json(lookup.Result.Select(x => new { street = x.DeliveryLine1, city = x.Components.CityName, state = x.Components.State, zip = x.Components.ZipCode + "-" + x.Components.Plus4Code }));

        }
    }
}

