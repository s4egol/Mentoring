using Cart.Business.Models;

namespace Cart.Business.Interfaces
{
    public interface ICartingService
    {
        IEnumerable<CartEntity>? GetAll();
        IEnumerable<ProductItemEntity> GetItems(string cartId);
        void AddItem(string cartId, ProductItemEntity item);
        void DeleteItem(string cartId, int itemId);
        void UpdateItems(IEnumerable<ProductMessage> messages);
    }
}
