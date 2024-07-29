using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServeBook_Backend.Aplications.Services.Token;
using ServeBook_Backend.Data;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Controllers
{
    public class LoginController : Controller
    {
        private readonly ITokenServices _tokenServices;
        private readonly ServeBooksContext _context;
        public LoginController(ServeBooksContext context, ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
            _context = context;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User authResponse)
        {
            var user = _context.Users.FirstOrDefault(u => u.email == authResponse.email);

            if(user != null && BCrypt.Net.BCrypt.Verify(authResponse.password, user.password))
            {
                string token = _tokenServices.GetToken(authResponse);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // La cookie no será accesible a través de JavaScript
                    Secure = true, // La cookie solo se enviará a través de conexiones HTTPS
                    Expires = DateTime.UtcNow.AddHours(1) // Establecer la expiración de la cookie
                };

                Response.Cookies.Append("Id", BCrypt.Net.BCrypt.HashPassword(user.id_user.ToString()), cookieOptions);
                Response.Cookies.Append("Email", BCrypt.Net.BCrypt.HashPassword(user.email), cookieOptions);
                Response.Cookies.Append("Role", BCrypt.Net.BCrypt.HashPassword(user.rol.ToString()), cookieOptions);

                return Ok(new { Token = token });
            }
            else
            {
                return NotFound("email or password are not correct.");
            }
        }

    }
}