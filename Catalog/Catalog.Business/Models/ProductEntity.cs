﻿namespace Catalog.Business.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
    }
}
