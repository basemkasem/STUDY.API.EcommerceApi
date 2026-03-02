using Ecommerce.Core.DTOs.Category;
using Ecommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _categoryService.GetCategoryAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto category)
    {
        var result = await _categoryService.CreateCategoryAsync(category);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto category)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, category);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }
}