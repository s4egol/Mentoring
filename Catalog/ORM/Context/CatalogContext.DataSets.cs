using Microsoft.EntityFrameworkCore;
using ORM.Entities;

namespace ORM.Context
{
    public partial class CatalogContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
