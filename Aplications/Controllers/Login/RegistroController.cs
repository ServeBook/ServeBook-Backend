using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Models;
using ServeBook_Backend.Aplications.Services.Mail;

namespace ServeBook_Backend.Aplications.Controllers
{
    public class RegistroController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly MailRepository _mailrepository;
        public RegistroController(IUserServices userServices, MailRepository mailRepository)
        {
            _userServices = userServices;
            _mailrepository = mailRepository;
        }

        [HttpPost]
        [Route("User/Create")]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                _userServices.Add(user);
                /* Enviar correo */
                var subject = "¡Te registraste en Serve Books!";
                var mensajeUser = $"{user.name}, ahora eres uno de nuestros usuarios Serve Books";
                _mailrepository.EmailSignUp(user.email, subject, mensajeUser, user);
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}