using System.Net.Http.Json;
using BooksApi;
using BooksApi.Books;

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
}