namespace Cart.DataAccess.Interfaces
{
    public interface ICartRepository
    {
        IEnumerable<NoSql.Models.Cart>? GetAll();
        NoSql.Models.Cart? GetById(string id);
        bool IsExists(string id);
        void Create(string id);
    }
}
