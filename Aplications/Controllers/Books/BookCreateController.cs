using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Models;
using ServeBook_Backend.Data;
using ServeBook_Backend.Aplications.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ServeBook_Backend.Aplications.Interfaces;

namespace ServeBook_Backend.Controllers
{
    public class BookCreateController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookCreateController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        [Route("book/create")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            try
            {
                await _bookRepository.CreateBook(book);
                return Ok("The book was create correctly");
            }
            catch (System.Exception)
            {
                return BadRequest("The book could not be created");
            }

        }
    }
}