using AutoMapper;
using Cart.Business.Models;

namespace Cart.Business.AutoMappers
{
    public class CartMapping : Profile
    {
        public CartMapping()
        {
            CreateMap<NoSql.Models.Cart, CartEntity>();
        }
    }
}
