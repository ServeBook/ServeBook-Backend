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

        public async Task CreateBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Book GetOne(int id)
        {
            return _context.Books.Find(id);
        }

        public void UpdateBook(Book booksito)
        {
            _context.Books.Update(booksito);
            _context.SaveChanges();
        }
        public void InactiveBook(Book booksito)
        {
            booksito.status = "Delete";
            _context.Books.Update(booksito);
            _context.SaveChanges();
        }

        public void ActiveBook(Book booksito)
        {
            booksito.status = "Available";
            _context.Books.Update(booksito);
            _context.SaveChanges();
        }

        public IEnumerable<Book> AvailableBook()
        {
            return _context.Books.Where(b => b.status == "Available").ToList();
        }
    }
}