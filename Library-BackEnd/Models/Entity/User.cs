using System.ComponentModel.DataAnnotations;

namespace Library_BackEnd.Models.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Il nome utente è obbligatorio")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "L'email è obbligatoria")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La password è obbligatoria")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public ICollection<Book> BorrowedBooks { get; set; }

    }
}
