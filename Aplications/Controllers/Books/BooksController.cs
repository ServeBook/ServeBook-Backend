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
    public class BooksController : Controller
    {   
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /*Utilizamos el metodo GET para Traer TODOS registros en la tabla Books*/
        [HttpGet]   
        [Route("api/books")]
        public IEnumerable <Book> GetBooks(){
            return _bookRepository.GetAll();
        }
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/

        /*Metodo GET para Traer registros en la tabla Books con Status BORROWED*/
        [HttpGet]
        [Route("api/books/borrowed")]
        public IEnumerable<Book> GetBooksBorrowed(){
            return _bookRepository.GetAllBorrowed();
        }
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/

        /*Metodo GET para Traer registro en la tabla Books con estatus AVAILABLE*/
        [HttpGet]
        [Route("api/books/available")]
        public IEnumerable<Book> GetAllAvailable(){
            return _bookRepository.GetAllAvailable();
        }
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        

         /* Obtener todas las solicitudes de préstamo pendientes */
        [HttpGet("loans/pending")]
        public IEnumerable<Loan> GetPendingLoans()
        {
            return _bookRepository.GetPendingLoans();
        }

        /* Aprobar una solicitud de préstamo */
        [HttpPost("loans/approve/{id}")]
        public IActionResult ApproveLoan(int leanId,int bookId)
        {
            _bookRepository.ApproveLoan(leanId,bookId);
            return Ok();
        }


    }
}
