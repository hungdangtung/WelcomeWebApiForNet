using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookServices.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}