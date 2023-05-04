using Catalog.DataAccess.Models.Filters;
using ORM.Entities;

namespace Catalog.DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product[]> GetByCategoryIdAsync(int categoryId);
        Task<bool> IsExistsAsync(int id);
        Task<IEnumerable<Product>> GetWithFiltrationAsync(ProductFilter query);
    }
}
