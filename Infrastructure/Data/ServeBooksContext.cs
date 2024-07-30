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
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre Loan y Book
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.bookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar la relación entre Loan y User
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.userId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}   