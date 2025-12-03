using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Models.Dto
{
    public class FormBookViewModel
    {

        public List<Category> Categories { get; set; } = new();
        public Book BookToEdit { get; set; } = new();
    }
}
