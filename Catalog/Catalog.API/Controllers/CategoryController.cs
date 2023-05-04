using AutoMapper;
using Catalog.API.Models.Category;
using Catalog.Business.Exceptions;
using Catalog.Business.Interfaces;
using Catalog.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/category-management")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK, "Categories were loaded")]
        public async Task<IActionResult> GetAll()
        {
            var categories = (await _categoryService.GetAllAsync())
                .Select(_mapper.Map<CategoryViewModel>);

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Category was added")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Parent category wasn't find")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public async Task<IActionResult> Add(CategoryContentViewModel categoryContent)
        {
            if (categoryContent == null)
            {
                return BadRequest();
            }

            try
            {
                await _categoryService.AddAsync(_mapper.Map<CategoryEntity>(categoryContent));
            }
            catch (EntityNotFountException)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Category was updated")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category wasn't find")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public async Task<IActionResult> Update(CategoryViewModel category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            try
            {
                await _categoryService.UpdateAsync(_mapper.Map<CategoryEntity>(category));
            }
            catch (EntityNotFountException)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// API to delete existing category
        /// </summary>
        /// <param name="id">Category id that need to delete</param>
        /// <returns>code response</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Category was deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category wasn't find")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _categoryService.DeleteAsync(id);
            }
            catch (EntityNotFountException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
