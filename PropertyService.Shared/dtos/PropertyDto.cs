using PropertyService.Shared.enums;

namespace PropertyService.Shared.dtos
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public decimal Price { get; set; }
        public int SquareMetres { get; set; }
        public PropertyStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public int AgencyId { get; set; }
    }
}
