using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Models.Dto
{
    public class FormBookViewModel
    {

        public List<ProductViewModel> Books { get; set; } = new();

        public List<Category> Categories { get; set; } = new();

    }
}
