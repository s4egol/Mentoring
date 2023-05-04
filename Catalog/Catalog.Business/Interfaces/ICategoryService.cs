using Catalog.Business.Models;

namespace Catalog.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryEntity> GetByIdAsync(int id);
        Task UpdateAsync(CategoryEntity entity);
        Task AddAsync(CategoryEntity entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
    }
}
