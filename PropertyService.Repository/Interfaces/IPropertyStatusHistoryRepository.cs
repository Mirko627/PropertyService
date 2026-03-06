using PropertyService.Repository.Entities;

namespace PropertyService.Repository.Interfaces
{
    public interface IPropertyStatusHistoryRepository
    {
        Task<List<PropertyStatusHistory>> GetAllAsync();
        Task<PropertyStatusHistory?> GetPropertyStatusHistoryById(int id);
        Task AddAsync(PropertyStatusHistory propertyStatusHistory);
        Task UpdateAsync(PropertyStatusHistory propertyStatusHistory);
        Task DeleteAsync(int id);
    }
}
