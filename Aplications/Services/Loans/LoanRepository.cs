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
using ServeBook_Backend.Aplications.Services.Mail;

namespace ServeBook_Backend.Aplications.Services{
    public class LoanRepository : ILoanRepository{
        private readonly ServeBooksContext _context;
        private readonly MailRepository _mailrepository;

        public LoanRepository(ServeBooksContext context, MailRepository mailRepository)
        {
            _context = context;
            _mailrepository = mailRepository;
        }

        public async Task<String> CreateLoan(LoanCreateDto loanDto)
        { 
            var loanData = new Loan{
                /* Asignar los valores de los demas campos */
                userId = loanDto.userId,
                bookId = loanDto.bookId,
                dateLoan = DateTime.Now,
                dateReturn = loanDto.dateReturn,
                status = "Wait"
            };

            /* Buscar el libro registrado en el prestamo. */
            var foundBook = await _context.Books.FirstOrDefaultAsync(b => b.id_book == loanData.bookId);
            /* Buscar el usuario registrado en el prestamo. */
            var foundUser = await _context.Users.FirstOrDefaultAsync(b => b.id_user == loanData.userId);
            /* Buscar el administrador de la página. */
            var foundAdmin = await _context.Users.FirstOrDefaultAsync(b => b.rol == "Admin");
            
            // Verificar si el libro está disponible
            if(foundBook == null)
            {
                // Libro no encontrado
                return $"El libro con id {loanData.bookId} no existe.";
            }
            else if (foundBook.status == "Delete")
            {
                // Libro encontrado pero está marcado como eliminado
                return $"El libro con id {loanData.bookId} no se encuentra disponible.";
            }

            // Convertir dateLoan a DateOnly para calcular los dias de prestamo
            var dateLoanDateOnly = DateOnly.FromDateTime(loanData.dateLoan);
            var loanDuration = loanData.dateReturn.DayNumber - dateLoanDateOnly.DayNumber;
            if(loanDuration > 20)
            {
                return $"El libro no puede ser prestado por más de 20 días. Registraste {loanDuration} días";
            }

            await _context.Loans.AddAsync(loanData);
            await _context.SaveChangesAsync();

            /* Enviar correo */
            var subject = "¡Nueva Solicitud de prestamo!";
            var mensajeAdmin = $"El usuario {foundUser.name}, ha pedido el prestamos del libro {foundBook.title}, Con fecha de entrega del {loanData.dateReturn}.\n Prestamo de {loanDuration} días. ¡Autoriza el prestamo!";
            _mailrepository.EmailSendLoan(foundAdmin.email, subject, mensajeAdmin, foundUser, foundBook, loanData, loanDuration);

            return "El registro del prestamo fue enviado, espera la respuesta.";
        }

        public IEnumerable<Loan> AvailableLoan()
        {
            return _context.Loans.Where(b => b.status == "Authorized").ToList();
        }
    }
}
