using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdrianBookStore.Models
{
    public class CheckoutDetails
    {
        public Cart CurrentCart { get; set; }

        public string ContactEmail { get; set; }
        public string ContactName { get; set; }

        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        //TODO: Collect more information, Billing Address/Card Info
    }
}