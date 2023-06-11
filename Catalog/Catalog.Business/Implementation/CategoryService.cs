using AutoMapper;
using Catalog.Business.Exceptions;
using Catalog.Business.Interfaces;
using Catalog.Business.Models;
using Catalog.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using ORM.Entities;

namespace Catalog.Business.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryEntity> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            
            if (category == null)
            {
                _logger.LogError($"Category with ID: {id} wasn't find");
                throw new KeyNotFoundException(nameof(id));
            }

            return _mapper.Map<CategoryEntity>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                _logger.LogError($"Category with ID: {id} wasn't find");
                throw new EntityNotFountException(nameof(id));
            }

            await RemoveEntityReferences(_mapper.Map<CategoryEntity>(category));
            await _unitOfWork.CategoryRepository.DeleteAsync(category.Id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository
                .GetAllAsync();

            return categories
                .Select(_mapper.Map<CategoryEntity>);
        }

        public async Task UpdateAsync(CategoryEntity entity)
        {
            await ValidateUpdatedEntityAsync(entity);

            await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<Category>(entity));
            await _unitOfWork.CommitAsync();
        }

        public async Task AddAsync(CategoryEntity entity)
        {
            await ValidateAddedEntityAsync(entity);

            await _unitOfWork.CategoryRepository.AddAsync(_mapper.Map<Category>(entity));
            await _unitOfWork.CommitAsync();
        }

        private async Task RemoveEntityReferences(CategoryEntity entity)
        {
            var children = await _unitOfWork.CategoryRepository.GetChildrenAsync(entity.Id);

            foreach (var child in children)
            {
                child.ParentId = default(int?);
                await _unitOfWork.CategoryRepository.UpdateAsync(child);
            }

            var products = await _unitOfWork.ProductRepository.GetByCategoryIdAsync(entity.Id);

            foreach (var product in products)
            {
                product.CategoryId = default(int?);
                await _unitOfWork.ProductRepository.UpdateAsync(product);
            }
        }

        private async Task ValidateAddedEntityAsync(CategoryEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (entity.ParentId.HasValue)
            {
                await ValidateParentEntityAsync(entity);
            }
        }

        public async Task ValidateUpdatedEntityAsync(CategoryEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (!await _unitOfWork.CategoryRepository.IsExistsAsync(entity.Id))
            {
                _logger.LogError($"Category with ID: {entity.Id} wasn't find");
                throw new EntityNotFountException(nameof(entity));
            }

            if (entity.ParentId.HasValue)
            {
                await ValidateParentEntityAsync(entity);
            }
        }

        public async Task ValidateParentEntityAsync(CategoryEntity entity)
        {
            if (entity.Id == entity.ParentId)
            {
                _logger.LogError($"An entity cannot be a parent to itself");
                throw new Exception("An entity cannot be a parent to itself");
            }

            if (!await _unitOfWork.CategoryRepository.IsExistsAsync(entity.ParentId.Value))
            {
                _logger.LogError($"Parent category wuth ID: {entity.ParentId.Value} doesn't exist");
                throw new EntityNotFountException(nameof(entity.ParentId));
            }
        }
    }
}
