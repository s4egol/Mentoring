using AutoMapper;
using Catalog.API.Models.Category;
using Catalog.Business.Models;

namespace Catalog.API.AutoMappers
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryViewModel, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryViewModel>();
            CreateMap<CategoryContentViewModel, CategoryEntity>();
        }
    }
}
