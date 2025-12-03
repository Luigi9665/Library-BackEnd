namespace Library_BackEnd.Models.Dto
{
    public class ProductViewModel
    {

        public Guid Id { get; set; }
        public string? Title { get; set; }
        public bool IsAvailable { get; set; }
        public string? CoverImageUrl { get; set; }

        public Guid CategoryId { get; set; }

        public string? NameCategory { get; set; }

    }
}
