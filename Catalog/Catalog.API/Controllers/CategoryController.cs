// Copyright © 2023 EPAM Systems, Inc. All Rights Reserved. All information contained herein is, and remains the
// property of EPAM Systems, Inc. and/or its suppliers and is protected by international intellectual
// property law. Dissemination of this information or reproduction of this material is strictly forbidden,
// unless prior written permission is obtained from EPAM Systems, Inc

using System.Data;
using AutoMapper;
using Catalog.API.Models.Category;
using Catalog.Business.Exceptions;
using Catalog.Business.Interfaces;
using Catalog.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/category-management")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService,
        IMapper mapper,
        ILogger<CategoryController> logger)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Authorize(Policy = "Viewers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status200OK, "Categories were loaded")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
    public async Task<IActionResult> GetAll()
    {
        var categories = (await _categoryService.GetAllAsync())
            .Select(_mapper.Map<CategoryViewModel>);

        return Ok(categories);
    }

    [HttpPost]
    [Authorize(Policy = "Editors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status200OK, "Category was added")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Parent category wasn't find")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
    public async Task<IActionResult> Add(CategoryContentViewModel categoryContent)
    {
        if (categoryContent == null)
        {
            _logger.LogError($"Wrong input: {nameof(categoryContent)}: {categoryContent}");
            return BadRequest();
        }

        try
        {
            await _categoryService.AddAsync(_mapper.Map<CategoryEntity>(categoryContent));
        }
        catch (EntityNotFoundException)
        {
            _logger.LogError("Error adding new category");
            return NotFound();
        }

        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = "Editors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status200OK, "Category was updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Category wasn't find")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
    public async Task<IActionResult> Update(CategoryViewModel category)
    {
        if (category == null)
        {
            _logger.LogError($"Wrong input: {nameof(category)}: {category}");
            return BadRequest();
        }

        try
        {
            await _categoryService.UpdateAsync(_mapper.Map<CategoryEntity>(category));
        }
        catch (EntityNotFoundException)
        {
            _logger.LogError($"Error updating category with ID: {category.Id}");
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
    [Authorize(Policy = "Editors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status200OK, "Category was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Category wasn't find")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad input")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "User doesn't have suitable role")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            _logger.LogError($"Wrong input: {nameof(id)}: {id}");
            return BadRequest();
        }

        try
        {
            await _categoryService.DeleteAsync(id);
        }
        catch (EntityNotFoundException)
        {
            _logger.LogError($"Error deleting category with ID: {id}");
            return NotFound();
        }

        return Ok();
    }
}
