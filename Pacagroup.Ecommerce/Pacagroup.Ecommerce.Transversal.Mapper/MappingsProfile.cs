using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            //CreateMap<Customers, CustomersDto>().ReverseMap()
            //    .ForMember(destination => destination.CustomerId, source => source.MapFrom(src => src.CustomerId));
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
