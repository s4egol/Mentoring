using NoSql.Models;

namespace Cart.DataAccess.Interfaces
{
    public interface IProductItemRepository
    {
        IEnumerable<ProductItem> GetProductItems(string cartId);
        IEnumerable<ProductItem> GetProductItems(IEnumerable<int> productIds);
        void Add(ProductItem productItem);
        void UpdateRange(IEnumerable<ProductItem> productItems);
        void Delete(string cartId, int productItemId);
    }
}
