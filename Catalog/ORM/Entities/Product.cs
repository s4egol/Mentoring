using System.ComponentModel.DataAnnotations;

namespace ORM.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
