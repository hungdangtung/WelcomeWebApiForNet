using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookServices.Models
{
    public class BookDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }
}