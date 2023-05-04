using AutoMapper;
using Catalog.API.Models;
using Catalog.API.Models.Product;
using Catalog.API.Models.Product.Queries;
using Catalog.Business.Exceptions;
using Catalog.Business.Interfaces;
using Catalog.Business.Models;
using Catalog.Business.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/product-management")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Products were loaded")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public async Task<IActionResult> GetAll([FromQuery] ProductQuery query)
        {
            if (query == null)
            {
                return BadRequest();
            }

            var products = (await _productService.GetAllAsync(_mapper.Map<ProductQueryEntity>(query)))
                .Select(_mapper.Map<ProductViewModel>);

            return Ok(new ProductWithHypermedia
            {
                Products = products,
                Links = new Dictionary<string, string>()
                {
                    { "Next page", @$"/api/product-management?page={query.Page + 1}{(query.CategoryId.HasValue ? $"&category-id={query.CategoryId.Value}" : string.Empty)}" }
                }
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK, "Product was added")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category wasn't found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public async Task<IActionResult> Add(ProductContentViewModel productContent)
        {
            if (productContent == null)
            {
                return BadRequest();
            }

            try
            {
                await _productService.AddAsync(_mapper.Map<ProductEntity>(productContent));
            }
            catch (EntityNotFountException)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public Task Delete(int id) => _productService.DeleteAsync(id);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK, "Product was updated")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product or category wasn't found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        public async Task<IActionResult> Update(ProductViewModel product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                await _productService.UpdateAsync(_mapper.Map<ProductEntity>(product));
            }
            catch (EntityNotFountException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
