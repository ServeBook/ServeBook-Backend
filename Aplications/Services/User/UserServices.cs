using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServeBook_Backend.Aplications.Interfaces;
using ServeBook_Backend.Data;
using ServeBook_Backend.Models;


namespace ServeBook_Backend.Aplications.Services
{
    public class UserServices : IUserServices
    {
        private readonly ServeBooksContext _context;
        public UserServices(ServeBooksContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.email == user.email);
            if (existingUser != null)
            {
                throw new Exception("El usuario con ese email ya existe.");
            }

            user.rol = "User";
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}