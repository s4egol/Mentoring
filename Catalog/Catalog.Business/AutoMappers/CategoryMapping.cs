using AutoMapper;
using Catalog.Business.Models;
using ORM.Entities;

namespace Catalog.Business.AutoMappers
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryEntity, Category>();
            CreateMap<Category, CategoryEntity>();
        }
    }
}
