using AutoMapper;
using BooksApi.Books;

namespace BooksApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Book, GetBookDto>();
        CreateMap<CreateBookDto, Book>();
    }
}