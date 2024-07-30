using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Models;
using ServeBook_Backend.Aplications.Services;
using ServeBook_Backend.Data;
using Microsoft.EntityFrameworkCore;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Dtos;

namespace ServeBook_Backend.Aplications.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ServeBooksContext _context;
        public BookRepository(ServeBooksContext context)
        {
            _context = context;
        }

        public async Task CreateBook(BookCreateDto bookDto)
        {
            var bookData = new Book
            {
                title = bookDto.title,
                author = bookDto.author,
                gender = bookDto.gender,
                datePublication = bookDto.datePublication,
                copiesAvailable = bookDto.copiesAvailable
            };

            /* Si la cantidad de copias del libro es mayor a 0 esta disponible */
            if(bookData.copiesAvailable > 0){
                bookData.status = "Available";
            }else if(bookData.copiesAvailable <= 0){
                /* Si la cantidad de copias del libro es menor o igual a 0 esta prestado */
                bookData.status = "Borrowed";
            }

            _context.Books.Add(bookData);
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
    }
}