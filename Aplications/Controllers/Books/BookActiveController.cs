  using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Controllers.Books
{
  [ApiController]
  [Route("api/book/active")]
  public class BookActiveController : ControllerBase
  {
    private readonly IBookRepository _bookRepository;
    public BookActiveController(IBookRepository bookRepository)
    {
      _bookRepository = bookRepository;
    }

    [HttpPut("{id}")]
    public IActionResult ActiveBook(int id)
    {
      try
      {
        var booksito = _bookRepository.GetOne(id);
        if (booksito.status == "Active")
        {
          return Ok($"El libro {booksito.title} ya se encuentra activo");
        }
        else
        {
          _bookRepository.ActiveBook(booksito);
          return Ok($"El libro {booksito.title} ha cambiado de estado a activo");
        }
      }
      catch (Exception e)
      {
        return StatusCode(500, $"Error al cambiar el estado: {e.Message}");
      }
    }
  }
}
