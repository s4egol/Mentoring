using AutoMapper;
using Catalog.API.Models;
using Catalog.API.Models.Product;
using Catalog.API.Models.Product.Queries;
using Catalog.Business.Exceptions;
using Catalog.Business.Interfaces;
using Catalog.Business.Models;
using Catalog.Business.Models.Queries;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService,
            IMapper mapper,
            ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize(Policy = "Viewers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status200OK, "Products were loaded")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
        public async Task<IActionResult> GetAll([FromQuery] ProductQuery query)
        {
            if (query == null)
            {
                _logger.LogError($"Wrong input: {nameof(query)}: {query}");
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
        [Authorize(Policy = "Editors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status200OK, "Product was added")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Category wasn't found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
        public async Task<IActionResult> Add(ProductContentViewModel productContent)
        {
            if (productContent == null)
            {
                _logger.LogError($"Wrong input: {nameof(productContent)}: {productContent}");
                return BadRequest();
            }

            try
            {
                await _productService.AddAsync(_mapper.Map<ProductEntity>(productContent));
            }
            catch (EntityNotFountException)
            {
                _logger.LogError($"Error adding new product in catalog");
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "Editors")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
        public Task Delete(int id) => _productService.DeleteAsync(id);

        [HttpPut]
        [Authorize(Policy = "Editors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status200OK, "Product was updated")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product or category wasn't found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
        public async Task<IActionResult> Update(ProductViewModel product)
        {
            if (product == null)
            {
                _logger.LogError($"Wrong input: {nameof(product)}: {product}");
                return BadRequest();
            }

            try
            {
                await _productService.UpdateAsync(_mapper.Map<ProductEntity>(product));
            }
            catch (EntityNotFountException)
            {
                _logger.LogError($"Error updating product with ID: {product.Id}");
                return NotFound();
            }

            return Ok();
        }
    }
}
