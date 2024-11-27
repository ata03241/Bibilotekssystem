using Microsoft.EntityFrameworkCore;
using Bibliotekssystem.Models;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseSqlServer("Server=ASHOKTAMANG;Database=Bibilotekssystem;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookID, ba.AuthorId }); 

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(ba => ba.BookAuthors)
            .HasForeignKey(ba => ba.BookID);
        
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(ba => ba.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);
        
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(l => l.Loans)
            .HasForeignKey(l => l.BookId);

    }
}