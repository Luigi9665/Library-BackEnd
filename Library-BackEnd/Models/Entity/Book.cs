using System.ComponentModel.DataAnnotations;

namespace Library_BackEnd.Models.Entity
{
    public class Book
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Il titolo del libro è obbligatorio")]
        public string Title { get; set; }

        [Required(ErrorMessage = "L'autore del libro è obbligatorio")]
        public string Author { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Url(ErrorMessage = "Inserisci un URL valido per l'immagine")]
        public string? CoverImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key per la categoria
        [Required(ErrorMessage = "La categoria del libro è obbligatoria")]
        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }


        public ICollection<RentRecord>? RentRecords { get; set; }
    }
}
