using System.ComponentModel.DataAnnotations;

namespace Library_BackEnd.Models.Entity
{
    public class RentRecord
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public DateTime? RentDate { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}
