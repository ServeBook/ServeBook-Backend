using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Aplications.Interfaces;

namespace ServeBook_Backend.Controllers.Books
{
    [ApiController]
    [Route("api/book/inactive")]
    public class BookInactiveController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookInactiveController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPut("{id}")]
        public IActionResult InactiveBook(int id)
        {
            try
            {
                var booksito = _bookRepository.GetOne(id);
                if (booksito.status == "Delete")
                {
                    return Ok($"El libro {booksito.title} ya se encuentra inactivo");
                }
                else
                {
                    _bookRepository.InactiveBook(booksito);
                    return Ok($"el libro {booksito.title} ha cambiado de estado a delete");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al cambiar el estado del libro: {e.Message}");
            }
        }
    }
}