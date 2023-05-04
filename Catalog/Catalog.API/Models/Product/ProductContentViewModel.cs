using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Product
{
    public class ProductContentViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        public int? CategoryId { get; set; }
    }
}
