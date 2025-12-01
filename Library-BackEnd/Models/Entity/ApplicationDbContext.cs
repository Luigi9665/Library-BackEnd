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

    }
}
