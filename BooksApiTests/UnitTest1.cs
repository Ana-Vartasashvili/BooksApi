using System.Net;
using System.Net.Http.Json;
using BooksApi;
using BooksApi.Books;
using Microsoft.AspNetCore.Mvc;

namespace BooksApiTests;

public class BasicTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public BasicTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetAllBooks_ReturnsOkResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/books");
        
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to get books: {content}");
        }
        var books = await response.Content.ReadFromJsonAsync<IEnumerable<GetBookDto>>();
        
        Assert.NotEmpty(books);
    }

    [Fact]
    public async Task GetAllBooks_ReturnsFilteredBooks()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/books?Title=Book 1");
        
        response.EnsureSuccessStatusCode();
       var books=await response.Content.ReadFromJsonAsync<IEnumerable<GetBookDto>>();
       
       Assert.Single(books);
    }

    [Fact]
    public async Task GetBookById_ReturnsOkResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/books/1");
        
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetBookById_ReturnsNotFoundResult()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/books/999999");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateBook_ReturnsCreatedResult()
    {
        var client = _factory.CreateClient();
        var book = new CreateBookDto
        {
            Title = "Book test",
            Author = "Author test",
            ISBN = "123456789",

        };
        
        var response = await client.PostAsJsonAsync("/books", book);
        
        response.EnsureSuccessStatusCode();
        
        var createdBook = await response.Content.ReadFromJsonAsync<GetBookDto>();
        Assert.NotNull(createdBook);
    }

    [Fact]
    public async Task CreateBook_ReturnsBadRequestResult()
    {
        var client = _factory.CreateClient();
        var invalidBook = new CreateBookDto();
        
        var response = await client.PostAsJsonAsync("/books", invalidBook);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(problemDetails);
        Assert.Contains("Title", problemDetails.Errors.Keys);
        Assert.Contains("Author", problemDetails.Errors.Keys);
        Assert.Contains("ISBN", problemDetails.Errors.Keys);
        Assert.Contains("'Title' must not be empty.", problemDetails.Errors["Title"]);
        Assert.Contains("'Author' must not be empty.", problemDetails.Errors["Author"]);
        Assert.Contains("'ISBN' must not be empty.", problemDetails.Errors["ISBN"]);
    }

    [Fact]
    public async Task CreateBook_ReturnsBadRequestWhenBookAlreadyExists()
    {
        var client = _factory.CreateClient();
        var existingBookTitle = "Book 1";
        var book = new CreateBookDto
        {
            Title = existingBookTitle,
            Author = "Author test",
            ISBN = "111111"
        };
        
        var response = await client.PostAsJsonAsync("/books", book);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(problemDetails);
        Assert.Contains("Title", problemDetails.Errors.Keys);
        Assert.Contains("Book with this title already exists", problemDetails.Errors["Title"]);
    }
}