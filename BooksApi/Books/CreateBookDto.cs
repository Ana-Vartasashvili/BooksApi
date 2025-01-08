using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Books;

public class CreateBookDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? ISBN { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public decimal? Rating { get; set; }
}

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    private readonly AppDbContext _dbContext;

    public CreateBookDtoValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(x=>x.Author).NotEmpty();
        RuleFor(x=>x.ISBN).NotEmpty();
        RuleFor(x => x.Title).NotEmpty()
            .MustAsync(NotDuplicate)
            .WithMessage("Book with this title already exists");
    }

    private async Task<bool> NotDuplicate(string? title, CancellationToken cancellationToken)
    {
        var bookWithSameTitle = await _dbContext.Books.SingleOrDefaultAsync(b=>b.Title==title);
        
        return bookWithSameTitle == null;
    }
}