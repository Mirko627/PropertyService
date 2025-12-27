using PropertyService.Shared.dtos;

namespace PropertyService.ClientHttp.Interfaces
{
    public interface IPropertyClient
    {
        public Task<List<PropertyDto>> GetAllAsync();
        public Task<PropertyDto?> GetByIdAsync(int id);
        Task AddAsync(CreatePropertyDto property);
        Task UpdateAsync(int id, UpdatePropertyDto property);
        public Task DeleteAsync(int id);
    }
}
