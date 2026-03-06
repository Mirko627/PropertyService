using PropertyService.Shared.enums;

namespace PropertyService.Shared.dtos
{
    public class ChangePropertyStatusDto
    {
        public PropertyStatus NewStatus { get; set; }
    }
}
