using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Models.Dto
{
    public class ManagementViewModel
    {

        public FormBookViewModel ToForm { get; set; } = new();

        public Book BookToEdit { get; set; } = new();

    }
}
