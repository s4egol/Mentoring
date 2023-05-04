using ORM.Entities;

namespace Catalog.DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsExistsAsync(int id);
        Task<Category[]> GetByIdAsync(int[] ids);
        Task<Category[]> GetChildrenAsync(int id);
    }
}
