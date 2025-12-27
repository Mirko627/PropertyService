using Microsoft.EntityFrameworkCore;
using PropertyService.Data.Context;
using PropertyService.Repository.Entities;
using PropertyService.Repository.Interfaces;

namespace PropertyService.Data.Repositories
{
    public class PropertyStatusHistoryRepository : IPropertyStatusHistoryRepository
    {
        private readonly PropertyDbContext context;

        public PropertyStatusHistoryRepository(PropertyDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(PropertyStatusHistory property)
        {
            await context.propertiesStatusHistory.AddAsync(property);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            PropertyStatusHistory? p = await context.propertiesStatusHistory.FindAsync(id);
            if (p == null)
                throw new Exception("Property non esistente");
            context.propertiesStatusHistory.Remove(p);
            await context.SaveChangesAsync();
        }

        public async Task<List<PropertyStatusHistory>> GetAllAsync()
        {
            return await context.propertiesStatusHistory.ToListAsync();
        }

        public async Task<PropertyStatusHistory?> GetPropertyStatusHistoryById(int id)
        {
            return await context.propertiesStatusHistory.FindAsync(id);
        }

        public async Task UpdateAsync(PropertyStatusHistory property)
        {
            context.propertiesStatusHistory.Update(property);
            await context.SaveChangesAsync();
        }
    }
}
