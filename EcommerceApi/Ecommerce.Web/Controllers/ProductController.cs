using Ecommerce.Core.DTOs.Product;
using Ecommerce.Core.Interfaces.Services;
using Ecommerce.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var products = await _productService.GetAllProductsAsync(paginationParams);
        return Ok(products.Data);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductAsync(id);
        return product.IsSuccess ? Ok(product.Data) : NotFound(product.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto product)
    {
        var result = await _productService.CreateProductAsync(product);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto product)
    {
        var result = await _productService.UpdateProductAsync(id, product);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }
}