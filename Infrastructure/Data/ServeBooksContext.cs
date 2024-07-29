using Microsoft.EntityFrameworkCore;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Data{
    public class ServeBooksContext : DbContext{
        public ServeBooksContext(DbContextOptions<ServeBooksContext> options) : base(options){}
        public DbSet<User> Users {get; set;}
        public DbSet<Book> Books {get; set;}
        public DbSet<Loan> Loans {get; set;}
    }
}