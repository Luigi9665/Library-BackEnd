using Library_BackEnd.Models.Entity;

namespace Library_BackEnd.Models.Dto
{
    public class CategoryViewModal
    {

        public List<Category> Categories { get; set; } = new();

        public Category CategoryToEdit { get; set; } = new();

    }
}
