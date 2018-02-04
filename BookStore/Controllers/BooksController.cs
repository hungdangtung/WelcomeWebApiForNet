using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BooksController : ApiController
    {

        private BookStore.Models.IBookRepository bookRepository = null;

        public BooksController()
        {
            this.bookRepository = new BookStore.Models.BookRepository();
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            IEnumerable<BookStore.Models.BookDetails> books = this.bookRepository.ReadAllBooks();

            if (books != null)
            {
                return Request.CreateResponse<IEnumerable<BookStore.Models.BookDetails>>(HttpStatusCode.OK, books);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            BookDetails book = this.bookRepository.ReadBook(id);

            if (book != null)
            {
                return Request.CreateResponse<BookDetails>(HttpStatusCode.OK, book);
            } 
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(BookDetails book)
        {
            if (this.ModelState.IsValid && book != null)
            {
                var newbook = this.bookRepository.CreateBook(book);
                if (newbook != null )
                {
                    var httpResponse = Request.CreateResponse(HttpStatusCode.Created, newbook);
                    string uri = Url.Link("DefaultApi", new { id = newbook.ID });
                    httpResponse.Headers.Location = new Uri(uri);
                    return httpResponse;
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public HttpResponseMessage Put(string id, BookDetails book)
        {
            if (this.ModelState.IsValid && book != null && book.ID.Equals(id, StringComparison.CurrentCultureIgnoreCase))
            {
                BookDetails bookModify = this.bookRepository.UpdateBook(id, book);
                if (bookModify != null)
                {
                    return Request.CreateResponse<BookDetails>(HttpStatusCode.OK, bookModify);
                }
                else
                {
                    Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public HttpResponseMessage Detete(string id)
        {
            BookDetails book = this.bookRepository.ReadBook(id);
            if (book != null)
            {
                if (this.bookRepository.DeleteBook(id))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
