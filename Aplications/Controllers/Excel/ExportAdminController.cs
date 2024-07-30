using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using ServeBook_Backend.Data;

namespace ServeBook_Backend.Aplications.Controllers.Excel
{
    public class ExportAdminController : Controller
    {
        private readonly ServeBooksContext _context;
        public ExportAdminController(ServeBooksContext context)
        {
            _context = context;
        }

        [Authorize/* (Policy = "AdminEmailPolicy") */]
        [HttpGet]
        [Route("export/register")]
        public async Task<IActionResult> ExportToExcel()
        {

            var users = await _context.Users.ToListAsync();
            var books = await _context.Books.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Users");
                worksheet.Cells["A1"].Value = "ID User";
                worksheet.Cells["B1"].Value = "Name User";
                worksheet.Cells["C1"].Value = "Email User";
                worksheet.Cells["D1"].Value = "Rol User";

                var row = 2;
                foreach (var user in users)
                {
                   worksheet.Cells[row, 1].Value = user.id_user;
                    worksheet.Cells[row, 2].Value = user.name;
                    worksheet.Cells[row, 3].Value = user.email;
                    worksheet.Cells[row, 4].Value = user.rol;
                    row++;
                }

                var worksheet2 = package.Workbook.Worksheets.Add("Books");
                worksheet2.Cells["A1"].Value = "ID Book";
                worksheet2.Cells["B1"].Value = "Title Book";
                worksheet2.Cells["C1"].Value = "author Book";
                worksheet2.Cells["D1"].Value = "Gender Book";
                worksheet2.Cells["E1"].Value = "Date Publication Book";
                worksheet2.Cells["F1"].Value = "Copies Available Book";
                worksheet2.Cells["G1"].Value = "status Book";

                row = 2;
                foreach (var book in books)
                {
                    worksheet2.Cells[row, 1].Value = book.id_book;
                    worksheet2.Cells[row, 2].Value = book.title;
                    worksheet2.Cells[row, 3].Value = book.author;
                    worksheet2.Cells[row, 4].Value = book.gender;
                    worksheet2.Cells[row, 5].Value = book.datePublication;
                    worksheet2.Cells[row, 6].Value = book.copiesAvailable;
                    worksheet2.Cells[row, 7].Value = book.status;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Register.xlsx");
            }
        }

    }
}