using Microsoft.AspNetCore.Mvc;
using ServeBook_Backend.Models;
using ServeBook_Backend.Dtos;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Dtos;

namespace ServerBook_Backend.Controllers{
    public class LoanCreateController : ControllerBase{
        private readonly ILoanRepository _LoanRepository;
        public LoanCreateController(ILoanRepository loanRepository){
            _LoanRepository = loanRepository;
        }

        [HttpPost]
        [Route("loan/create")]
        public async Task<IActionResult> Create([FromBody] LoanCreateDto loanDto){
            var result = await _LoanRepository.CreateLoan(loanDto);
            if(result == "El registro del prestamo fue enviado, espera la respuesta, te llegará al correo."){
                return Ok(new{Message = result});
            }
            return BadRequest(new {Message = result});
        }
    }
}