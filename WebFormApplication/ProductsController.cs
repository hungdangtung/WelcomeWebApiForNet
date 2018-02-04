using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebFormApplication
{
    public class ProductsController : ApiController
    {

        Product[] products = new Product[]
        {
            new Product {ID = 1, Name = "Tomato soup", Category = "Groceries", Price=1},
            new Product {ID = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M},
            new Product {ID = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M}
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = products.FirstOrDefault<Product>(x => x.ID == id);

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return product;
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return products.Where<Product>(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase));
        }

    }
}