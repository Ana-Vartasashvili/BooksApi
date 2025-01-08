namespace BooksApi.Books;

public class Book
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public required bool IsAvailable { get; set; }
    public DateTime? PublishDate { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public decimal? rating { get; set; }
}