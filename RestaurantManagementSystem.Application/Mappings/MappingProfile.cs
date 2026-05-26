using AutoMapper;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Domain.Entities;
namespace RestaurantManagementSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();

        CreateMap<MenuItem, MenuItemDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));
    }
}