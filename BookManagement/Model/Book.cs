using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookManagement.Model
{
    public class Book
    {
        public int BooKId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Decimal Price { get; set; }
    }
}