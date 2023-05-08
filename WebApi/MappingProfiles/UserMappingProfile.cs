using Angular.DBContext;
using AutoMapper;
using Angular.Models;

namespace Angular.MappingProfiles;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}