using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Interfaces
{
    public interface ILoanRepository
    {
        public IEnumerable<Loan> AvailableLoan();
    }
}