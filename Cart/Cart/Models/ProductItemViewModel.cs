using System.ComponentModel.DataAnnotations;

namespace Cart.Models
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
