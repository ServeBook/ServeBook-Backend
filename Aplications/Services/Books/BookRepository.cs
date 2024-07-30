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
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        /*Crear registros en la tabla Books*/
        public async Task CreateBook(Book book){
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        /*=======================================================*/
        

        /*Todos los registros de la tabla Books*/
        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }
        /*=======================================================*/
        
        /*Todos los registros de la tabla Books con status AVAILABLE*/
        public IEnumerable<Book> GetAllAvailable()
        {
            return _context.Books.Where(s => s.status == "Available").ToList();
        }

        /*=======================================================*/

        /*Todos los registros de la tabla Books con status Borrowed*/
        public IEnumerable<Book> GetAllBorrowed(){
            return _context.Books.Where(s => s.status == "Borrowed").Include(c=>c.Loans).ToList();
        } 
        /*=======================================================*/


        /*Modificar un registro en la tabla Books*/
        public void UpdateBook(Book booksito)
        {
            _context.Books.Update(booksito);
            _context.SaveChanges();
        }
        /*=======================================================*/

        /* Obtener todas las solicitudes de préstamo pendientes */
        public IEnumerable<Loan> GetPendingLoans()
        {
            return _context.Loans
                .Where(r => r.status == "Wait")
                .Include(r => r.Book) // Incluye la propiedad Book para obtener los detalles del libro
                .Include(r => r.User) // Incluye la propiedad User para obtener los detalles del usuario
                .ToList();
        }
        /*=====================================================================================*/

        /* Aprobar una solicitud de préstamo */
        public void ApproveLoan(int loanId, int bookId)
        {
            var loan = _context.Loans.Find(loanId);
            var book = _context.Books.Find(bookId);
            if(loan.status == "Wait")
            {
                loan.status = "Authorized";
                _context.Loans.Attach(loan);
                _context.Entry(loan).Property(b=>b.status).IsModified = true;
                book.status = "Borrowed";
                _context.Books.Attach(book);
                _context.Entry(book).Property(b=>b.status).IsModified = true;
                _context.SaveChanges();
            }
        }
            
        /*=============================================================================================================*/



        /*Task IBookRepository.ApproveLoan(int loanId)
        {
            var loan = _context.Loans.Find(id);
            if (loan!= null && loan.status == "Wait")
            {
                loan.status = "Authorized";
                _context.SaveChanges();
            }
        }*/
    }   

}