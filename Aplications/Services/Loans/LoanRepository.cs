using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Data;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Services
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ServeBooksContext _context;
        public LoanRepository(ServeBooksContext context)
        {
            _context = context;
        }

        public IEnumerable<Loan> AvailableLoan()
        {
            return _context.Loans.Where(b => b.status == "Authorized").ToList();
        }
    }
}