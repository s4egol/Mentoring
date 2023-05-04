namespace ORM.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public ICollection<Category> Children { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = null!;
    }
}
