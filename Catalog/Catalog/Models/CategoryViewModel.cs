using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? ParentId { get; set; }
        public CategoryViewModel? Parent { get; set; }
    }
}
