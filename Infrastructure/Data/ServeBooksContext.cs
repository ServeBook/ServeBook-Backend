using Microsoft.EntityFrameworkCore;
using ServeBook_Backend.Models;

namespace ServeBook_Backend.Data{
    public class ServeBooksContext : DbContext{
        public ServeBooksContext(DbContextOptions<ServeBooksContext> options) : base(options){}
        public DbSet<User> Users {get; set;}
        public DbSet<Book> Books {get; set;}
        public DbSet<Loan> Loans {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Users)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.userId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Books)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.bookId);

        base.OnModelCreating(modelBuilder);
    }
    }
}