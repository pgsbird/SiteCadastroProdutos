using AutoMapper;
using SiteMercadoProdutos.Dtos;
using SiteMercadoProdutos.Models;

namespace SiteMercadoProdutos.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            //Source - Target
            CreateMap<Product,ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto,Product>();
            CreateMap<Product, ProductUpdateDto>();
        }
    }
}