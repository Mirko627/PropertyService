using PropertyService.Shared.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Repository.Entities
{
    public class Property
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public decimal Price { get; set; }
        public int SquareMetres { get; set; }
        public PropertyStatus Status { get; set; } = PropertyStatus.Available;
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public int AgencyId { get; set; }
    }
}
