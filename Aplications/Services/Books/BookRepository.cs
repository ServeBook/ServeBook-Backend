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
        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        /*Crear registros en la tabla Books*/

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
        }        /*=======================================================*/

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




    }   
}