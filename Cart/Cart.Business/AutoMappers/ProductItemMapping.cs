using AutoMapper;
using Cart.Business.Models;
using NoSql.Models;

namespace Cart.Business.AutoMappers
{
    public class ProductItemMapping : Profile
    {
        public ProductItemMapping()
        {
            CreateMap<ProductItem, ProductItemEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));
            CreateMap<ProductItemEntity, ProductItem>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
