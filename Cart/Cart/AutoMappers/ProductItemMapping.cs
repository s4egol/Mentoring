using AutoMapper;
using Cart.Business.Models;
using Cart.Models;

namespace Cart.AutoMappers
{
    public class ProductItemMapping : Profile
    {
        public ProductItemMapping()
        {
            CreateMap<ProductItemEntity, ProductItemViewModel>();
            CreateMap<ProductItemViewModel, ProductItemEntity>();
        }
    }
}
