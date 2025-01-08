namespace BooksApi.Books;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public decimal? Rating { get; set; }
}