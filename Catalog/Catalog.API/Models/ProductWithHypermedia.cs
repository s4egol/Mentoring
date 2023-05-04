using Catalog.API.Models.Product;

namespace Catalog.API.Models
{
    public class ProductWithHypermedia
    {
        public IEnumerable<ProductViewModel> Products { get; init; }
        public Dictionary<string, string> Links { get; init; }
    }
}
