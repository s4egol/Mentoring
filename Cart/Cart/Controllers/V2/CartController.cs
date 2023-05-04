﻿using AutoMapper;
using Cart.Business.Interfaces;
using Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Cart.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cart-management")]
    public class CartController : ControllerBase
    {
        private readonly ICartingService _cartingService;
        private readonly IMapper _mapper;

        public CartController(ICartingService cartingService, IMapper mapper)
        {
            _cartingService = cartingService ?? throw new ArgumentNullException(nameof(cartingService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
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
                return BadRequest();
            }

            return Ok(productItems);
        }
    }
}
