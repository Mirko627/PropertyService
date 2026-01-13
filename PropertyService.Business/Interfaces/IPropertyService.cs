using PropertyService.Shared.dtos;

namespace PropertyService.Business.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetAllAsync();
        Task<PropertyDto> GetPropertyByIdAsync(int id);
        Task AddAsync(CreatePropertyDto property, int userId);
        Task UpdateAsync(int id, UpdatePropertyDto property, int userId);
        Task DeleteAsync(int id, int userId);
        Task ChangeStatusAsync(int id, ChangePropertyStatusDto dto, int userId);
    }
}
