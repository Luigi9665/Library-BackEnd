using Library_BackEnd.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace Library_BackEnd.Models.Dto
{
    public class FormBookViewModel
    {

        public List<Category> Categories { get; set; } = new();
        //public Book BookToEdit { get; set; } = new();

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Il titolo del libro è obbligatorio")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "L'autore del libro è obbligatorio")]
        public string? Author { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public IFormFile? CoverImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key per la categoria
        [Required(ErrorMessage = "La categoria del libro è obbligatoria")]
        public Guid CategoryId { get; set; }

    }
}
