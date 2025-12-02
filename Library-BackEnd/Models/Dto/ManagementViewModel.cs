using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Models.Dto
{
    public class ManagementViewModel
    {

        public List<ProductViewModel> Books { get; set; } = new();

        public Book BookToEdit { get; set; } = new();

    }
}
