using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Category
{
    public class CategoryViewModel : CategoryContentViewModel
    {
        public int Id { get; set; }
        public CategoryViewModel? Parent { get; set; }
    }
}
