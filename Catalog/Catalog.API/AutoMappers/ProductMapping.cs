using AutoMapper;
using Catalog.API.Models.Product;
using Catalog.API.Models.Product.Queries;
using Catalog.Business.Models;
using Catalog.Business.Models.Queries;

namespace Catalog.API.AutoMappers
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductViewModel, ProductEntity>();
            CreateMap<ProductEntity, ProductViewModel>();
            CreateMap<ProductContentViewModel, ProductEntity>();
            CreateMap<ProductQuery, ProductQueryEntity>();
        }
    }
}
