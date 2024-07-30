using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using ServeBook_Backend.Data;

namespace ServeBook_Backend.Aplications.Controllers
{
    public class ExportUserController : Controller
    {
        private readonly ServeBooksContext _context;

        public ExportUserController(ServeBooksContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("export/loan")]
        public async Task<IActionResult> ExportToExcel()
        {
            // Obtener el ID del usuario del claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId;
            if (!int.TryParse(userIdClaim, out userId))
            {
                return BadRequest("Invalid user ID.");
            }

            // Consultar los préstamos del usuario
            var loans = await _context.Loans
                .Where(loan => loan.userId == userId)
                .Include(loan => loan.Books) // Asumiendo que tienes una relación entre Loan y Book configurada en el modelo
                .ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Loans");
                 worksheet.Cells["A1"].Value = "ID Loan";
                worksheet.Cells["B1"].Value = "User ID";
                worksheet.Cells["C1"].Value = "Book ID";
                worksheet.Cells["D1"].Value = "Book Title";
                worksheet.Cells["E1"].Value = "Book Author";
                worksheet.Cells["F1"].Value = "Date Loan";
                worksheet.Cells["G1"].Value = "Date Return";
                worksheet.Cells["H1"].Value = "Status";


                var row = 2;
                foreach (var loan in loans)
                {
                   worksheet.Cells[row, 1].Value = loan.id_loan;
                    worksheet.Cells[row, 2].Value = loan.userId;
                    worksheet.Cells[row, 3].Value = loan.Books;
                    worksheet.Cells[row, 4].Value = loan.Books?.title; // Asumiendo que tienes una relación entre Loan y Book configurada en el modelo
                    worksheet.Cells[row, 5].Value = loan.Books?.author; // Asumiendo que tienes una relación entre Loan y Book configurada en el modelo
                    worksheet.Cells[row, 6].Value = loan.dateLoan.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 7].Value = loan.dateReturn.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 8].Value = loan.status;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "loans.xlsx");
            }
        }
    }
}