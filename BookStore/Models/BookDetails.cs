using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BookStore.Models
{
    public class BookDetails
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }

    public interface IBookRepository
    {
        BookDetails CreateBook(BookDetails book);
        IEnumerable<BookDetails> ReadAllBooks();
        BookDetails ReadBook(string id);
        BookDetails UpdateBook(string id, BookDetails book);
        bool DeleteBook(string id);
    }

    public class BookRepository : IBookRepository
    {
        private string xmlFilename = null;
        private XDocument xmlDocument = null;

        public BookRepository()
        {
            try
            {
                xmlFilename = HttpContext.Current.Server.MapPath("~/app_data/Books.xml");
                xmlDocument = XDocument.Load(xmlFilename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookDetails CreateBook(BookDetails book)
        {
            try
            {
                var highestBook = (from bookNode in xmlDocument.Elements("catalog").Elements("book") orderby bookNode.Attribute("id").Value descending select bookNode).Take(1);
                string highestId = highestBook.Attributes("id").First().Value;

                string newID = "bk" + (Convert.ToInt32(highestId.Substring(2)) + 1).ToString();
                if (this.ReadBook(newID) == null)
                {
                    XElement bookCatalogRoot = xmlDocument.Elements("catalog").Single();
                    XElement newBook = new XElement("book", new XAttribute("id", newID));
                    XElement[] bookInfor = FormatBookData(book);
                    newBook.ReplaceNodes(bookInfor);
                    bookCatalogRoot.Add(newBook);
                    xmlDocument.Save(xmlFilename);
                    return this.ReadBook(newID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private XElement[] FormatBookData(BookDetails book)
        {
            XElement[] bookInfo = {
                new XElement("author", book.Author),
                new XElement("title", book.Title),
                new XElement("genre", book.Genre),
                new XElement("price", book.Price),
                new XElement("publish_date", book.PublishDate.ToString()),
                new XElement("description", book.Description)
            };

            return bookInfo;
        }

        public bool DeleteBook(string id)
        {
            try
            {
                if (this.ReadBook(id) != null)
                {
                    xmlDocument.Elements("catalog").Elements("book").Where(x => x.Attribute("id").Value.Equals(id, StringComparison.CurrentCultureIgnoreCase)).Remove();
                    xmlDocument.Save(xmlFilename);
                    return true;
                } else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
                throw;
            }
        }

        public IEnumerable<BookDetails> ReadAllBooks()
        {
            try
            {
                return (from book in xmlDocument.Elements("catalog").Elements("book")
                        orderby book.Attribute("id") descending
                        select new BookDetails
                        {
                            ID = book.Attribute("id").Value,
                            Author = book.Attribute("author").Value,
                            Description = book.Attribute("description").Value,
                            Genre = book.Attribute("genre").Value,
                            Price = Convert.ToDecimal(book.Attribute("price").Value),
                            PublishDate = Convert.ToDateTime(book.Attribute("publish_date").Value),
                            Title = book.Attribute("title").Value
                        }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public BookDetails ReadBook(string id)
        {
            try
            {
                return (from book in xmlDocument.Elements("catalog").Elements("book")
                        where string.Equals(book.Attribute("id").Value, id, StringComparison.CurrentCultureIgnoreCase)
                        select new BookDetails
                        {
                            ID = book.Attribute("id").Value,
                            Author = book.Attribute("author").Value,
                            Title = book.Attribute("title").Value,
                            Genre = book.Attribute("genre").Value,
                            Price = Convert.ToDecimal(book.Attribute("price").Value),
                            PublishDate = Convert.ToDateTime(book.Attribute("publish_date").Value),
                            Description = book.Attribute("description").Value
                        }).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookDetails UpdateBook(string id, BookDetails book)
        {
            try
            {
                XElement updateBook = xmlDocument.XPathSelectElement(String.Format("catalog/book[@id='{0}']", id));
                if (updateBook != null)
                {
                    XElement[] bookInfo = FormatBookData(book);
                    updateBook.ReplaceNodes(bookInfo);
                    xmlDocument.Save(xmlFilename);
                    return this.ReadBook(id);
                }
            }
            catch
            {
                throw;
            }
            return null;
        }
    }
}