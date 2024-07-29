using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Models;
using ServeBook_Backend.Aplications.Services;
using ServeBook_Backend.Data;
using Microsoft.EntityFrameworkCore;
using ServeBook_Backend.Aplications.Interfaces;

namespace ServeBook_Backend.Aplications.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ServeBooksContext _context;
        public BookRepository(ServeBooksContext context)
        {
            _context = context;
        }

        public async Task CreateBook(Book book){
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

    }
}