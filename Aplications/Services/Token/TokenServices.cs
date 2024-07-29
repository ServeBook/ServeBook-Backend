using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ServeBook_Backend.Aplications.Services.Token
{
    /* public class TokenServices : ITokenServices
    {
        private readonly string Key;   
        private readonly string Issuer;
        private readonly string Audience;
        public TokenServices(IConfiguration configuration)
        {
            Key = configuration["Jwt:Key"];
            Issuer = configuration["Jwt:Issuer"];
            Audience = configuration["Jwt:Audience"];
        }
        public string GetToken(User authResponse)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            
                var claims = new List<Claim>
                {
                    /* new Claim(JwtRegisteredClaimNames.Email, authResponse.email) */
                /* };

                var tokenOptions = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = Issuer,
                    Audience = Audience,
                    SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                    
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenOptions);
                var tokenString = tokenHandler.WriteToken(securityToken);

                return tokenString;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
            
        } 
    } */
}