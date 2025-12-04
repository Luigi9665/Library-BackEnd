using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Models.Dto
{
    public class BookDetailsViewModel
    {

        public Book? Book { get; set; }
        public List<RentRecord>? RentRecords { get; set; }

    }
}
