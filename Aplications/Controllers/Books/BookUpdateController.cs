using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Controllers.Books
{
    [ApiController]
    [Authorize]
    [Route("api/book/update")]
    public class BookUpdateController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookUpdateController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPut("{id}")]
        public ActionResult Update(Book booksito)
        {
            if (booksito == null)
            {
                return BadRequest("Se esta creando el libro con datos nulos o erroneos");
            }

            try
            {
                _bookRepository.UpdateBook(booksito);
                return Ok(new { Correcto = $"El libro {booksito.title} se ah actualizado correctamente en la base de datos con estos datos: ", booksito });
            }
            catch (Exception e)
            {
                return BadRequest($"El curso {booksito.title} tiene datos incorrectos: {e.Message}");
            }
        }
    }
}