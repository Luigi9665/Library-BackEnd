namespace Library_BackEnd.Models.Dto
{
    public class ToFIlterModal
    {

        public bool? IsAvailable { get; set; }

        public List<Guid> CategoryIds { get; set; } = new();

    }
}
