using System;
using Microsoft.EntityFrameworkCore;

namespace bookshelf_api.Models
{
    public class BookshelfContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=bookshelf.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Books)
            .WithOne(b => b.LoanedTo);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "channa@gmail.com",
                    Name = "Channa"
                },
                new User
                {
                    UserId = 2,
                    UserName = "lasitha@gmail.com",
                    Name = "Lasith"
                },
                new User
                {
                    UserId = 3,
                    UserName = "dilshan@gmail.com",
                    Name = "Dilshan"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    ISBN = "978-0262033848",
                    Author = "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, Clifford Stein",
                    Title = "Introduction to Algorithms"
                },
                new Book
                {
                    BookId = 2,
                    ISBN = "9780141439518",
                    Author = "Jane Austen",
                    Title = "Pride and Prejudice"
                },
                new Book
                {
                    BookId = 3,
                    ISBN = "978-1598955094",
                    Author = "Maxim Gorky",
                    Title = "The Mother v2"
                },
                new Book
                {
                    BookId = 4,
                    ISBN = "978-1544955094",
                    Author = "Maxim Gorky",
                    Title = "The Mother"
                }
            );
        }
    }
}
