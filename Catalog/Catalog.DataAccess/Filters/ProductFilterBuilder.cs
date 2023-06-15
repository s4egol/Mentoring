// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.Linq.Expressions;
using Catalog.DataAccess.Filters.Interfaces;
using ORM.Entities;

namespace Catalog.DataAccess.Filters;

public sealed class ProductFilterBuilder : IProductFilterBuilder
{
    public Expression<Func<Product, bool>> Filter { get; private set; } = product => true;

    public IProductFilterBuilder WhereCategoryId(int? categoryId)
    {
        if (categoryId.HasValue)
        {
            Expression<Func<Product, bool>> hasCategory = product =>
                product.CategoryId.HasValue && product.CategoryId.Value == categoryId.Value;

            var invokedExpr = Expression.Invoke(hasCategory, Filter.Parameters.Cast<Expression>());

            Filter = Expression.Lambda<Func<Product, bool>>(
                Expression.AndAlso(Filter.Body, invokedExpr),
                Filter.Parameters);
        }

        return this;
    }
}
