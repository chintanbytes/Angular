using Angular.DBContext;
using AutoMapper;
using Angular.Models;


namespace Angular.MappingProfiles;
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}