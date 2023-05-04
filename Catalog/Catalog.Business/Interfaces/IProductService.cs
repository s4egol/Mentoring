using Catalog.Business.Models;
using Catalog.Business.Models.Queries;

namespace Catalog.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductEntity> GetAsync(int id);
        Task<IEnumerable<ProductEntity>> GetAllAsync(ProductQueryEntity query);
        Task AddAsync(ProductEntity entity);
        Task UpdateAsync(ProductEntity entity);
        Task DeleteAsync(int id);
    }
}
