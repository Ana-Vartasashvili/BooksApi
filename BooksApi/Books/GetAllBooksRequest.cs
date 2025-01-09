using FluentValidation;

namespace BooksApi.Books;

public class GetAllBooksRequest
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? Title { get; set; }
}

public class GetAllBooksRequestValidator : AbstractValidator<GetAllBooksRequest>
{
    public GetAllBooksRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");
        RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("You can not get more than 100 items.");
    }
}
