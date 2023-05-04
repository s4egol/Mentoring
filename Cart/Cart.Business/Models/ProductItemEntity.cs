namespace Cart.Business.Models
{
    public class ProductItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
