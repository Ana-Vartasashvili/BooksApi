namespace BooksApi.Books;

public class GetBookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public decimal? Rating { get; set; }
}