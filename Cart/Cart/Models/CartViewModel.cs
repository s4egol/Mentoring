namespace Cart.Models
{
    public class CartViewModel
    {
        public string CartId { get; init; }
        public IEnumerable<ProductItemViewModel>? ProductItems { get; init; }
    }
}
