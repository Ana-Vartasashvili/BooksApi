using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Books;

public class BooksController : BaseController
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public BooksController(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Gets all of the books
    /// </summary>
    /// <returns>Returns books in a JSON array</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetBookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllBooks([FromQuery] GetAllBooksRequest request)
    {
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;
        
        var query = _dbContext.Books
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
           query = query.Where(book => book.Title.Contains(request.Title)); 
        }

        var books = await query.ToArrayAsync();

        return Ok(books.Select(book=>_mapper.Map<GetBookDto>(book)));
    }
}