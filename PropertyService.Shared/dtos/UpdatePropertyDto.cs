namespace PropertyService.Shared.dtos
{
    public class UpdatePropertyDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public int SquareMetres { get; set; }
    }
}
