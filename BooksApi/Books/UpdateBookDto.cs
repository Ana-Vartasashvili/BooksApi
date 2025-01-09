using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Books;

public class UpdateBookDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? ISBN { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public decimal? Rating { get; set; }
}

public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
{
    private readonly AppDbContext _dbContext;

    public UpdateBookDtoValidator(AppDbContext context)
    {
        _dbContext = context;

        RuleFor(x => x.Author).NotEmpty().When(x => x.Author != null);
        RuleFor(x => x.ISBN).NotEmpty().When(x => x.ISBN != null);
        RuleFor(x => x.Title)
            .NotEmpty()
            .When(x => x.Title != null)
            .MustAsync(NotDuplicate).WithMessage("Book with this title already exists");
    }
    
    private async Task<bool> NotDuplicate(string? title, CancellationToken cancellationToken)
    {
        var bookWithSameTitle = await _dbContext.Books.SingleOrDefaultAsync(b=>b.Title==title, cancellationToken);
        
        return bookWithSameTitle == null;
    }
}