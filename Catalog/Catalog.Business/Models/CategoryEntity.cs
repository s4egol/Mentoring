namespace Catalog.Business.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? ParentId { get; set; }
        public CategoryEntity? Parent { get; set; }
    }
}
