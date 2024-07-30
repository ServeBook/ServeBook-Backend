using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Interfaces
{
    public interface IBookRepository
    {
        Task CreateBook(Book book);
        public IEnumerable<Book> GetAll();
        Book GetByStatus(string status);
        public Book GetOne(int id);
        public void UpdateBook(Book booksito);
        public void InactiveBook(Book booksito);
        public void ActiveBook(Book booksito);
    }
}