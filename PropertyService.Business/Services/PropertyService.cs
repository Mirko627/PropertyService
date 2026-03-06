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
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyStatusHistoryRepository _propertyStatusHistoryRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository, IPropertyStatusHistoryRepository propertyStatusHistoryRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _propertyStatusHistoryRepository = propertyStatusHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyDto>> GetAllAsync()
        {
            List<Property> properties = await _propertyRepository.GetAllAsync();
            return _mapper.Map<List<PropertyDto>>(properties);
        }

        public async Task<PropertyDto> GetPropertyByIdAsync(int id)
        {
            Property property = await _propertyRepository.GetPropertyById(id) ?? throw new KeyNotFoundException($"Proprietà con ID {id} non trovata.");

            return _mapper.Map<PropertyDto>(property);
        }

        public async Task AddAsync(CreatePropertyDto dto, int userId)
        {
            Property p = _mapper.Map<Property>(dto);

            p.OwnerId = userId;
            p.Status = PropertyStatus.Available;
            p.CreatedAt = DateTime.UtcNow;

            await _propertyRepository.AddAsync(p);
        }

        public async Task UpdateAsync(int id, UpdatePropertyDto dto, int userId)
        {
            Property property = await _propertyRepository.GetPropertyById(id) ?? throw new KeyNotFoundException($"Proprietà con ID {id} non trovata.");

            if (property.OwnerId != userId) throw new UnauthorizedAccessException("Non hai i permessi per modificare questa proprietà.");

            _mapper.Map(dto, property);

            await _propertyRepository.UpdateAsync(property);
        }

        public async Task DeleteAsync(int id, int userId)
        {
            Property property = await _propertyRepository.GetPropertyById(id) ?? throw new KeyNotFoundException($"Proprietà con ID {id} non trovata.");

            if (property.OwnerId != userId) throw new UnauthorizedAccessException("Non hai i permessi per modificare questa proprietà.");

            await _propertyRepository.DeleteAsync(id);
        }

        public async Task ChangeStatusAsync(int id, ChangePropertyStatusDto dto, int userId)
        {
            Property property = await _propertyRepository.GetPropertyById(id) ?? throw new KeyNotFoundException($"Proprietà con ID {id} non trovata.");

            if (property.OwnerId != userId) throw new UnauthorizedAccessException("Non hai i permessi per modificare questa proprietà.");

            if (dto.NewStatus == property.Status)
                return;

            var statusHistory = new PropertyStatusHistory
            {
                Property = property,
                OldStatus = property.Status,
                NewStatus = dto.NewStatus,
                ChangedAt = DateTime.UtcNow
            };

            property.Status = dto.NewStatus;

            await _propertyRepository.UpdateAsync(property);
            await _propertyStatusHistoryRepository.AddAsync(statusHistory);
        }
    }
}