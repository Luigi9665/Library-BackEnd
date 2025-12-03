using Microsoft.EntityFrameworkCore;

namespace Library_BackEnd.Models.Entity
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Inizializzo il DbSet per la mia entità Book
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RentRecord> RentRecords { get; set; }

    }
}
