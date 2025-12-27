using PropertyService.Shared.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Repository.Entities
{
    public class PropertyStatusHistory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public required Property Property { get; set; }
        public PropertyStatus OldStatus { get; set; } = PropertyStatus.Available;
        public PropertyStatus NewStatus { get; set; } = PropertyStatus.Available;
        public DateTime ChangedAt { get; set; }
    }
}
