using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdrianBookStore.Models
{
    public class Cart
    {
        //Example of a composite model - reusing the 'Book' class that I previously created.
        public Book[] Books { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public decimal Total { get; set; }
    }
}