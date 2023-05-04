using Microsoft.EntityFrameworkCore;
using ORM.Entities;

namespace ORM.Context
{
    public partial class CatalogContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId);      
                
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasData(new Category { Id = 1, Name = "Global", Image = "http://sun9-10.userapi.com/s/v1/if1/GLxZhpT9fdBcK75gkkrxCLmbS6RInoeJzVj7v3sjZwbz_PbWSHisrybNByu92kcyIRJyvA.jpg?size=200x200&quality=96&crop=0,0,480,480&ava=1" });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId);

                entity.HasData(new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Image = "http://sun9-10.userapi.com/s/v1/if1/GLxZhpT9fdBcK75gkkrxCLmbS6RInoeJzVj7v3sjZwbz_PbWSHisrybNByu92kcyIRJyvA.jpg?size=200x200&quality=96&crop=0,0,480,480&ava=1",
                    Price = 1000,
                    Amount = 10,
                    CategoryId = 1,
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
