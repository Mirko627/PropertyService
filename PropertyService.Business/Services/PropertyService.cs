using AutoMapper;
using PropertyService.Business.Interfaces;
using PropertyService.Repository.Entities;
using PropertyService.Repository.Interfaces;
using PropertyService.Shared.dtos;
using PropertyService.Shared.enums;

namespace PropertyService.Business.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IPropertyStatusHistoryRepository propertyStatusHistoryRepository;
        private readonly IMapper mapper;

        public PropertyService(IPropertyRepository propertyRepository, IPropertyStatusHistoryRepository propertyStatusHistoryRepository, IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.propertyStatusHistoryRepository = propertyStatusHistoryRepository;
            this.mapper = mapper;
        }

        public async Task<List<PropertyDto>> GetAllAsync()
        {
            List<Property> properties = await propertyRepository.GetAllAsync();
            List<PropertyDto> propertyDtos = mapper.Map<List<PropertyDto>>(properties);
            return propertyDtos;
        }

        public async Task<PropertyDto> GetPropertyByIdAsync(int id)
        {
            Property property = await propertyRepository.GetPropertyById(id);
            PropertyDto propertyDto = mapper.Map<PropertyDto>(property);
            return propertyDto;
        }
        public async Task AddAsync(CreatePropertyDto property)
        {
            Property p = mapper.Map<Property>(property);
            p.Status = PropertyStatus.Available;
            p.CreatedAt = DateTime.Now;
            await propertyRepository.AddAsync(p);
            return;
        }

        public async Task DeleteAsync(int id)
        {
           await propertyRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, UpdatePropertyDto property)
        {
            Property p = await propertyRepository.GetPropertyById(id);
            mapper.Map(property, p);
            await propertyRepository.UpdateAsync(p);
        }
        public async Task ChangeStatusAsync(int id, ChangePropertyStatusDto dto)
        {
            Property p = await propertyRepository.GetPropertyById(id);
            if (dto.NewStatus == p.Status)
                return;
            PropertyStatusHistory statusHistory = new PropertyStatusHistory
            {
                Property = p,
                OldStatus = p.Status,
                NewStatus = dto.NewStatus,
                ChangedAt = DateTime.Now
            };
            p.Status = dto.NewStatus;
            await propertyRepository.UpdateAsync(p);
            await propertyStatusHistoryRepository.AddAsync(statusHistory);
        }
    }
}
