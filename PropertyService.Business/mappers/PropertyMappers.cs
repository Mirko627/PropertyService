using AutoMapper;
using PropertyService.Repository.Entities;
using PropertyService.Shared.dtos;
namespace PropertyService.Business.mappers
{
    public class PropertyMappers :Profile
    {
        public PropertyMappers() {
            CreateMap<CreatePropertyDto, Property>()
               .ForMember(dest => dest.Status, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdatePropertyDto, Property>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.AgencyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Property, PropertyDto>();

            CreateMap<ChangePropertyStatusDto, PropertyStatusHistory>();
        }
    }
}
