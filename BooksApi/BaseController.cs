using Microsoft.AspNetCore.Mvc;

namespace BooksApi;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BaseController : Controller
{
    
}