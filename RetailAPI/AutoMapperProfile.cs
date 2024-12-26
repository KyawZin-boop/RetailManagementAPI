using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Model.Entities;
using Model.DTO;

namespace RetailAPI
{
    public class AutoMapperProfile : Profile
    {
            public AutoMapperProfile()
            {
                CreateMap<Product, ProductDTO>().ReverseMap();

            }
        
    }
}
