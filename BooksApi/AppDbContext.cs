using BooksApi.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksApi;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    DbSet<Book> Books { get; set; }
}