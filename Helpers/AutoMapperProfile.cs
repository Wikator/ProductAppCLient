using AutoMapper;
using ProductsApp.Models.DTOs;

namespace ProductsApp.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductDto, ProductUpsertDto>();
    }
}