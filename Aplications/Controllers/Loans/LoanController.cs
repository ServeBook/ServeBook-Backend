using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Controllers
{
    
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        public LoanController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Loan")]
        public ActionResult<IEnumerable<Loan>> GetLoans()
        {
            try
            {
                return Ok(_loanRepository.AvailableLoan());
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error, no hay prestamos activos actualmente");
            }
        }
    }
}