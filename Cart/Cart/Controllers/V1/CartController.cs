// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using AutoMapper;
using Cart.Business.Interfaces;
using Cart.Business.Models;
using Cart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Cart.Controllers.V1;

[ApiController]
[Authorize(Policy = "Viewers")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cart-management")]
public class CartController : ControllerBase
{
    private readonly ICartingService _cartingService;
    private readonly IMapper _mapper;
    private readonly ILogger<CartController> _logger;

    public CartController(ICartingService cartingService,
        IMapper mapper,
        ILogger<CartController> logger)
    {
        _cartingService = cartingService ?? throw new ArgumentNullException(nameof(cartingService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Product in cart was added")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
    public IActionResult Add(string cartId, ProductItemViewModel productItem)
    {
        if (string.IsNullOrWhiteSpace(cartId) || productItem == null)
        {
            _logger.LogError($"Wrong input: {productItem}. {nameof(productItem)} or {nameof(cartId)} can't be null.");
            return BadRequest();
        }

        try
        {
            _cartingService.AddItem(cartId, _mapper.Map<ProductItemEntity>(productItem));
        }
        catch (Exception)
        {
            _logger.LogError("Error of adding product to cart");
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Product was deleted from cart")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
    public IActionResult Delete(string cartId, int productId)
    {
        if (string.IsNullOrWhiteSpace(cartId) || productId <= 0)
        {
            _logger.LogError($"Wrong input: {nameof(cartId)}: {cartId} or {nameof(productId)}: {productId}.");
            return BadRequest();
        }

        _cartingService.DeleteItem(cartId, productId);

        return Ok();
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Products added in cart were loaded")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
    public IActionResult GetCartItems(string cartId)
    {
        if (string.IsNullOrWhiteSpace(cartId))
        {
            _logger.LogError($"Wrong input: {nameof(cartId)}: {cartId}.");
            return BadRequest();
        }

        var productItems = Array.Empty<ProductItemViewModel>();

        try
        {
            productItems = _cartingService.GetItems(cartId.ToString())
                .Select(_mapper.Map<ProductItemViewModel>)
                .ToArray();
        }
        catch (Exception)
        {
            _logger.LogError("Error of cart's items processing.");
            return BadRequest();
        }

        return Ok(new CartViewModel
        {
            CartId = cartId.ToString(),
            ProductItems = productItems
        });
    }
}
