using System.ComponentModel.DataAnnotations;

namespace Library_BackEnd.Models.Entity
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria è obbligatorio")]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
