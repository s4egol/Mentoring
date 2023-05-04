namespace NoSql.Models
{
    public class ProductItem
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
}
