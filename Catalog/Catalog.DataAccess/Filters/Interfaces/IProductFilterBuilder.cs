using ORM.Entities;
using System.Linq.Expressions;

namespace Catalog.DataAccess.Filters.Interfaces
{
    public interface IProductFilterBuilder
    {
        Expression<Func<Product, bool>> Filter { get; }
        IProductFilterBuilder WhereCategoryId(int? categoryId);
    }
}
