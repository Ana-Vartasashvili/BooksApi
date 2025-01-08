using BooksApi.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksApi;

public static class SeedData
{
    public static void MigrateAndSeed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        if (!context.Books.Any())
        {
            var books = new List<Book>
            {
                new Book
                {
                    Title = "Book 1",
                    Author = "Author 1",
                    ISBN = "123456789",
                    Genre = "Genre 1",
                    Description = "Book 1 description",
                    Publisher = "Publisher 1",
                    Rating = 1
                },
                
                new Book
                {
                    Title = "Book 2",
                    Author = "Author 2",
                    ISBN = "123456789222",
                    Genre = "Genre 2",
                    Description = "Book 2 description",
                    Publisher = "Publisher 2",
                    Rating = 2
                }
            };
            
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}