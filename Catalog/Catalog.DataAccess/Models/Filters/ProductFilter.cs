namespace Catalog.DataAccess.Models.Filters
{
    public class ProductFilter
    {
        public int? CategoryId { get; init; }
        public int Limit { get; init; }
        public int Page { get; init; }
    }
}
