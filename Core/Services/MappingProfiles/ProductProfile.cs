using AutoMapper;
using Domain.Models.Products;
using Services.MappingProfiles;
using Shared.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.NewFolder
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(Dist => Dist.BrandName, option => option.MapFrom(src => src.Brand.Name))
                .ForMember(Dist => Dist.TypeName, option => option.MapFrom(src => src.Type.Name))
                .ForMember(Dist => Dist.PictureUrl, option => option.MapFrom<ProductResolver>());


            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
                

        }
    }
}
