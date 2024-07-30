using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServeBook_Backend.Aplications.Controllers.Excel
{
    public class ExportAdminController : Controller
    {
        private readonly ILogger<ExportAdminController> _logger;

        public ExportAdminController(ILogger<ExportAdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}