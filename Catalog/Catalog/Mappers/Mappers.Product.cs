using Catalog.Business.Models;
using Catalog.Models;

namespace Catalog.Mappers
{
    public static partial class Mappers
    {
        public static ProductViewModel ToMvc(this ProductEntity product)
            => new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Amount = product.Amount,
                CategoryId = product.CategoryId,
                Category = product.Category?.ToMvc()
            };

        public static ProductEntity ToBusiness(this ProductViewModel product)
            => new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Amount = product.Amount,
                CategoryId = product.CategoryId,
                Category = product.Category?.ToBusiness()
            };
    }
}
