using Catalog.DataAccess.Filters.Interfaces;
using Catalog.DataAccess.Interfaces;
using ORM.Context;

namespace Catalog.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogContext _dbContext;
        private IProductFilterBuilder _productFilterBuilder;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public UnitOfWork(CatalogContext dbContext,
            IProductFilterBuilder productFilterBuilder)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _productFilterBuilder = productFilterBuilder ?? throw new ArgumentNullException(nameof(productFilterBuilder));
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_dbContext);
                }

                return _categoryRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dbContext, _productFilterBuilder);
                }

                return _productRepository;
            }
        }

        public Task CommitAsync() => _dbContext.SaveChangesAsync();
    }
}
