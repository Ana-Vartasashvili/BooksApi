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

    /// <summary>
    /// Gets book by Id
    /// </summary>
    /// <param name="id">Book id</param>
    /// <returns>Returns book object</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetBookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetBookById(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<GetBookDto>(book));
    }

    /// <summary>
    /// Adds new book
    /// </summary>
    /// <returns>Returns created book object</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GetBookDto),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBook(CreateBookDto book)
    {
        var newBook = _mapper.Map<Book>(book);
        _dbContext.Add(newBook);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
    }

    /// <summary>
    /// Updates existing book
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateBookDto"></param>
    /// <returns>Returns updated book</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GetBookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBook(int id, [FromBody]UpdateBookDto updateBookDto)
    {
        var book = await _dbContext.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }
        
        _mapper.Map(updateBookDto, book);
        await _dbContext.SaveChangesAsync();
        
        return Ok(_mapper.Map<GetBookDto>(book));
    }

    /// <summary>
    /// Deletes book with a given Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }
        
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}