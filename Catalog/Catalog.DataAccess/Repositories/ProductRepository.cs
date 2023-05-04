using Catalog.DataAccess.Filters.Interfaces;
using Catalog.DataAccess.Interfaces;
using Catalog.DataAccess.Models.Filters;
using Microsoft.EntityFrameworkCore;
using ORM.Context;
using ORM.Entities;

namespace Catalog.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _dbContext;
        private readonly IProductFilterBuilder _productFilterBuilder;

        public ProductRepository(CatalogContext dbContext,
            IProductFilterBuilder productFilterBuilder)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _productFilterBuilder = productFilterBuilder ?? throw new ArgumentNullException(nameof(productFilterBuilder));
        }

        public async Task AddAsync(Product entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _dbContext.Products.AddAsync(entity);
        }

        public async Task DeleteAsync(int entityId)
        {
            var product = await GetDbEntityByIdAsync(entityId);

            _dbContext.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _dbContext.Products
                .ToArrayAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetWithFiltrationAsync(ProductFilter query)
        {
            var filter = _productFilterBuilder
                .WhereCategoryId(query.CategoryId)
                .Filter;
            var products = await _dbContext.Products
                .Include(product => product.Category)
                .Where(filter)
                .Skip(query.Limit * (query.Page - 1))
                .Take(query.Limit)
                .ToArrayAsync();

            return products;
        }

        public async Task<Product[]> GetByCategoryIdAsync(int categoryId)
        {
            var products = await _dbContext.Products
                .Where(product => product.CategoryId.HasValue && product.CategoryId.Value == categoryId)
                .ToArrayAsync();

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await GetDbEntityByIdAsync(id);

            return product;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            var isProductExists = await _dbContext.Products.AnyAsync(product => product.Id == id);

            return isProductExists;
        }

        public async Task UpdateAsync(Product entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var product = await GetDbEntityByIdAsync(entity.Id);

            if (product == null)
            {
                throw new Exception($"Entity wasn't found {nameof(product)}");
            }

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Image = entity.Image;
            product.Price = entity.Price;
            product.Amount = entity.Amount;
            product.CategoryId = entity.CategoryId;
        }

        private async Task<Product?> GetDbEntityByIdAsync(int id)
        {
            var product = await _dbContext.Products
                .SingleOrDefaultAsync(product => product.Id == id);

            return product;
        }
    }
}
