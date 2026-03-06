using Microsoft.EntityFrameworkCore;
using PropertyService.Data.Context;
using PropertyService.Repository.Entities;
using PropertyService.Repository.Interfaces;

namespace PropertyService.Data.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyDbContext context;

        public PropertyRepository(PropertyDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Property property)
        {
            await context.properties.AddAsync(property);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Property? p = await context.properties.FindAsync(id);
            if (p == null)
                throw new Exception("Property non esistente");
            context.properties.Remove(p);
            await context.SaveChangesAsync();
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await context.properties.ToListAsync();      
        }

        public async Task<Property?> GetPropertyById(int id)
        {
            return await context.properties.FindAsync(id);
        }

        public async Task UpdateAsync(Property property)
        {
            context.properties.Update(property);
            await context.SaveChangesAsync();
        }
    }
}
