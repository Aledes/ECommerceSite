using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdrianBookStore.Models
{
    public class Book
    {
        public string Title { get; set; } //Matches name of Form in View 'Create.cshtml'
        public string Author { get; set; }
        public int Year { get; set; }
        public string CoverImage { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}