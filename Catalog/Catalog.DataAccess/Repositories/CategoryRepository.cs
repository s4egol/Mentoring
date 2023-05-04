using Catalog.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using ORM.Context;
using ORM.Entities;

namespace Catalog.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _dbContext;

        public CategoryRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Category entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _dbContext.Categories.AddAsync(entity);
        }

        public async Task DeleteAsync(int entityId)
        {
            var category = await _dbContext.Categories
                .Where(category => category.Id == entityId)
                .SingleAsync();

            _dbContext.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _dbContext.Categories
                .Include(category => category.Parent)
                .ToArrayAsync();

            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await GetEntitiesByIdsQuery(new[] { id })
                .SingleOrDefaultAsync();

            return category;
        }

        public async Task<Category[]> GetByIdAsync(int[] ids)
        {
            var categories = await GetEntitiesByIdsQuery(ids)
                .ToArrayAsync();

            return categories;
        }

        public async Task<Category[]> GetChildrenAsync(int id)
        {
            var categories = await _dbContext.Categories
                .Where(category => category.ParentId.HasValue && category.ParentId.Value == id)
                .ToArrayAsync();

            return categories;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            var isCategoryExists = await _dbContext.Categories.AnyAsync(category => category.Id == id);

            return isCategoryExists;
        }

        public async Task UpdateAsync(Category entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var category = await GetEntitiesByIdsQuery(new[] { entity.Id })
                .SingleOrDefaultAsync();

            category.Name = entity.Name;
            category.Image = entity.Image;
            category.ParentId = entity.ParentId;

            _dbContext.Categories.Update(category);
        }

        private IQueryable<Category> GetEntitiesByIdsQuery(int[] ids)
        {
            var query = _dbContext.Categories
                .Include(category => category.Parent)
                .Where(category => ids.Contains(category.Id));

            return query;
        }
    }
}
