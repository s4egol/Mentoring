using Catalog.DataAccess.Filters.Interfaces;
using ORM.Entities;
using System.Linq.Expressions;

namespace Catalog.DataAccess.Filters
{
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
}
