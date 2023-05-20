using MyShop.WebApi.Data;
using MyShop.WebApi.Dtos;

namespace MyShop.WebApi.MappingProfiles;
public class CustomerMappingProfile : GenericMappingProfile<Customer, CustomerDto>
{
    public CustomerMappingProfile() : base()
    {
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                Address1 = src.Address,
                City = src.City,
                State = src.State,
                PostalCode = src.PostalCode,
                Country = src.Country
            }))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => new PhoneNumber
            {
                Cell = src.Cell,
                Phone = src.Phone
            }))
            .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => new ApplicationUser
            {
                UserName = src.Username,
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth
            })).ReverseMap();
    }
}