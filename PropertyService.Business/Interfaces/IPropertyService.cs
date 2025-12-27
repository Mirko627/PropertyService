using PropertyService.Shared.dtos;

namespace PropertyService.Business.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetAllAsync();
        Task<PropertyDto> GetPropertyByIdAsync(int id);
        Task AddAsync(CreatePropertyDto property);
        Task UpdateAsync(int id, UpdatePropertyDto property);
        Task DeleteAsync(int id);
        Task ChangeStatusAsync(int id, ChangePropertyStatusDto dto);
    }
}
