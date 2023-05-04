using NoSql.Context.Abstract;
using NoSql.Models;

namespace NoSql.Context
{
    public class CartContext : LiteDbContext
    {
        public readonly LiteDbSet<Cart> Carts;

        public readonly LiteDbSet<ProductItem> ProductItems;

        public CartContext(string databasePath) : base(databasePath)
        {
            Carts = new LiteDbSet<Cart>(InternalDatabase);
            Carts.ConfigureIndices(x => x.Id);
            ProductItems = new LiteDbSet<ProductItem>(InternalDatabase);
        }
    }
}
