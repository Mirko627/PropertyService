namespace PropertyService.Shared.dtos
{
    public class CreatePropertyDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public decimal Price { get; set; }
        public int SquareMetres { get; set; }
    }
}
