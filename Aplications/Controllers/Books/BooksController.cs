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
    public class BooksController : Controller
    {   
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        [HttpGet]
        [Route("api/Books")]

        public IEnumerable  <Book> GetBooks(){
            return _bookRepository.GetAll();
        }
        /*******************************************************/
        /*Utilizamos el metodo GET para Traer regitros en la tabla Books por Status*/
        [HttpGet]
        [Route("api/Books/{status}")]
        public Book Details(string status) {
            return _bookRepository.GetByStatus(status);
        }
        /*******************************************************/
}
}