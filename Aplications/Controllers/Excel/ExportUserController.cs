using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServeBook_Backend.Aplications.Controllers.Excel
{
    public class ExportUserController : Controller
    {
        private readonly ILogger<ExportUserController> _logger;

        public ExportUserController(ILogger<ExportUserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}