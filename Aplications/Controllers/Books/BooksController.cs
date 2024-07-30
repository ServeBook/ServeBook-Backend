using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Aplications.Services.Token;
using ServeBook_Backend.Data;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            try
            {
                return Ok(_bookRepository.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error, libro no encontrado: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var booksito = _bookRepository.GetOne(id);
                if (booksito == null)
                {
                    return NotFound($"No se encontro el libro con el id {id}");
                }
                return Ok(booksito);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error, libro no se encontro: {e.Message}");
            }
        }
        
        /*******************************************************/
        /*Utilizamos el metodo GET para Traer regitros en la tabla Books por Status*/
        [HttpGet]
        [Route("/status/{status}")]
        public Book Details(string status)
        {
            return _bookRepository.GetByStatus(status);
        }
        /*******************************************************/


    }
}