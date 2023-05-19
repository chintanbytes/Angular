using MyShop.WebApi.Dtos;
using AutoMapper;
using System.Linq.Expressions;

namespace MyShop.WebApi.MappingProfiles;
public class GenericMappingProfile<T, D> : Profile where T : class where D : BaseDto
{
    public GenericMappingProfile()
    {
        CreateMap<T, D>().ReverseMap();
    }
}

public static class MappingProfileExtensions
{
    public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map,
        Expression<Func<TDestination, object>> selector)
    {
        map.ForMember(selector, config => config.Ignore());
        return map;
    }
}