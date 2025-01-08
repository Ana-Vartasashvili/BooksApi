namespace BooksApi.Books;

public class GetAllBooksRequest
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? Title { get; set; }
}