namespace BookAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookAPI.Models.BookAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookAPI.Models.BookAPIContext context)
        {

            context.Authors.AddOrUpdate(
                new Models.Author[] {
                new Models.Author() {AuthorId = 1, Name = "Ralls Kim"},
                new Models.Author() {AuthorId = 2, Name = "Corets Eva"},
                new Models.Author() {AuthorId = 3, Name = "Randall, Cathya"},
                new Models.Author() {AuthorId = 4, Name = "Thurman, Paula"}
            });

            context.Books.AddOrUpdate(new Models.Book[] {
                new Models.Book() {BookID = 1, Title = "Midnight Rain", Genre = "Fantasy", PublishDate = new DateTime(2000, 12, 06), AuthorId = 1, Description = "A former architect battles an evil sorcess.", Price = 14.55M},
                new Models.Book() {BookID = 2, Title = "Maeve Accendant", Genre = "Fantasy", PublishDate = new DateTime(2000, 11, 17), AuthorId = 2, Description = "After the collapse of nanotechnology sociaty, the young suvivals lay the foundation of a new society", Price = 12.5M},
                new Models.Book() {BookID = 3, Title = "The Sundered Gail", Genre = "Fantasy", PublishDate = new DateTime(2001, 09, 10), AuthorId = 2, Description = "The two daughter of Maeve Battle to control of England", Price = 12.95M},
                new Models.Book() {BookID = 4, Title = "Love Bird", Genre = "Romatic", PublishDate = new DateTime(2000, 09, 02), AuthorId = 3, Description = "When Carla meets Paul at an ornithology conferrence, temper fly", Price = 7.99M},
                new Models.Book() {BookID = 5, Title = "Splish Splash", Genre = "Romantic", PublishDate = new DateTime(2000, 10, 02), AuthorId = 4, Description = "A deep sea diver find true love 20,000 league beneas the sea", Price = 6.99M}
            });
        }
    }
}
