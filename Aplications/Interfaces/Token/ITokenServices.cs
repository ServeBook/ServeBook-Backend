using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Aplications.Services.Token
{
    public interface ITokenServices
    {
        string GetToken(User authResponse);
    }
}