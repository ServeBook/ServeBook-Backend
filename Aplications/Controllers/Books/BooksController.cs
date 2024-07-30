using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Aplications.Services.Token;
using ServeBook_Backend.Data;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Controllers
{
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [Authorize]
        [HttpGet]
        [Route("api/books")]
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
        
        [Authorize]
        [HttpGet]
        [Route("api/{id}/books")]
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


        /*Metodo GET para Traer registros en la tabla Books con Status BORROWED*/
        [Authorize]
        [HttpGet]
        [Route("/borrowed")]
        public IEnumerable<Book> GetBooksBorrowed(){
            return _bookRepository.GetAllBorrowed();
        }
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/

        /*Metodo GET para Traer registro en la tabla Books con estatus AVAILABLE*/
        [Authorize]
        [HttpGet]
        [Route("/available")]
        public IEnumerable<Book> GetAllAvailable(){
            return _bookRepository.GetAllAvailable();
        }
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        

         /* Obtener todas las solicitudes de préstamo pendientes */
        [Authorize]
        [HttpGet("loans/pending")]
        public IEnumerable<Loan> GetPendingLoans()
        {
            return _bookRepository.GetPendingLoans();
        }

        /* Aprobar una solicitud de préstamo */
        [Authorize]
        [HttpPost("loans/approve/{id}")]
        public IActionResult ApproveLoan(int leanId,int bookId)
        {
            _bookRepository.ApproveLoan(leanId,bookId);
            return Ok();
        }

    }
}
