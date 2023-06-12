// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using Catalog.DataAccess.Models.Filters;
using ORM.Entities;

namespace Catalog.DataAccess.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product[]> GetByCategoryIdAsync(int categoryId);
    Task<bool> IsExistsAsync(int id);
    Task<IEnumerable<Product>> GetWithFiltrationAsync(ProductFilter query);
}
