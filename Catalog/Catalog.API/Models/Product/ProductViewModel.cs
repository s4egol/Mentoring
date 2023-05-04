using Catalog.API.Models.Category;

namespace Catalog.API.Models.Product
{
    public class ProductViewModel : ProductContentViewModel
    {
        public int Id { get; set; }
        public CategoryViewModel? Category { get; set; }
    }
}
