using Catalog.API.Models.Query;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Models.Product.Queries
{
    public class ProductQuery : PageQueryParams
    {
        [FromQuery(Name = "category-id")]
        public int? CategoryId { get; set; }
    }
}
