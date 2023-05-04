using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Category
{
    public class CategoryContentViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? Name { get; set; }
        [Url]
        public string? Image { get; set; }
        public int? ParentId { get; set; }
    }
}
