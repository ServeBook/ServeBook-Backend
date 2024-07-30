using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Controllers
{
    public class RegistroController : Controller
    {
        private readonly IUserServices _userServices;
        public RegistroController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("User/Create")]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                _userServices.Add(user);
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}