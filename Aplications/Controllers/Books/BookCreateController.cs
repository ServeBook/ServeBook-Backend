using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Models;
using ServeBook_Backend.Data;
using ServeBook_Backend.Aplications.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace ServeBook_Backend.Controllers
{
    public class BookCreateController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookCreateController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("book/create")]
        public async Task<IActionResult> Create([FromBody] BookCreateDto bookDto)
        {
            try
            {
                await _bookRepository.CreateBook(bookDto);
                return Ok("The book was create correctly");
            }
            catch (System.Exception)
            {
                return BadRequest("The book could not be created");
            }
        }
    }
}