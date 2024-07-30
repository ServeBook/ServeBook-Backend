using ServeBook_Backend.Models;
using ServeBook_Backend.Dtos;

namespace ServeBook_Backend.Aplications.Interfaces
{
    public interface ILoanRepository
    {
        public IEnumerable<Loan> AvailableLoan();
        Task<String> CreateLoan(LoanCreateDto loanDto);
    }
}