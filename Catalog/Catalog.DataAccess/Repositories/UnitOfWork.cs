// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Catalog.DataAccess.Filters.Interfaces;
using Catalog.DataAccess.Interfaces;
using ORM.Context;

namespace Catalog.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogContext _dbContext;
    private readonly IProductFilterBuilder _productFilterBuilder;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public UnitOfWork(CatalogContext dbContext,
        IProductFilterBuilder productFilterBuilder)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _productFilterBuilder = productFilterBuilder ?? throw new ArgumentNullException(nameof(productFilterBuilder));
        _categoryRepository = new CategoryRepository(_dbContext);
        _productRepository = new ProductRepository(_dbContext, _productFilterBuilder);
    }

    public ICategoryRepository CategoryRepository => _categoryRepository;

    public IProductRepository ProductRepository => _productRepository;

    public Task CommitAsync() => _dbContext.SaveChangesAsync();
}
