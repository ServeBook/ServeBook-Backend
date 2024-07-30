using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Models;
using ServeBook_Backend.Dtos;

namespace ServeBook_Backend.Aplications.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllAvailable();
        IEnumerable<Book> GetAllBorrowed();
        Task CreateBook(BookCreateDto bookDto);
                public Book GetOne(int id);
        public void UpdateBook(Book booksito);
        IEnumerable<Book> GetAll();

        /* Obtener todas las solicitudes de préstamo pendientes */
        IEnumerable<Loan> GetPendingLoans();

        /* Aprobar una solicitud de préstamo */
        void ApproveLoan(int loanId, int bookId);
        public void InactiveBook(Book booksito);
        public void ActiveBook(Book booksito);
        public IEnumerable<Book> AvailableBook();
    }
}