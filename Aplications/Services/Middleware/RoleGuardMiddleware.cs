using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ServeBook_Backend.Data;

namespace ServeBook_Backend.Aplications.Services.Middleware
{
    public class RoleGuardMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleGuardMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ServeBooksContext dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
                if (userIdClaim != null)
                {
                    var userId = int.Parse(userIdClaim.Value);
                    var user = dbContext.Users.FirstOrDefault(u => u.id_user == userId);
                    if (user != null)
                    {
                        var claimsIdentity = (ClaimsIdentity)context.User.Identity;
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.rol));
                    }
                }
            }

            await _next(context);
        }
    }
}