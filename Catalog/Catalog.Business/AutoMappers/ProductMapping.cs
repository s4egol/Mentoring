using AutoMapper;
using Catalog.Business.Models;
using Catalog.Business.Models.Queries;
using Catalog.DataAccess.Models.Filters;
using ORM.Entities;

namespace Catalog.Business.AutoMappers
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductEntity>();
            CreateMap<ProductEntity, Product>();
            CreateMap<ProductEntity, ProductMessage>();
            CreateMap<ProductQueryEntity, ProductFilter>();
        }
    }
}
