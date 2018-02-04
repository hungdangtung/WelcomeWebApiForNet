namespace BookServices.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookServices.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BookServices.Models.BookServicesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookServices.Models.BookServicesContext context)
        {
            context.Authors.AddOrUpdate(x => x.ID,
                new Author() { ID = 1, Name = "Jane Austen"},
                new Author() { ID = 2, Name = "Charles Dickens"},
                new Author() { ID = 3, Name = "Miguel de Carvantes"}
                );
            context.Books.AddOrUpdate(x => x.ID, 
                new Book() { ID = 1, Title = "Pride of Prejudice", Year = 1813, AuthorID = 1, Price = 9.99M, Genre = "Comedy of Manners"},
                new Book() { ID = 2, Title = "Northanger Abbey", Year = 1817, AuthorID = 1, Price = 12.95M, Genre = "Gothic padory"},
                new Book() { ID = 3, Title = "David Copperfield", Year = 1850, AuthorID = 2, Price = 15M, Genre = "Billdungsroman"},
                new Book() { ID = 4, Title = "Don Quixote", Year = 1617, AuthorID =3, Price = 8.95M, Genre = "PicaresqueAdd"}
                );
        }
    }
}
