using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllAvailable();
        IEnumerable<Book> GetAllBorrowed();
        Task CreateBook(Book book);
        
        public void UpdateBook(Book booksito);
        IEnumerable<Book> GetAll();

        /* Obtener todas las solicitudes de préstamo pendientes */
        IEnumerable<Loan> GetPendingLoans();

        /* Aprobar una solicitud de préstamo */
        void ApproveLoan(int loanId, int bookId);
        
    }
}