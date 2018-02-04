using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class ProductController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product {ID = 1, Name = "Tomato soup", Category = "Groceries", Price=1},
            new Product {ID = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M},
            new Product {ID = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M}
        };

        public IEnumerable<Product> GetAllProducts ()
        {
            return this.products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault<Product>(x => x.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //public HttpResponseMessage DeleteProduct(int id) { }
    }
}
