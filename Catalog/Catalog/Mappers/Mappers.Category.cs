using Catalog.Business.Models;
using Catalog.Models;

namespace Catalog.Mappers
{
    public static partial class Mappers
    {
        public static CategoryViewModel ToMvc(this CategoryEntity category)
            => new()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                ParentId = category.ParentId,
                Parent = category.Parent?.ToMvc()
            };

        public static CategoryEntity ToBusiness(this CategoryViewModel category)
            => new()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                ParentId = category.ParentId,
                Parent = category.Parent?.ToBusiness()
            };
    }
}
