using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServeBook_Backend.Aplications.Services.Token;

namespace ServeBook_Backend.Aplications.Controllers
{
    public class LoginController : Controller
    {
        /* private readonly ITokenServices _tokenServices;
        private readonly BaseContext _context;
        public LoginController(BaseContext context, ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromBody] User authResponse)
        {
            var user = _context.users.FirstOrDefault(u => u.email == authResponse.email);

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

                Response.Cookies.Append("Id", BCrypt.Net.BCrypt.HashPassword(usuario.Id.ToString()), cookieOptions);
                Response.Cookies.Append("Email", BCrypt.Net.BCrypt.HashPassword(usuario.Email), cookieOptions);
                Response.Cookies.Append("RoleId", BCrypt.Net.BCrypt.HashPassword(usuario.RoleId.ToString()), cookieOptions);

                return Ok(new { Token = token });
            }
            else
            {
                return NotFound("email or password are not correct.");
            }
        } */

    }
}